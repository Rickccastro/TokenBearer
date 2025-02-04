using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiTokenBearer.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login(string username,string password)
    {
        if(IsValidUser(username, password))
        {
            var claimsPrincipal = new ClaimsPrincipal
                (
                    new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) },
                    BearerTokenDefaults.AuthenticationScheme
                ));

            return SignIn(claimsPrincipal);
        }

        return Unauthorized();  
    }


    [HttpGet("/user")]
    public IActionResult GetUser()
    {
        var user = User;
        if (user?.Identity?.IsAuthenticated?? false)
        {
            return Ok($"Bem vindo{user.Identity.Name}");
        }
        
        return Unauthorized();
    }

    private bool IsValidUser(string username, string password)
    {
        return username == "rickccastro" && password == "123456";
    }
}
