using Microsoft.AspNetCore.Http;
using Repository.viewmodel;

namespace Service.Interfaces;

public interface IAuthservice
{
    public Task<string> AuthenticateUser(LoginViemodel model);
    void setJwtToken(HttpResponse Response,string token );
    void SetCookie(HttpResponse Response,string Email);
     Task<bool> AddNewuser(AddNewUserviewmodel model);
}
