using Microsoft.AspNetCore.Identity;

namespace AnsarTask.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user, string role);
    }
}
