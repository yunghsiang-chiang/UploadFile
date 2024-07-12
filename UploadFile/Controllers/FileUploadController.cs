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
    [EnableCors(origins: "http://192.168.11.51:8080", headers: "*", methods: "*")]
    public class FileUploadController : ApiController
    {
        [HttpPost]
        public string[] UploadFiles()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string[] path = new string[files.Count];
            for(var i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                string roothPath = "~/Upload/" + file.FileName;
                path[i] = roothPath.Substring(1);
                file.SaveAs(HttpContext.Current.Server.MapPath(roothPath));
            }

            return path;
        }
    }
}
