using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDisbatch.API.Models.DTO
{
    public class ImageDto
    {
        public Guid Id { get; set; }

        [NotMapped] // We don't store this file in database
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
