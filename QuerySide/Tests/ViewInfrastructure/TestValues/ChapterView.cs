using QuerySide.QueryCommon;

namespace QuerySide.Tests.ViewInfrastructure.TestValues
{
    public sealed class ChapterView : View,
        IHandle<TextLineAdded>
    {
        public string LastTextLine { get; private set; }
        
        public static ChapterView NewChapterView => new ChapterView();
        
        public void Handle(TextLineAdded e)
        {
            LastTextLine = e.TextLine;
        }
    }
}