using System.ComponentModel.DataAnnotations;

namespace coreAPI.Models.DTO.Auth
{
    public class RegisterRequestDto
    {
        [DataType(DataType.EmailAddress)]
        public required string Username { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public required string[] Roles { get; set; }
    }
}