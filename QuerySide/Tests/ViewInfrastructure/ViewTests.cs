using System.Threading.Tasks;
using FluentAssertions;
using Tests.ViewInfrastructure.TestValues;
using Xunit;
using static Tests.ViewInfrastructure.TestValues.ChapterTestValues;
using static Tests.ViewInfrastructure.TestValues.ChapterView;
using static Tests.ViewInfrastructure.TestValues.SomeOtherEvent;

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
        public void Version_is_incremented_when_TextLineAdded_event_is_passed()
        {
            _chapterView.Apply(SomeTextAddedToFirstChapter);
            _chapterView.Version.Should().Be(1);
        }

        [Fact]
        public void Version_is_not_incremented_when_some_event_that_is_not_handled_by_the_view_is_passed()
        {
            _chapterView.Apply(NewSomeOtherEvent);
            _chapterView.Version.Should().Be(0);
        }

        [Fact]
        public async Task waiting_task_is_completed_when_applying_event_that_can_be_handled_by_view()
        {
            var task = _chapterView.WaitNewVersionAsync();
            
            _chapterView.Apply(SomeTextAddedToFirstChapter);

            await task;
            task.IsCompleted.Should().BeTrue();
        }

        [Fact]
        public void LastTextLine_is_updated_with_MoreText_after_second_event()
        {
            _chapterView.Apply(SomeTextAddedToFirstChapter);
            _chapterView.Apply(MoreTextAddedToFirstChapter);
            _chapterView.LastTextLine.Should().Be(MoreText);
        }
        
        [Fact]
        public void Version_is_2_after_second_event_is_applied()
        {
            _chapterView.Apply(SomeTextAddedToFirstChapter);
            _chapterView.Apply(MoreTextAddedToFirstChapter);
            _chapterView.Version.Should().Be(2);
        }

        [Fact]
        public async Task second_waiting_for_version_change_will_work_even_if_first_task_was_not_awaited()
        {
            var task1 = _chapterView.WaitNewVersionAsync();
            var task2 = _chapterView.WaitNewVersionAsync(); 
            
            _chapterView.Apply(SomeTextAddedToFirstChapter);

            await task2;
            task2.IsCompleted.Should().BeTrue();
        }        
    }
}