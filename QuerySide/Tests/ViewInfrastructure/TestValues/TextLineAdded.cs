using System;
using Common.Messaging;

namespace QuerySide.Tests.ViewInfrastructure.TestValues
{
    public sealed class TextLineAdded : DomainEvent
    {
        public ChapterName Name { get; }
        public string TextLine { get; }

        public TextLineAdded(ChapterName name, string textLine) : base(Guid.NewGuid(), "Chapter")
        {
            Name = name;
            TextLine = textLine;
        }
    }
}