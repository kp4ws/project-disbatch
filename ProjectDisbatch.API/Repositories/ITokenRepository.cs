using Microsoft.AspNetCore.Identity;

namespace ProjectDisbatch.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
