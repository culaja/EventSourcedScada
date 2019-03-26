namespace QuerySide.Tests.ViewInfrastructure.TestValues
{
    public static class ChapterTestValues
    {
        public static readonly string SomeText = "SomeText";
        public static readonly string MoreText = "MoreText";

        public static readonly ChapterName FirstChapter = new ChapterName("FirstChapter");
        public static readonly TextLineAdded SomeTextAddedToFirstChapter = new TextLineAdded(FirstChapter, SomeText);
        public static readonly TextLineAdded MoreTextAddedToFirstChapter = new TextLineAdded(FirstChapter, MoreText);

        public static readonly ChapterName SecondChapter = new ChapterName("SecondChapter");
        public static readonly TextLineAdded SomeTextAddedToSecondChapter = new TextLineAdded(SecondChapter, SomeText);
        public static readonly TextLineAdded MoreTextAddedToSecondChapter = new TextLineAdded(SecondChapter, MoreText);
    }
}