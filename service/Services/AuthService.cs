using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using Repository.Models;
using Repository.viewmodel;
using Service.Interfaces;

namespace Service.Services;

public class AuthService : IAuthservice
{
    private readonly ILoginRepo _loginrepo;

    private readonly IConfiguration _configuration;

    public AuthService(ILoginRepo loginRepo, IConfiguration configuration)
    {
        _loginrepo = loginRepo;
        _configuration = configuration;
    }

    public async Task<string> AuthenticateUser(LoginViemodel model)
    {    
        var existinguser = await _loginrepo.GetUserByEmail(model.Email);

        if (existinguser == null)
        {
            throw new Exception("User does not exists");
        }

        if (model.Password != existinguser.Password)
        {
            throw new Exception("Invalid password.");
        }

        if (existinguser.UserroleNavigation == null)
        {
            throw new Exception("User role not found.");
        }

        return GenerateJwtToken(existinguser.Email, existinguser.UserroleNavigation.RoleName);
           
    }


    private string GenerateJwtToken(string email, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

   
    public void setJwtToken(HttpResponse Response, string token)
    {
         Response.Cookies.Append("jwtToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }

    public void SetCookie(HttpResponse Response,string Email)
    {
         Response.Cookies.Append("Email", Email, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(30),
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict
            });
    }
   
   public async Task<bool> AddNewuser(AddNewUserviewmodel model)
   {
        var exists = await _loginrepo.GetUser(model.Email);

        if(exists)
        {
            return false;
        }

        if(model == null)
        {
            return false;
        }

        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            Password = model.Password,
            UserRoleId = 2,
        };

        await _loginrepo.UpdateUser(user);
        return true;
   }
}
