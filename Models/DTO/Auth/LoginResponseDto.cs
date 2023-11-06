namespace coreAPI.Models.DTO.Auth
{
    public class LoginResponseDto
    {
        public string JwtToken { get; }

        public LoginResponseDto(string jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
