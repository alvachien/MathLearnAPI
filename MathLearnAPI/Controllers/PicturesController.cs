using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MathLearnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<PicturesController> _logger;
        private IAuthorizationService _authorizationService;

        public PicturesController(IHostingEnvironment env, ILogger<PicturesController> logger, IAuthorizationService authorizationService)
        {
            _hostingEnvironment = env;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        // GET: api/PhotoFile
        [HttpGet]
        public IActionResult Get()
        {
            return Forbid();
        }

        // GET: api/Pictures/filename
        [HttpGet("{filename}")]
        [ResponseCache(Duration = 864000)]
        public IActionResult Get(string filename)
        {
            String strFullFile = Startup.UploadFolder + "\\" + filename;
            if (System.IO.File.Exists(strFullFile))
            {
                var image = System.IO.File.OpenRead(Startup.UploadFolder + "\\" + filename);
                return File(image, "image/jpeg");
            }

            return NotFound();
        }

        // POST: api/Pictures
        [HttpPost]
        public async Task<IActionResult> UploadPhotos(ICollection<IFormFile> files)
        {
            if (Request.Form.Files.Count <= 0)
                return BadRequest("No Files");

            // Only care about the first file
            var file = Request.Form.Files[0];

            // TBD. Authority control
            //var usrName = User.FindFirst(c => c.Type == "sub").Value;
            //Int32 minSize = 0, maxSize = 0; Boolean allowUpload = false;
            //using (SqlConnection conn = new SqlConnection(Startup.DBConnectionString))
            //{
            //    await conn.OpenAsync();

            //    String cmdText = @"SELECT [UploadFileMinSize],[UploadFileMaxSize],[PhotoUpload]
            //          FROM [dbo].[UserDetail] WHERE [UserID] = N'" + usrName + "'";
            //    SqlCommand cmdUserRead = new SqlCommand(cmdText, conn);
            //    SqlDataReader usrReader = await cmdUserRead.ExecuteReaderAsync();
            //    if (usrReader.HasRows)
            //    {
            //        usrReader.Read();
            //        if (!usrReader.IsDBNull(0))
            //            minSize = usrReader.GetInt32(0);
            //        if (!usrReader.IsDBNull(1))
            //            maxSize = usrReader.GetInt32(1);
            //        if (!usrReader.IsDBNull(2))
            //            allowUpload = usrReader.GetBoolean(2);
            //    }

            //    usrReader.Close();
            //    usrReader = null;
            //    cmdUserRead.Dispose();
            //    cmdUserRead = null;
            //}
            //if (!allowUpload || maxSize == 0 || maxSize <= minSize)
            //{
            //    return StatusCode(400, "User has no authoirty or wrongly set!");
            //}

            var fileSize = file.Length / 1024;
            if (fileSize > 2048)
            {
                return StatusCode(400, "File is too large");
            }

            // TBD: File size control
            //if (maxSize >= fileSize && minSize <= fileSize)
            //{
            //    // Succeed
            //}
            //else
            //{
            //    return StatusCode(400, "Wrong size!");
            //}

            //var fileFullPath = Path.Combine(Startup.UploadFolder, strFile);
            //var filename = Path.GetFileNameWithoutExtension(fileFullPath);
            var filename = Guid.NewGuid().ToString("N"); // Use GUID to avoid name conflicts
            var idx1 = file.FileName.LastIndexOf('.');
            var fileext = file.FileName.Substring(idx1);
            using (var fileStream = new FileStream(Path.Combine(Startup.UploadFolder, filename + fileext), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return new JsonResult("api/Pictures/" + filename + "." + fileext);
        }

        // PUT: api/Pictures/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Forbid();
        }

        // DELETE: api/Pictures/5
        [HttpDelete("{strfile}")]
        [Authorize]
        public async Task<IActionResult> Delete(String strfile)
        {
            var fileFullPath = Path.Combine(Startup.UploadFolder, strfile);
            //var filename = Path.GetFileNameWithoutExtension(fileFullPath);
            //var fileext = Path.GetExtension(fileFullPath);

            try
            {
                // File
                if (System.IO.File.Exists(fileFullPath))
                {
                    System.IO.File.Delete(fileFullPath);
                }
            }
            catch (Exception exp)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(exp.Message);
#endif

                return BadRequest(exp.Message);
            }

            return Ok();
        }
    }
}
