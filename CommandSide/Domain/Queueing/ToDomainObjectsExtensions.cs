namespace CommandSide.Domain.Queueing
{
    public static class ToDomainObjectsExtensions
    {
        public static CounterId ToCounterId(this string counterId) => new CounterId(counterId);
    }
}