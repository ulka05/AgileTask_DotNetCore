using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgileInfoTask.Model
{
    public class UserModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        User,
        Admin
    }
}
