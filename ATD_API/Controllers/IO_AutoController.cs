using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IO_AutoController : ControllerBase
    {
        [HttpPost("[controller]/[action]")]

        public async Task<IActionResult> UploadSqliteTemplateData(IFormFile file)
        {

            try

            {

                if (file == null || file.Length == 0)

                {

                    return BadRequest("No file uploaded.");

                }
                var fileName = file.FileName;

                string backupsPath = "E:\\SqliteTemplateBackup\\Backups";

                string modelNamePath = "E:\\SqliteTemplateBackup\\Data.db";

                if (!Directory.Exists(backupsPath))

                {

                    Directory.CreateDirectory(backupsPath);

                }

                string? dir = Path.GetDirectoryName(modelNamePath);

                if (dir != null && !Directory.Exists(dir))

                {

                    Directory.CreateDirectory(dir);

                }

                string ip = HttpContext.Connection.RemoteIpAddress!.ToString().Replace(':', '.');

                string backupModelNameFile = Path.Combine(backupsPath, "[" + ip + "]_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".db");

                if (System.IO.File.Exists(modelNamePath))

                {

                    System.IO.File.Copy(modelNamePath, backupModelNameFile);

                }

                using (var stream = new FileStream(modelNamePath, FileMode.Create))

                {

                    await file.CopyToAsync(stream);

                }

                return Ok("文件上传成功");

            }

            catch (Exception ex)

            {

                return BadRequest(ex);

            }

        }


        /// <summary>

        /// IO模板文件下载

        /// </summary>

        /// <returns></returns>

        [HttpGet("[controller]/[action]")]

        public IActionResult GetSqliteTemplateData()

        {

            try

            {

                string dbFilePath = "E:\\SqliteTemplateBackup\\Data.db";


                if (!System.IO.File.Exists(dbFilePath))

                {

                    return NotFound("Data.db 文件未找到");

                }

                var fileBytes = System.IO.File.ReadAllBytes(dbFilePath);

                var fileName = "Data.db";

                var mimeType = "application/octet-stream";

                return File(fileBytes, mimeType, fileName);

            }

            catch (Exception ex)

            {

                return BadRequest(ex.Message);

            }

        }
    }
}
