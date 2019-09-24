using System.IO;

namespace Rikkonbi.WebAPI.Interfaces
{
    public interface IReportService
    {
        MemoryStream CreateUnpaidReport();
        bool HasUnpaidOrders();
    }
}