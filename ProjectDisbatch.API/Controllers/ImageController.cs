using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDisbatch.API.Models.Domain;
using ProjectDisbatch.API.Models.DTO;
using ProjectDisbatch.API.Repositories;

namespace ProjectDisbatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        //POST: /api/Images/Upload
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if (ModelState.IsValid)
            {
                //Convert DTO to Domain Model
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                    FileName = imageUploadRequestDto.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription
                };

                imageDomainModel = await imageRepository.UploadAsync(imageDomainModel);

                var imageDto = new ImageDto
                {
                    Id = imageDomainModel.Id,
                    File = imageDomainModel.File,
                    FileName = imageDomainModel.FileName,
                    FileDescription = imageDomainModel.FileDescription,
                    FileExtension = imageDomainModel.FileExtension,
                    FileSizeInBytes = imageDomainModel.FileSizeInBytes,
                    FilePath = imageDomainModel.FilePath
                };

                return Ok(imageDto);
            }

            return BadRequest(ModelState);
        }

        //Validate against extension and file size
        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension.");
            }

            //10mb in bytes is 10485760
            if (imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller file.");
            }
        }
    }
}
