using FluentAssertions;
using Tests.ViewInfrastructure.TestValues;
using Xunit;
using static Tests.ViewInfrastructure.TestValues.ChapterTestValues;
using static Tests.ViewInfrastructure.TestValues.ChapterView;

namespace Tests.ViewInfrastructure
{
    public sealed class ViewTests
    {
        private readonly ChapterView _chapterView = NewChapterView;
        
        [Fact]
        public void LastTextLine_is_updated_when_TextLineAdded_event_is_passed()
        {
            _chapterView.Apply(SomeTextAddedToFirstChapter);
            _chapterView.LastTextLine.Should().Be(SomeText);
        }

        [Fact]
        public void LastTextLine_is_updated_with_MoreText_after_second_event()
        {
            _chapterView.Apply(SomeTextAddedToFirstChapter);
            _chapterView.Apply(MoreTextAddedToFirstChapter);
            _chapterView.LastTextLine.Should().Be(MoreText);
        }
    }
}