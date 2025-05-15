using Microsoft.AspNetCore.Mvc;
using Repository.viewmodel;
using Service.Interfaces;


namespace Presentation.Controllers;

public class LoginController : Controller
{
     private readonly IAuthservice _authService;

      public LoginController(IAuthservice authService)
    {
        _authService = authService;
    }
      public IActionResult Login()
    {
        if(!string.IsNullOrEmpty(Request.Cookies["jwtToken"]))
        {
             return RedirectToAction("Blog","Blog");
        }
        else{
        return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViemodel model)
    {
        try
        {
            var result = await _authService.AuthenticateUser(model);
            _authService.setJwtToken(Response,result);
              if (model.RememberMe)
            {
                _authService.SetCookie(Response, model.Email);
            }
            TempData["LoginSuccess"] = true;
            return RedirectToAction("Blog","Blog");
        }
        catch (Exception e)
        {
            ViewBag.ErrorMessage = e.Message;
            return View();
        }
    }

    public IActionResult NewUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddNewuser(AddNewUserviewmodel model)
    {
        var isAdded = await _authService.AddNewuser(model);
        if(isAdded)
        {
            TempData["Added"] = true;
            return RedirectToAction("Login","Login");
        }
        else{
             TempData["AddeFail"] = true;
            return RedirectToAction("NewUser","Login");
        }
    }


      public IActionResult Logout()
    {
        Response.Cookies.Delete("jwtToken");
        Response.Cookies.Delete("Email");
        TempData["LogOutSuccess"] = true;
        return RedirectToAction("Login", "Login");
    }

  

}
