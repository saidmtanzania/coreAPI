using Microsoft.AspNetCore.Identity;

namespace coreAPI.Repositories.Tokens
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}