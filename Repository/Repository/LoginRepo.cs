using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repository;

public class LoginRepo : ILoginRepo
{
    private readonly TestingContext _db;

    public LoginRepo(TestingContext db)
    {
        _db = db;
    }

    public async Task<User> GetUserByEmail(string Email)
    {
        try
        {
            User user = await _db.users.Include(u => u.UserroleNavigation).FirstOrDefaultAsync(u => u.Email == Email && u.Isdeleted == false);
            return user;
        }
        catch (Exception e)
        {
            return null;
        }

    }

    public async Task<bool> GetUser(string Email)
    {
        try
        {
            var email = await _db.users.AnyAsync(u => u.Email == Email);
            if (email)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {

            throw;
        }

    }

    public async Task UpdateUser(User user)
    {
        try
        {
            _db.users.Add(user);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {

            throw;
        }

    }


}
