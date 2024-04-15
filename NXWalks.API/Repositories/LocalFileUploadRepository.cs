using NXWalks.API.Data;
using NXWalks.API.Models.Domains;

namespace NXWalks.API.Repositories
{
    public class LocalFileUploadRepository : FileUploadRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NXWalksDBContext nXWalksDBContext;

        public LocalFileUploadRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,NXWalksDBContext nXWalksDBContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nXWalksDBContext = nXWalksDBContext;
        }
        public async Task<Image> UploadFile(Image image)
        {
            //Now First defining the path to the uploaded files that will get from iWebhostingEnviorment
            var localpath = Path.Combine(webHostEnvironment.ContentRootPath, "UploadedImages",
               $"{image.FileName}{image.FileExtension}");

            //now using File Stream to upload the Coming File to the Above path defined
            using var stream = new FileStream(localpath, FileMode.Create);
            //CopytoAsync is the FileForm Function that will take stream and create that 
            await image.File.CopyToAsync(stream);

            //Now with above line of code the file will be uploaded into API now time to save changes in data base 
            //using HTTPContextAccessor that will provide the URL of Running project that will serve the files will provide an actual path 
            //httpContextAccessor.HttpContext.Request.Scheme = this is will wheater its http or https defined scheme && httpContextAccessor.HttpContext.Request.Host == localHost && httpContextAccessor.HttpContext.Request.PathBase == host no i.e 7564 then provide the folder name and File along with its extension
            var URLFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/UploadedImages/{image.FileName}{image.FileExtension}";

            image.FilePath = URLFilePath;

            // Now Saving the Changes to the Database 
            await nXWalksDBContext.Images.AddAsync(image);
            await nXWalksDBContext.SaveChangesAsync();

            return image;
        }
    }
}
