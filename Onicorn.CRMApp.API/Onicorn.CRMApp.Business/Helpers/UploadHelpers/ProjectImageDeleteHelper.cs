using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Onicorn.CRMApp.Business.Helpers.UploadHelpers
{
    public class ProjectImageDeleteHelper
    {
        public static void Delete(IHostingEnvironment hostingEnvironment, string file)
        {
            try
            {
                string path = Path.Combine(hostingEnvironment.WebRootPath, "ProjectImages", file);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
