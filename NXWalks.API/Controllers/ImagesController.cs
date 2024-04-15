using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NXWalks.API.Models.Domains;
using NXWalks.API.Models.DTO;
using NXWalks.API.Repositories;

namespace NXWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly FileUploadRepository fileUploadRepository;

        public ImagesController(FileUploadRepository fileUploadRepository)
        {
            this.fileUploadRepository = fileUploadRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDTO imageUploadRequestDTO)
        {
            ValidateFileUpload(imageUploadRequestDTO);

            if(ModelState.IsValid)
            {
                //Now Convert the Request DTO To Domain Model 
                var ImageDomainModel = new Image
                {
                    File = imageUploadRequestDTO.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDTO.File.FileName),
                    FileSize = imageUploadRequestDTO.File.Length,
                    FileName = imageUploadRequestDTO.FileName,
                    FileDescription = imageUploadRequestDTO.FileDescription,
                };
                //Upload image through repository 

                await fileUploadRepository.UploadFile(ImageDomainModel);

                return Ok(ImageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO imageUploadRequestDTO)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtentions.Contains(Path.GetExtension(imageUploadRequestDTO.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Extention Error");
            }

            if(imageUploadRequestDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "The File Size is too large");
            }
        }
    }
}
