using FluentAssertions;
using QuerySide.Tests.ViewInfrastructure.TestValues;
using Xunit;

namespace QuerySide.Tests.ViewInfrastructure
{
    public sealed class ViewTests
    {
        private readonly ChapterView _chapterView = ChapterView.NewChapterView;

        [Fact]
        public void LastTextLine_is_updated_when_TextLineAdded_event_is_passed()
        {
            _chapterView.Apply(ChapterTestValues.SomeTextAddedToFirstChapter);
            _chapterView.LastTextLine.Should().Be(ChapterTestValues.SomeText);
        }

        [Fact]
        public void LastTextLine_is_updated_with_MoreText_after_second_event()
        {
            _chapterView.Apply(ChapterTestValues.SomeTextAddedToFirstChapter);
            _chapterView.Apply(ChapterTestValues.MoreTextAddedToFirstChapter);
            _chapterView.LastTextLine.Should().Be(ChapterTestValues.MoreText);
        }
    }
}