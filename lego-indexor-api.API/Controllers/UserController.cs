using lego_indexor_api.Core.Interfaces.Brokers;
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
    private readonly IUserBroker _userBroker;
    private readonly IMapper<User, UserRequest> _userMapper;
    private readonly IAuthenticationService _authenticationService;
    
    public UserController(
        IUserBroker userBroker, 
        IMapper<User, UserRequest> userMapper, 
        IAuthenticationService authenticationService)
    {
        _userBroker = userBroker;
        _userMapper = userMapper;
        _authenticationService = authenticationService;
    }
    
    [HttpGet]
    public ActionResult<User> Index()
    {
        return Ok(_userBroker.GetUsers().First());
    }

    [HttpPost("/login")]
    public ActionResult<bool> Login(UserRequest request)
    {
        var user = _userMapper.RequestToModel(request);
        var loginUser = _authenticationService.Login(user);
        return Ok(loginUser != null);
    }
}