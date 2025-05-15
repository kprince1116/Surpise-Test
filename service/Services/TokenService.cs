using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Repository.Models;
using service.Interfaces;

namespace service.Services;

public class TokenService : ITokenService
{
      private readonly TestingContext _db;

    public TokenService(TestingContext db)
    {
        _db = db;
    }

    public string GetRoleFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(token);

        var Role =  jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

        return Role;
    }

     public string GetEmailFromToken(string token)
    {

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(token);

        if (jsonToken != null)
        {
            var email = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            if (email != null)
            {
                return email;
            }
        }

        return null;
    }

}
