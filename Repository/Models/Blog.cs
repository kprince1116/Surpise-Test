namespace Repository.Models;

public class Blog
{
    public int BlogId {get; set;}
    public string BlogTitle {get; set;}
    public string BlogContent {get; set;}
    public string BlogTag {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public bool IsDelete {get; set;}
    public int UserId {get; set;}
    public virtual User? User {get ; set;}
    public ICollection<Comment> Comments {get; set;} = new List<Comment>();

}
