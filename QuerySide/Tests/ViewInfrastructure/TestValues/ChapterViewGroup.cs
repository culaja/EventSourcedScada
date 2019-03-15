using QuerySide.QueryCommon;

namespace Tests.ViewInfrastructure.TestValues
{
    public sealed class ChapterViewGroup : ViewGroup<ChapterView>,
        IHandle<TextLineAdded>
    {
        public static ChapterViewGroup NewChapterViewGroup => new ChapterViewGroup();
        
        public void Handle(TextLineAdded e)
        {
            PassEventToViewWithId(e.Name, e);
        }
    }
}