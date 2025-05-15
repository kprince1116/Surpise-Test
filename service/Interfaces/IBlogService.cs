using Repository.viewmodel;

namespace service.Interfaces;

public interface IBlogService
{
    Task<BlogViewModel> GetBlogData(string searchKey);
    Task<bool> AddBlog(BlogViewModel model);
    Task<bool> AddComment(BlogViewModel model);
    Task<bool> DeleteBlog(int id);
    Task<BlogViewModel> EditBlog(int id);
    Task<bool> EditBlog(BlogViewModel model);
}
