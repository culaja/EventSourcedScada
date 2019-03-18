namespace QuerySide.Views.System
{
    public sealed class PrinterStatus
    {
        public string Status { get; }
        public string Description { get; }
        public string BusAddress { get; }

        public PrinterStatus(
            string status,
            string description,
            string busAddress)
        {
            Status = status;
            Description = description;
            BusAddress = busAddress;
        }
    }
}