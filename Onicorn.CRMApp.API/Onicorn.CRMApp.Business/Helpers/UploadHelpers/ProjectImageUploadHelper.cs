using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Helpers.UploadHelpers
{
    public class ProjectImageUploadHelper
    {
        public static async Task Run(IHostingEnvironment hostingEnvironment, IFormFile file, CancellationToken cancellationToken)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.UtcNow.Minute + DateTime.UtcNow.Second;
            var extName = Path.GetExtension(file.FileName);
            string path = Path.Combine(hostingEnvironment.WebRootPath, "ProjectImages", fileName + extName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
                stream.Close();
            }
        }
    }
}
