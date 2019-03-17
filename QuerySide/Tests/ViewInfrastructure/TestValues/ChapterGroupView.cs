using System.Collections.Generic;
using Common;
using QuerySide.QueryCommon;

namespace Tests.ViewInfrastructure.TestValues
{
    public sealed class ChapterGroupView : GroupView<Chapter>,
        IHandle<TextLineAdded>
    {
        public static ChapterGroupView NewChapterViewGroup => new ChapterGroupView();
        
        private readonly Dictionary<Id, string> _chapterToTextLine = new Dictionary<Id, string>();
        
        public void Handle(TextLineAdded e)
        {
            _chapterToTextLine.Add(e.Name, e.TextLine);
        }

        public override Chapter GenerateViewFor(Id id) => new Chapter(
            _chapterToTextLine[id]);
    }
}