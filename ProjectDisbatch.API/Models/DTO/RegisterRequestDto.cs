using System.ComponentModel.DataAnnotations;

namespace ProjectDisbatch.API.Models.DTO
{
    public class RegisterRequestDto
    {
        //username is email in our app
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
