using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.viewmodel;
using service.Interfaces;

namespace Presentation.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [Authorize(Roles = "User,Admin")]
    public IActionResult Blog()
    {
        return View();
    }

    public async Task<IActionResult> GetBlogData(string searchKey)
    {
        var blogs = await _blogService.GetBlogData(searchKey);
        return PartialView("_blogdata", blogs);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddBlog(BlogViewModel model)
    {
        var isAdded = await _blogService.AddBlog(model);
        if (isAdded)
        {
            TempData["BlogAdded"] = true;
            return RedirectToAction("Blog", "Blog");
        }
        else
        {
            return RedirectToAction("Blog", "Blog");
        }
    }

    public async Task<IActionResult> EditBlog(int id)
    {
        var result = await _blogService.EditBlog(id);
        return PartialView("_editblogdata", result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> EditBlog(BlogViewModel model)
    {
        var result = await _blogService.EditBlog(model);
          if (result)
        {
            TempData["BlogEdited"] = true;
            return Json(new { success = true });
        }
        else
        {
            return Json(new { success = false });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        var isDelete = await _blogService.DeleteBlog(id);
        if (isDelete)
        {
            TempData["BlogDeleted"] = true;
            return RedirectToAction("Blog", "Blog");
        }
        else
        {
            return RedirectToAction("Blog", "Blog");
        }
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> AddComment(BlogViewModel model)
    {
        var isAdded = await _blogService.AddComment(model);
        if (isAdded)
        {
            TempData["CommentAdded"] = true;
            return RedirectToAction("Blog", "Blog");
        }
        else
        {
            return RedirectToAction("Blog", "Blog");
        }
    }

}
