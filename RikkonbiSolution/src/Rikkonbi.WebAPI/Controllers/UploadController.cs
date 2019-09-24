using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rikkonbi.WebAPI.Helpers;
using Rikkonbi.WebAPI.Services;
using System;
using System.IO;
using System.Linq;

namespace Rikkonbi.WebAPI.Controllers
{
    public class UploadController : BaseApiController
    {
        [HttpPost, DisableRequestSizeLimit]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Upload(IFormFile image)
        {
            try
            {
                var file = Request.Form.Files[0];
                byte[] fileBytes;

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                if (ImageUntiltity.isRealImageFile(fileBytes))
                {
                    var rootResourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                    var imageDir = Path.Combine(rootResourceDir, "Images");

                    if (!Directory.Exists(imageDir))
                    {
                        Directory.CreateDirectory(imageDir);
                    }

                    if (file.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString().Replace("-", "") + "." + file.FileName.Split('.').Last();
                        string filePath = Path.Combine(imageDir, fileName);
                        string fileUrl = BaseURIs.STATIC_RESOURCES + filePath.Replace(rootResourceDir, "").Replace("\\", "/");

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        return Ok(new { dbPath =  fileUrl });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return StatusCode(500, "The uploaded File is not Image File");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}