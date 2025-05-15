namespace Repository.Models;

public class User
{
    public int UserId {get; set;}
    public string UserName {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public int UserRoleId {get; set;}
    public bool Isdeleted { get; set; }
    public virtual UserRole? UserroleNavigation { get; set; }
    public ICollection<Blog> Blogs {get; set;} = new List<Blog>();
    public ICollection<Comment> Comments {get; set;} = new List<Comment>();


}
