namespace SnowRunnerModManager.Utils.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorField(string name, int order) : EditorMetadata(name, order);
}
