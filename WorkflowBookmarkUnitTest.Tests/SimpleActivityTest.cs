namespace WorkflowBookmarkUnitTest.Tests
{
    using Microsoft.Activities.UnitTesting;
    using System.Linq;
    using WorkflowBookmarkUnitTest;
    using Xunit;

    public class SimpleActivityTest
    {
        [Fact]
        public void Given_10_For_Value_Two_Should_Have_A_Return_Argument_Of_True()
        {
            // Arrange
            var wfTester = WorkflowApplicationTest.Create(new SampleActivity
            {
                ValueOne = 123,
                ValueTwo = 10
            });

            // Act
            wfTester.TestActivity();

            // Assert
            wfTester.AssertOutArgument.IsTrue("Result");
        }

        [Fact]
        public void Given_9_For_Value_Two_Should_Have_Bookmark_Using_Value_One()
        {
            // Arrange
            var wfTester = WorkflowApplicationTest.Create(new SampleActivity
            {
                ValueOne = 123,
                ValueTwo = 9
            });

            // Act
            wfTester.TestActivity();
            wfTester.TestWorkflowApplication.GetBookmarks();

            // Assert
            Assert.Equal("Bookmark_123", wfTester.Bookmarks.First());
        }

        [Fact]
        public void When_Resuming_A_Workflow_Should_Get_The_Return_Value_False()
        {
            // Arrange
            var wfTester = WorkflowApplicationTest.Create(new SampleActivity
            {
                ValueOne = 123,
                ValueTwo = 9
            });

            // Act
            wfTester.TestActivity();
            wfTester.TestWorkflowApplication.ResumeBookmark("Bookmark_123", 0);

            // Assert
            wfTester.AssertOutArgument.IsFalse("Result");
        }
    }
}