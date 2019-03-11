namespace Common.Exceptions
{
    public abstract class BadRequestException : DomainException
    {
        protected BadRequestException(string message) : base(message)
        {
        }
    }
}