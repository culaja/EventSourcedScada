using System.Threading.Tasks;
using FluentAssertions;
using Tests.ViewInfrastructure.TestValues;
using Xunit;
using static Tests.ViewInfrastructure.TestValues.ChapterTestValues;
using static Tests.ViewInfrastructure.TestValues.ChapterViewGroup;

namespace Tests.ViewInfrastructure
{
    public sealed class ViewGroupTests
    {
        private readonly ChapterViewGroup _chapterViewGroup = NewChapterViewGroup;

        [Fact]
        public async Task SomeText_returned_when_waiting_for_FirstChapter_to_get_updated_after_applying_event()
        {
            var task = _chapterViewGroup.WaitNewVersionOfViewWithId(FirstChapter);
            
            _chapterViewGroup.Apply(SomeTextAddedToFirstChapter);

            (await task).LastTextLine.Should().Be(SomeText);
        }
        
        [Fact]
        public async Task SomeText_returned_when_waiting_for_SecondChapter_to_get_updated_after_applying_event()
        {
            var task = _chapterViewGroup.WaitNewVersionOfViewWithId(SecondChapter);
            
            _chapterViewGroup.Apply(SomeTextAddedToFirstChapter);
            _chapterViewGroup.Apply(MoreTextAddedToFirstChapter);
            _chapterViewGroup.Apply(SomeTextAddedToSecondChapter);

            (await task).LastTextLine.Should().Be(SomeText);
        }
    }
}