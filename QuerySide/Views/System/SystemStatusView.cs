using System.Collections.Generic;
using QuerySide.QueryCommon;

namespace QuerySide.Views.System
{
    public sealed class SystemStatusView : SynchronizedView
    {
        public IReadOnlyList<PrinterStatus> PrinterStatuses { get; } = new List<PrinterStatus>()
        {
            new PrinterStatus("OK", "Online", "MockAddress1"),
            new PrinterStatus("PAPER_LOW", "Paper in printer is bellow 20%", "MockAddress2"),
            new PrinterStatus("OUT_OF_PAPER", "No paper in printer", "MockAddress3"),
            new PrinterStatus("ERROR ", "Printer is not reachable", "MockAddress4")
        };   
    }
}