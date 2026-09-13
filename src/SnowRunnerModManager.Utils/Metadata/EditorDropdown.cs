namespace SnowRunnerModManager.Utils.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorDropdown(string name, int order) : EditorMetadata(name, order);
}
