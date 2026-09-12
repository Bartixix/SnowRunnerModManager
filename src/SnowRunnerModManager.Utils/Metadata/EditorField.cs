namespace SnowRunnerModManager.Utils.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorField(string name) : Attribute
    {
        public string Name { get; } = name;
    }
}
