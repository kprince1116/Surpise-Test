using System.ComponentModel.DataAnnotations;

namespace Repository.viewmodel;

public class LoginViemodel
{
    [Required(ErrorMessage = "Email Field is Required")]
    public string Email { get; set; } 

    [Required(ErrorMessage = "Password Field is Required")]
    public string Password { get; set; }
    public bool RememberMe {get ; set;} 
    public string Role { get; set; } = null!;
}
