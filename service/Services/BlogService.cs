using Repository.Interfaces;
using Repository.Models;
using Repository.viewmodel;
using service.Interfaces;

namespace service.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepo _blogRepo;
    private readonly ITokenService _tokenService;


    public BlogService(IBlogRepo blogRepo , ITokenService tokenService)
    {
        _blogRepo = blogRepo;
        _tokenService = tokenService;
    }

    public async Task<BlogViewModel> GetBlogData(string searchKey)
    {
        try
        {       
            var blogs = await _blogRepo.GetBlogData();

            if(!string.IsNullOrEmpty(searchKey))
            {
                 var lowerSearchQuery = searchKey.Trim().ToLower();
              blogs = blogs.Where(o =>
                (!string.IsNullOrEmpty(o.BlogTag) && o.BlogTag.ToLower().Contains(lowerSearchQuery)) ||
                (!string.IsNullOrEmpty(o.BlogTitle) && o.BlogTitle.ToLower().Contains(lowerSearchQuery)) 
            ).ToList();
            }

        var viewmodel = blogs.Select(u=> new BlogListViewModel
        {
            BlogId = u.BlogId,
            UserId = u.UserId,
            BlogTitle = u.BlogTitle,
            BlogContent = u.BlogContent,
            BlogTags = u.BlogTag,
            CreatedAt = u.CreatedAt,
            Comments = u.Comments.Select(u=> new CommentViewModel
            {
                CommentId = u.CommentId,
                CommentText = u.CommentContent,
                UserName = u.User.UserName
            }).OrderByDescending(u=>u.CommentId).ToList()
        }).ToList();

        return new BlogViewModel
        {
            Blogs = viewmodel
        };
        }
        catch (Exception e)
        {
           return null;
        }
        
    }

    public async Task<bool> AddBlog(BlogViewModel model)
    {
        if(model==null)
        {
            return false;
        }

        var blog = new Blog
        {
            BlogTitle = model.AddBlog.BlogTitle,
            BlogTag = model.AddBlog.BlogTags,
            BlogContent = model.AddBlog.BlogContent,
            UserId = 3
        };
        await _blogRepo.AddBlog(blog);
        return true;
    }
    public async  Task<bool> AddComment(BlogViewModel model)
    {
        
        if(model==null)
        {
            return false;
        }

        var comment = new Comment
        {
            BlogId = model.AddComment.BlogId,
            UserId = model.AddComment.UserId,
            CommentContent = model.AddComment.CommentContent
        };
        await _blogRepo.AddComment(comment);
        return true;
    }

    public async Task<bool> DeleteBlog(int id)
    {
        var blog = await _blogRepo.GetBlog(id);
        if(blog == null)
        {
            return false;
        }
        blog.IsDelete = true;
        await _blogRepo.UpdateBlog(blog);
        return true;
    }

    public async Task<BlogViewModel> EditBlog(int id)
    {
        var blog = await _blogRepo.GetBlog(id);
        if(blog == null)
        {
            return null;
        }

        var viewmodel = new AddBlogViewModel
        {
            BlogId = blog.BlogId,
            BlogContent = blog.BlogContent,
            BlogTags = blog.BlogTag,
            BlogTitle = blog.BlogTitle
        };
        return new BlogViewModel
        {
            AddBlog = viewmodel
        };
    }

    public async Task<bool> EditBlog(BlogViewModel model)
    {
        var blog = await _blogRepo.GetBlog(model.AddBlog.BlogId);
        if(blog == null)
        {
            return false;
        }
        blog.BlogContent = model.AddBlog.BlogContent;
        blog.BlogTag = model.AddBlog.BlogTags;
        blog.BlogTitle = model.AddBlog.BlogTitle;

        await _blogRepo.UpdateBlog(blog);
        return true;

    }

}
