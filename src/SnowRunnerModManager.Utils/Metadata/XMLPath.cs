using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SnowRunnerModManager.Utils.Metadata
{
    /// <summary>
    /// Specifies how a property is resolved from SnowRunner XML data.
    /// </summary>
    /// <remarks>
    /// <para>
    /// XMLPath expressions support element selection, attribute access,
    /// property-name references, fallback expressions, and parser-defined operations.
    /// </para>
    /// <list type="table">
    ///     <item>
    ///         <term><c>Tag</c></term>
    ///         <description>Selects an XML element.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>&gt;</c></term>
    ///         <description>Selects a child element.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>#Attr</c></term>
    ///         <description>Reads the value of an XML attribute.</description>
    ///     </item>
    ///     <item>
    ///         <term><c>$</c></term>
    ///         <description>
    ///             Resolves to the name of the decorated C# property.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>||</c></term>
    ///         <description>
    ///             Uses the following expression when the previous lookup fails.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>%</c></term>
    ///         <description>
    ///             Invokes a parser-defined operation.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>@</c></term>
    ///         <description>
    ///             Specifies a parser operation argument, not required for every operation.
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class XMLPath(string path) : Attribute
    {
        public string Path { get; } = path;
        public string? DefaultValue { get; init; }
        public string? ListSeparator { get; init; }
        public SpecialResolver Special { get; init; } = SpecialResolver.None;
    }

    public enum SpecialResolver
    {
        None,
        MultipleValues,
        AllChildren,
        AllAttributes
    }

    public enum SpecialOp
    {
        FileName,
        FileRelative,
        TagName,
        OriginalState
    }

    public enum RootElement
    {
        Template,
        Object
    }
    public enum TemplateType
    {
        WheelFriction,
        Wheel,
        TruckTire,
        Engine,
    }

    public class XMLElement(string path) : XMLPath(path);
    public class XMLTemplate(TemplateType type, params string[] path) : XMLPath(GetPath(type, path))
    {
        private static string GetPath(TemplateType type, params string[] path)
        {
            StringBuilder sb = new();
            string enumStr = Enum.GetName(type)!;

            for (int i = 0; i < path.Length; i++)
            {
                sb.Append($"{enumStr}>{path[i]}");

                if (i < path.Length - 1)
                    sb.Append("||");
            }

            return sb.ToString();
        }
    }
    //public class XMLOperation(string path) : XMLPath(path);
    public class XMLSpecial(SpecialOp op) : XMLPath($"%{Enum.GetName(op)!}");
    public class XMLRoot(RootElement element) : XMLPath(Enum.GetName(element)!);
}
