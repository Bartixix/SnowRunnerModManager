using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.RuntimeTests.Renderer
{
    [EditorSimpleClass]
    public class ListTestClass
    {
        public ListTestClass(string testStr, int testInt)
        {
            TestStr = testStr;
            TestInt = testInt;
        }

        public ListTestClass()
        {
            TestStr = "";
            TestInt = 0;
        }

        [EditorField("Test 1", 0)]
        public string TestStr { get; set; }

        [EditorField("Test 2", 1)]
        public int TestInt { get; set; }
    }
}
