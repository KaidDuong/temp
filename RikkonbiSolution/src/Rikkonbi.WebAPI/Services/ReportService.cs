using OfficeOpenXml.Style;
using Rikkonbi.Core.Aggregates.OrderReport;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rikkonbi.WebAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private List<UnpaidOrder> _unpaidOrders;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public MemoryStream CreateUnpaidReport()
        {

            var stream = new MemoryStream();

            using (var package = new OfficeOpenXml.ExcelPackage(stream))
            {
                var i = 0;
                i++;
                var sheetName = DateTime.Now.ToString("yyyy-MM");
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                workSheet.Cells[i, 1].Value = "Danh sách đơn hàng";
                workSheet.Cells[i, 1].Style.Font.Bold = true;
                workSheet.Cells[i, 1].Style.Font.Size = 16;
                i++;
                workSheet.Cells[i, 1].Value = "Tháng: " + DateTime.Now.ToString("MM/yyyy");
                workSheet.Cells[i, 1].Style.Font.Bold = true;
                workSheet.Cells[i, 1].Style.Font.Size = 12;
                i++;
                workSheet.Cells[i, 1].Value = "STT";
                workSheet.Cells[i, 2].Value = "Họ và tên";
                workSheet.Cells[i, 3].Value = "Email";
                workSheet.Cells[i, 4].Value = "Tổng tiền (VND)";
                workSheet.Cells[i, 5].Value = "Trạng thái thanh toán";
                workSheet.Cells[i, 6].Value = "Ngày thanh toán";
                workSheet.Cells[i, 7].Value = "Ghi chú";
                workSheet.Cells[i, 1, i, 7].Style.Font.Bold = true;
                workSheet.Cells[i, 1, i, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[i, 1, i, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

                var exportData = new List<UnpaidOrder>();

                foreach (var item in _unpaidOrders)
                {
                    if (exportData.Any(x => x.Email == item.Email))
                    {
                        var existItem = exportData.First(x => x.Email == item.Email);
                        existItem.TotalAmount += item.TotalAmount;
                    }
                    else
                    {
                        exportData.Add(new UnpaidOrder
                        {
                            Email = item.Email,
                            FullName = item.FullName,
                            TotalAmount = item.TotalAmount
                        });
                    }
                }

                i++;
                var row = i;
                foreach (var item in exportData)
                {
                    workSheet.Cells[row, 1].Value = row - i + 1;
                    workSheet.Cells[row, 2].Value = item.FullName;
                    workSheet.Cells[row, 3].Value = item.Email;
                    workSheet.Cells[row, 4].Value = item.TotalAmount;
                    workSheet.Cells[row, 4].Style.Numberformat.Format = "###,###,##0";
                    workSheet.Cells[row, 5].Value = "Chưa thanh toán";
                    row++;
                }

                workSheet.Column(1).Width = 5;
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).Width = 30;


                for (row = 3; row <= 3 + exportData.Count; row++)
                {
                    for (int col = 1; col <= 7; col++)
                    {
                        workSheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                }

                package.Save();

                if (_unpaidOrders.Count > 0)
                {
                    _reportRepository.UpdatePaymentStatus(_unpaidOrders.Select(x => x.OrderId).ToList());
                }
            }

            stream.Position = 0;
            return stream;
        }

        public bool HasUnpaidOrders()
        {
            DateTime currentDate = DateTime.Now;
            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 23, 59, 59);

            // Get list of unpaid orders
            _unpaidOrders = _reportRepository.GetUnpaidList(currentDate);

            return _unpaidOrders.Count > 0;
        }
    }
}