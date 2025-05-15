using System.ComponentModel.DataAnnotations;

namespace Repository.viewmodel;

public class BlogViewModel
{
    public List<BlogListViewModel> Blogs { get; set; }
    public AddBlogViewModel AddBlog {get; set;} 
    public AddCommentViewModel AddComment {get; set;} 
 
}
 
public class BlogListViewModel
{
    public int BlogId { get; set; }

    public int UserId {get; set;}
    public string BlogTitle { get; set; }
    public string BlogContent { get; set; }
    public string BlogTags { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public List<CommentViewModel> Comments { get; set; }
}
 
public class CommentViewModel
{
    public int CommentId { get; set; }
    public string CommentText { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
} 

public class AddBlogViewModel
{
    public int BlogId { get; set; }

    [Required(ErrorMessage ="BlogTitle is Required")]
    public string BlogTitle { get; set; }

    [Required(ErrorMessage ="BlogContent is Required")]
    public string BlogContent { get; set; }
        
    [Required(ErrorMessage ="BlogTags is Required")]
    public string BlogTags { get; set; }
    public DateTime CreatedAt { get; set; }   
}

public class AddCommentViewModel
{
    public int UserId {get; set;}
    public int BlogId {get; set;}
    
    [Required(ErrorMessage ="Content is Required")]
    public string CommentContent {get; set;}
}