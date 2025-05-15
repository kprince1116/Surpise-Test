using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repository;

public class BlogRepo : IBlogRepo
{
    private readonly TestingContext _db;

    public BlogRepo(TestingContext db)
    {
        _db = db;
    }

   

    public async Task<List<Blog>> GetBlogData()
    {
        try
        {
             var blog =  await _db.blogs.Include(u=>u.Comments).ThenInclude(u=>u.User).Where(u=>u.IsDelete == false).ToListAsync();
             return blog;
        }
        catch (Exception e)
        {
            
            return null;
        }
    }

     public async Task AddBlog(Blog blog)
    {
        _db.blogs.Add(blog);
        await _db.SaveChangesAsync();
    }

    public async Task AddComment(Comment comment)
    {
        try
        {
             _db.comments.Add(comment);
             await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            
            throw;
        }
       
    }

    public async Task<Blog> GetBlog(int id)
    {
        var blog = await _db.blogs.FirstOrDefaultAsync(u=>u.BlogId == id);
        return blog;
    }

      public async  Task UpdateBlog(Blog blog)
    {
        _db.blogs.Update(blog);
        await _db.SaveChangesAsync();
    }


}
