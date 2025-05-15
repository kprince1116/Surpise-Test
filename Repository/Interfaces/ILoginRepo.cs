using Repository.Models;

namespace Repository.Interfaces;

public interface ILoginRepo
{
    public Task<User> GetUserByEmail(string Email);

    public Task<bool> GetUser(string Email);
    public Task UpdateUser(User user);
}
