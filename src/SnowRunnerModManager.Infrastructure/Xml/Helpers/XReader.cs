using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SnowRunnerModManager.Infrastructure.Xml.Helpers
{
    internal class XReader
    {
        public static void Read(XDocument doc, XmlReader xr)
        {
            XTemplateRoot? templateRoot = null;
            XElementRoot? elementRoot = null;

            xr.MoveToContent();

            XTemplateBase? template = null;
            XElementBase? element = null;

            bool readTemplates = false;
            bool readElements = false;

            if (xr.LocalName != "_templates")
                readTemplates = true;

            do
            {
                if (xr.Depth == 0 && xr.NodeType == XmlNodeType.Element)
                {
                    if (!readTemplates && templateRoot == null)
                    {
                        templateRoot = new(xr.LocalName, doc);
                        template = templateRoot;

                        doc.SetTemplateRoot(templateRoot);
                        continue;
                    }

                    if (!readElements && elementRoot == null)
                    {
                        elementRoot = new(xr.LocalName, doc);
                        element = elementRoot;

                        doc.SetElementRoot(elementRoot);
                        continue;
                    }

                    throw new Exception();
                }

                if (!CheckType(xr))
                    continue;

                if (!readTemplates)
                {
                    if (template == null)
                        throw new Exception();

                    template = HandleTemplate(template, xr, out readTemplates);
                    continue;
                }

                if (!readElements)
                {
                    if (element == null)
                        throw new Exception();

                    element = HandleElement(element, xr, out readElements);
                    continue;
                }
            } while (xr.MoveToNextAttribute() || xr.Read());
        }
        private static bool CheckType(XmlReader xr)
        {
            return xr.NodeType switch
            {
                XmlNodeType.Element => true,
                XmlNodeType.EndElement => true,
                XmlNodeType.Attribute => true,
                _ => false
            };
        }

        private static XTemplateBase HandleTemplate(XTemplateBase template, XmlReader xr, out bool exitingRoot)
        {
            exitingRoot = false;

            switch (xr.NodeType)
            {
                case XmlNodeType.Element:
                    template = ValidateEmpty(template);

                    XTemplate temp = new(xr.LocalName, template, template.Document) { IsEmptyElement = xr.IsEmptyElement };
                    template.Children.Add(temp);

                    return temp;

                case XmlNodeType.EndElement:
                    template = ValidateEmpty(template);

                    if (template.Parent == null)
                    {
                        exitingRoot = true;
                        return template;
                    }

                    return template.Parent;

                case XmlNodeType.Attribute:
                    if (xr.LocalName == "Include" && template is XTemplateRoot templateRoot)
                    {
                        templateRoot.SetInclude(xr.Value);
                        return template;
                    }

                    if (xr.LocalName == "_template")
                    {
                        SetGlobalTemplate(template, xr);
                        return template;
                    }

                    template.Attributes.Add(new(xr.LocalName, xr.Value));
                    return template;
                default:
                    throw new NotImplementedException();
            }
        }

        private static XElementBase HandleElement(XElementBase element, XmlReader xr, out bool exitingRoot)
        {
            exitingRoot = false;

            switch (xr.NodeType)
            {
                case XmlNodeType.Element:
                    element = ValidateEmpty(element);

                    XElement temp = new(xr.LocalName, element, element.Document) { IsEmptyElement = xr.IsEmptyElement };
                    element.Children.Add(temp);
                    return temp;

                case XmlNodeType.EndElement:
                    element = ValidateEmpty(element);

                    if (element.Parent == null)
                    {
                        exitingRoot = true;
                        return element;
                    }

                    return element.Parent;

                case XmlNodeType.Attribute:
                    if (xr.LocalName == "_template")
                    {
                        if (!BindTemplate(element, xr))
                            throw new Exception();

                        return element;
                    }

                    element.Attributes.Add(new(xr.LocalName, xr.Value));
                    return element;

                default:
                    throw new NotImplementedException();
            }
        }

        private static bool BindTemplate(XElementBase element, XmlReader xr)
        {
            XDocument doc = element.Document;

            XTemplateRoot? localTemplates = doc.TemplateRoot;

            if (localTemplates == null)
            {
                SetGlobalTemplate(element, xr);
                return true;
            }

            XTemplate? template = localTemplates.Element(element.Name)?.Element(xr.Value);

            if (template == null)
            {
                SetGlobalTemplate(element, xr);
                return true;
            }

            XTemplateBinding binding = new(template);
            element.SetTemplate(binding);
            return true;
        }

        private static void SetGlobalTemplate(XBase xBase, XmlReader xr)
        {
            string globalPath = $"{xBase.Name}\\{xr.Value}";
            xBase.SetTemplate(new(globalPath));
        }

        private static XTemplateBase ValidateEmpty(XTemplateBase template)
        {
            if (template.IsEmptyElement)
                return template.Parent ?? throw new Exception();

            return template;
        }

        private static XElementBase ValidateEmpty(XElementBase element)
        {
            if (element.IsEmptyElement)
                return element.Parent ?? throw new Exception();

            return element;
        }
    }
}
