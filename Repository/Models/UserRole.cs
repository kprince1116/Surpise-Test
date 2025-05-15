namespace Repository.Models;

public class UserRole
{
    public int RoleId {get; set;}
    public string RoleName {get; set;}

    public bool Isdeleted {get; set;}
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
