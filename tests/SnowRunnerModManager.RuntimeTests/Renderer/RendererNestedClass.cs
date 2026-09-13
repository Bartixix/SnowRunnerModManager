using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.RuntimeTests.Renderer
{
    [EditorSimpleClass]
    public class RendererNestedClass
    {
        public RendererNestedClass(string nestedString, int nestedInt)
        {
            NestedString = nestedString;
            NestedInt = nestedInt;
        }

        [EditorField("Nested test string", 0)]
        public string NestedString { get; set; }

        [EditorField("Nested test int", 1)]
        public int NestedInt { get; set; }
    }
}
