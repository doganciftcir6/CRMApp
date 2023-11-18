using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Onicorn.CRMApp.Business.Helpers.UploadHelpers
{
    public class ProjectImageUploadHelper
    {
        public static async Task<string> Run(IHostingEnvironment hostingEnvironment, IFormFile file, IConfiguration configuration, CancellationToken cancellationToken)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            string path = Path.Combine(hostingEnvironment.WebRootPath, "ProjectImages", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
                stream.Close();
            }
            var apiUrl = configuration["ApiSettings:ApiUrl"];
            var fileUrl = $"{apiUrl}/ProjectImages/{fileName}";

            return fileUrl;
        }
    }
}
