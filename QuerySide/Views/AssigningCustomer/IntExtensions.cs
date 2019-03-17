namespace QuerySide.Views.AssigningCustomer
{
    public static class IntExtensions
    {
        public static CounterId ToCounterId(this int counterId) => new CounterId(counterId);
    }
}