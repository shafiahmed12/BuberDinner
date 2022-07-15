using BuberDinner.Api.Filters;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
// [ErrorHandlingFilterAttribute]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult<AuthenticationResponse> Register(RegisterRequest request)
    {
        var result = _authService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        var res = new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);
        return Ok(res);
    }

    [HttpPost("login")]
    public ActionResult Login(LoginRequest request)
    {
        var result = _authService.Login(request.Email, request.Password);
        var res = new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);
        return Ok(res);
    }

}