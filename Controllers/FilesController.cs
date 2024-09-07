using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace reportesApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost("UploadFilesTicket")]
        public async Task<IActionResult> UploadFiles([FromQuery] string id_ticket, List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            string workingDirectory = Environment.CurrentDirectory;
            string ruta = workingDirectory + @"\Uploads\Files\" + id_ticket.ToString();
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }


            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(ruta, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { message = $"{files.Count} files uploaded successfully." });
        }
    }
}
