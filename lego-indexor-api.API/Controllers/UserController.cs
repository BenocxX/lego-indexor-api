using lego_indexor_api.API.Responses;
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
    public ActionResult<AuthenticationResponse> Login(AuthenticationLoginRequest request)
    {
        var tempUser = _userAuthenticationMapper.RequestToModel(request);
        var (user, details) = _authenticationService.Login(tempUser);
        
        if (user == null)
            return Ok(new AuthenticationResponse(false, details));
        
        return Ok(new AuthenticationResponse(true, userId: user.Id));
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

        return Ok(new AuthenticationResponse(true, userId: user.Id));
    }
}