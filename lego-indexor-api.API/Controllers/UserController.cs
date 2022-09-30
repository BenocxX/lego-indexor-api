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
    private readonly IMapper<User, UserRequest> _userMapper;
    private readonly IAuthenticationService _authenticationService;
    
    public UserController(
        IMapper<User, UserRequest> userMapper, 
        IAuthenticationService authenticationService)
    {
        _userMapper = userMapper;
        _authenticationService = authenticationService;
    }
    
    [HttpPost("/login")]
    public ActionResult<User> Login(UserRequest request)
    {
        var user = _userMapper.RequestToModel(request);
        var loginUser = _authenticationService.Login(user);
        
        if (loginUser == null)
            return Ok("Invalid login details");
        
        return Ok(loginUser);
    }
    
    [HttpPost("/signup")]
    public ActionResult<User> Signup(UserRequest request)
    {
        if (request.Password != request.ConfirmPassword)
            return Ok("Password does not match its confirmation.");
        
        var user = _userMapper.RequestToModel(request);
        var (newUser, details) = _authenticationService.Signup(user);
        
        return Ok(details);
    }
}