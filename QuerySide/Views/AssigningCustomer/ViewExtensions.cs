namespace QuerySide.Views.AssigningCustomer
{
    public static class ViewExtensions
    {
        public static CounterNumber ToCounterNumber(this int counterId) => new CounterNumber(counterId);
    }
}