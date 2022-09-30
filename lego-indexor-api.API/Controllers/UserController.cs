using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs.AuthenticationRequests;
using lego_indexor_api.Core.Models.Entities;
using lego_indexor_api.Core.Models.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly IMapper<User, AuthenticationRequest> _userAuthenticationMapper;
    private readonly IAuthenticationService _authenticationService;
    
    public UserController(
        IMapper<User, AuthenticationRequest> userAuthenticationMapper, 
        IAuthenticationService authenticationService)
    {
        _userAuthenticationMapper = userAuthenticationMapper;
        _authenticationService = authenticationService;
    }
    
    [HttpPost("/login")]
    public ActionResult<User> Login(AuthenticationLoginRequest request)
    {
        var tempUser = _userAuthenticationMapper.RequestToModel(request);
        var loginUser = _authenticationService.Login(tempUser);
        
        if (loginUser == null)
            return Ok("Invalid login details");
        
        return Ok(loginUser);
    }
    
    [HttpPost("/signup")]
    public ActionResult<User> Signup(AuthenticationSignupRequest request)
    {
        if (request.Password != request.ConfirmPassword)
            return Ok("Password does not match its confirmation.");

        var tempUser = _userAuthenticationMapper.RequestToModel(request);
        var (newUser, details) = _authenticationService.Signup(tempUser);
        
        return Ok(details);
    }
}