using Microsoft.AspNetCore.Identity;

namespace NXWalks.API.Repositories
{
    public interface ITokenRepository
    {
      string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
