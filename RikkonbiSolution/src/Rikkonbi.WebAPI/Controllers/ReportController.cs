using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rikkonbi.WebAPI.Helpers;
using Rikkonbi.WebAPI.Interfaces;
using System;
using System.IO;

namespace Rikkonbi.WebAPI.Controllers
{
    public class ReportController : BaseApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("GetCurentMonth")]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult GetCurentMonth()
        {
            try
            {
                if (_reportService.HasUnpaidOrders())
                {
                    string rootResourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                    string reportDir = Path.Combine(rootResourceDir, "Reports");
                    string xlsFileName = $"Rikkonbi-OrderList_{DateTime.Now.ToString("yyyyMM")}_{Guid.NewGuid().ToString().Replace("-", "")}.xlsx";
                    string xlsFilePath = Path.Combine(reportDir, xlsFileName);
                    string xlsFileUrl = BaseURIs.STATIC_RESOURCES + xlsFilePath.Replace(rootResourceDir, "").Replace("\\","/");

                    if (!Directory.Exists(reportDir))
                    {
                        Directory.CreateDirectory(reportDir);
                    }

                    MemoryStream ms = _reportService.CreateUnpaidReport();
                    using (var fs = new FileStream(xlsFilePath, FileMode.Create))
                    {
                        ms.CopyTo(fs);
                    }

                    return Ok(new { filePath = xlsFileUrl });
                }
                else
                {
                    return BadRequest("Hiện tại không có đơn hàng nào chưa thanh toán!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }
    }
}