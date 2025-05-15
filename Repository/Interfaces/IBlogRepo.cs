using Repository.Models;

namespace Repository.Interfaces;

public interface IBlogRepo
{
    Task<List<Blog>> GetBlogData();
    Task AddBlog(Blog blog);
    Task AddComment(Comment comment);
    Task UpdateBlog(Blog blog);
    Task<Blog> GetBlog(int id);
}
