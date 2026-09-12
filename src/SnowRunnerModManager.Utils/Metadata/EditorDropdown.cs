namespace SnowRunnerModManager.Utils.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorDropdown(string name) : Attribute
    {
        public string Name { get; } = name;
    }
}
