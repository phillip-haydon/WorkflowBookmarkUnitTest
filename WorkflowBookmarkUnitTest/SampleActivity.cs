namespace WorkflowBookmarkUnitTest
{
    using System.Activities;

    public class SampleActivity : NativeActivity<bool>
    {
        public InArgument<int> ValueOne { get; set; }
        public InArgument<int> ValueTwo { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        protected override void Execute(NativeActivityContext context)
        {
            var valueOne = context.GetValue(ValueOne);
            var valueTwo = context.GetValue(ValueTwo);

            if (valueTwo % 2 == 0)
            {
                //We do something and return true...
                context.SetValue(Result, true);

                return;
            }

            // Else condition was not met so we sleep and come back later
            context.CreateBookmark("Bookmark_" + valueOne, ResumeBookmarkCallback);
        }

        private void ResumeBookmarkCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
            // When we wake up, return false and let it try again
            context.SetValue(Result, false);
        }
    }
}