namespace service.Interfaces;

public interface IUserRoleService
{
    Task<bool> IsUserInRoleAsync(string RoleName);
}
