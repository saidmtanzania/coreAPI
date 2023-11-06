using System.ComponentModel.DataAnnotations;

namespace coreAPI.Models.DTO.Auth
{
    public class LoginRequestDto
    {
        [DataType(DataType.EmailAddress)]
        public required string Username { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }


    }
}