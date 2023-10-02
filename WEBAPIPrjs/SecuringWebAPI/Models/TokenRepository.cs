using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecuringWebAPI.Models
{
    public class TokenRepository : ITokenRepository
    {
        //This should come from DAL layer of Users database using EF Core
        //Dictionary<string, string> UsersRecords = new Dictionary<string, string>
        //{
        //    { "admin","password"},
        //    { "guest","password2"},
        //    {"ramnath","abcd" }
        //};

        private readonly IConfiguration _configuration;
        private readonly IUsersADO _usersADO;
        public TokenRepository(IConfiguration configuration, IUsersADO usersADO)
        {
            _configuration = configuration;
            _usersADO = usersADO;
        }
        public Tokens Authenticate(Users users)
        {           

            if (!_usersADO.IsValidUser(users))
            {
                return null;
            }
            else
            {
                //else return valid token
                //Generate JSON Web Token
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //Issuer = _configuration["JWT:Issuer"],
                    Audience = _configuration["JWT:Audience"],
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new Tokens { Token = tokenHandler.WriteToken(token) };
            }
        }
    }
}

