using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace UploadFile.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileUploadController : ApiController
    {
        [HttpPost]
        public IHttpActionResult UploadFiles()
        {
            try
            {
                HttpFileCollection files = HttpContext.Current.Request.Files;
                if (files.Count == 0)
                {
                    return Json(new { uploaded = false, error = new { message = "No file uploaded." } });
                }

                string[] paths = new string[files.Count];
                for (var i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string rootPath = "~/Upload/" + file.FileName;
                    string fullPath = HttpContext.Current.Server.MapPath(rootPath);
                    file.SaveAs(fullPath);

                    // 構建圖片的 URL
                    string fileUrl = "http://internal.hochi.org.tw:8081" + rootPath.Substring(1);
                    paths[i] = fileUrl;
                }

                // 判斷是否為 CKEditor 請求
                if (HttpContext.Current.Request.Headers["User-Agent"] != null &&
                    HttpContext.Current.Request.Headers["User-Agent"].Contains("CKEditor"))
                {
                    // CKEditor 的返回格式
                    return Json(new { uploaded = true, url = paths[0] });
                }

                // 默認返回現有應用的字串數組格式
                return Ok(paths);
            }
            catch (Exception ex)
            {
                return Json(new { uploaded = false, error = new { message = ex.Message } });
            }
        }
    }

}
