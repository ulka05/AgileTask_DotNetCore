using AgileInfoTask.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AgileInfoTask.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserModel AuthenticateUser(UserModel user)
        {
            // Implement logic to validate username and password against your data store (e.g., database)
            // This example uses a simple in-memory validation for demonstration purposes
            if (user.Username == "admin" && user.Password == "12345")
            {
                return new UserModel { Username = user.Username, Role = UserRole.Admin };
            }
            else if (user.Username == "user" && user.Password == "user123")
            {
                return new UserModel { Username = user.Username, Role = UserRole.User };
            }
            return user;
        }

        public string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                              _configuration["Jwt:Audience"],
                                              null,
                                              expires: DateTime.Now.AddMinutes(5),
                                              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}