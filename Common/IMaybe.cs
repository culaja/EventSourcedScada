namespace Common
{
    public interface IMaybe
    {
        bool HasValue { get; }

        bool HasNoValue { get; }
    }
}