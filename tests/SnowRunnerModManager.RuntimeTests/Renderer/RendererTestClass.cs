using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.RuntimeTests.Renderer
{
    public class RendererTestClass
    {
        public RendererTestClass(string testStr, int testInt, double testDouble, TestEnum testEnumF, bool testBoolFalse, bool testBoolTrue, RendererNestedClass nestedClass)
        {
            TestStr = testStr;
            TestInt = testInt;
            TestDouble = testDouble;
            TestEnumF = testEnumF;
            TestBoolFalse = testBoolFalse;
            TestBoolTrue = testBoolTrue;
            NestedClass = nestedClass;
        }

        public enum TestEnum
        {
            A, B, C
        }

        [EditorField("Test string", 0)]
        public string TestStr { get; set; }

        [EditorField("Test int", 1)]
        public int TestInt { get; set; }

        [EditorField("Test double", 2)]
        public double TestDouble { get; set; }

        [EditorField("Test enum", 3)]
        public TestEnum TestEnumF { get; set; }

        [EditorField("Test bool false", 4)]
        public bool TestBoolFalse { get; set; }

        [EditorField("Test bool true", 6)]
        public bool TestBoolTrue { get; set; }

        [EditorField("Test nested class", 5)]
        [EditorSimpleClass]
        public RendererNestedClass NestedClass { get; set; }
    }
}
