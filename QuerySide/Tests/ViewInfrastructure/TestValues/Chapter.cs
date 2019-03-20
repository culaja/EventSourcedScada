namespace QuerySide.Tests.ViewInfrastructure.TestValues
{
    public sealed class Chapter
    {
        public string LastTextLine { get; }

        public Chapter(string lastTextLine)
        {
            LastTextLine = lastTextLine;
        }
    }
}