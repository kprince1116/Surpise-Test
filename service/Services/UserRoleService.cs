using Microsoft.AspNetCore.Http;
using Repository.Models;
using service.Interfaces;

namespace service.Services;

public class UserRoleService : IUserRoleService
{
      private readonly TestingContext _db;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor; 

        public UserRoleService(ITokenService tokenService, IHttpContextAccessor httpContextAccessor , TestingContext db)
        {
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor; 
            _db=db;
        }

        public async Task<bool> IsUserInRoleAsync(string RoleName)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwtToken"];
            var role =  _tokenService.GetRoleFromToken(token);

            if(role == RoleName)
            {
                return true;
            }
            return false;

        }
}
