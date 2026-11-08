using System.Text.Json;

namespace SnowRunnerModManager.Infrastructure.Xml
{
    public abstract class XElementBase(string name, XDocument document) : XBase<XElementBase, XElement>(name, document);
    public abstract class XTemplateBase(string name, XDocument document) : XBase<XTemplateBase, XTemplate>(name, document);

    public abstract class XBase(string name, XDocument document)
    {
        public string Name { get; } = name;
        public XDocument Document { get; } = document;
        public XTemplateBinding? Template { get; private set; }
        public List<XAttribute> Attributes { get; } = [];

        public bool IsEmptyElement { get; init; } = false;

        public XAttribute? GetAttribute(string name) => Attributes.Find(c => c.Name == name);
        public bool SetTemplate(XTemplateBinding binding)
        {
            if (Template != null)
                return false;

            Template = binding;
            return true;
        }
    }

    public abstract class XBase<TParent, TChild>(string name, XDocument document) : XBase(name, document)
        where TParent : XBase<TParent, TChild>
        where TChild : XBase<TParent, TChild>
    {
        public List<TChild> Children { get; } = [];
        public abstract TParent? Parent { get; }

        public TChild? Element(string name) => Children.Find(c => c.Name == name);
        public List<TChild> Elements(string name) => Children.FindAll(c => c.Name == name);
        public List<TChild> QuerySelector(List<string> selectors)
        {
            if (selectors.Count == 0)
            {
                if (this is TChild child)
                    return [child];
                throw new ArgumentException("No selectors provided.");
            }

            IEnumerable<TChild> temp = Children
                .Where(c => c.Name == selectors[0]);

            foreach (string selector in selectors.Skip(1))
                temp = temp
                    .SelectMany(c => c.Children)
                    .Where(c => c.Name == selector);

            return [.. temp];
        }
    }
}
