namespace Repository.Models;

public class Comment
{
    public int BlogId {get; set;}
    public int CommentId {get; set;}
    public string CommentContent {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public int UserId {get; set;}
    public bool IsDelete {get; set;}
    public virtual Blog? Blog {get ; set;}
    public virtual User? User {get ; set;}

}
