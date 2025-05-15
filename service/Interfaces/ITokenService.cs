using Repository.Models;

namespace service.Interfaces;

public interface ITokenService
{
   public string GetRoleFromToken(string token);

   public string GetEmailFromToken(string token);

}
