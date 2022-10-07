using lego_indexor_api.API.Responses;
using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs.AuthenticationRequests;
using lego_indexor_api.Core.Models.Entities;
using lego_indexor_api.Core.Models.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IMapper<User, AuthenticationRequest> _userAuthenticationMapper;
    private readonly IAuthenticationService _authenticationService;
    private readonly IConnectionService _connectionService;
    
    public AuthenticationController(
        IMapper<User, AuthenticationRequest> userAuthenticationMapper, 
        IAuthenticationService authenticationService,
        IConnectionService connectionService)
    {
        _userAuthenticationMapper = userAuthenticationMapper;
        _authenticationService = authenticationService;
        _connectionService = connectionService;
    }
    
    [HttpPost("/login")]
    public ActionResult<AuthenticationResponse> Login(AuthenticationLoginRequest request)
    {
        var tempUser = _userAuthenticationMapper.RequestToModel(request);
        var (user, details) = _authenticationService.Login(tempUser);
        
        if (user == null)
            return Ok(new AuthenticationResponse(false, details));

        var token = _connectionService.CreateNewConnection(user);
        
        return Ok(new AuthenticationResponse(true, userId: user.Id, token: token));
    }
    
    [HttpPost("/login/token")]
    public ActionResult<AuthenticationResponse> LoginToken(string token)
    {
        var userId = _connectionService.Login(token);
        
        if (userId == null)
            return Ok(new AuthenticationResponse(false, "Invalid token."));
        
        return Ok(new AuthenticationResponse(true, userId: userId));
    }
    
    [HttpPost("/signup")]
    public ActionResult<AuthenticationResponse> Signup(AuthenticationSignupRequest request)
    {
        if (request.Password != request.ConfirmPassword)
            return Ok(new AuthenticationResponse(false, "Password does not match its confirmation."));

        var tempUser = _userAuthenticationMapper.RequestToModel(request);
        var (user, details) = _authenticationService.Signup(tempUser);

        if (user == null)
            return Ok(new AuthenticationResponse(false, details));
        
        var token = _connectionService.CreateNewConnection(user);

        return Ok(new AuthenticationResponse(true, userId: user.Id, token: token));
    }
}