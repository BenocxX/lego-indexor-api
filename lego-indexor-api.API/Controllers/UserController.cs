using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs;
using lego_indexor_api.Core.Models.Entities;
using lego_indexor_api.Core.Models.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly IMapper<User, AuthenticationLoginRequest> _userMapper;
    private readonly IAuthenticationService _authenticationService;
    
    public UserController(
        IMapper<User, AuthenticationLoginRequest> userMapper, 
        IAuthenticationService authenticationService)
    {
        _userMapper = userMapper;
        _authenticationService = authenticationService;
    }
    
    [HttpPost("/login")]
    public ActionResult<User> Login(AuthenticationLoginRequest request)
    {
        var loginUser = _authenticationService.Login(request.Username, request.Password);
        
        if (loginUser == null)
            return Ok("Invalid login details");
        
        return Ok(loginUser);
    }
    
    [HttpPost("/signup")]
    public ActionResult<User> Signup(AuthenticationSignupRequest request)
    {
        if (request.Password != request.ConfirmPassword)
            return Ok("Password does not match its confirmation.");

        var (newUser, details) = _authenticationService.Signup(request.Username, request.Password);
        
        return Ok(details);
    }
}