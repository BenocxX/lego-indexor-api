using System.Security.Claims;
using lego_indexor_api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SecurityController : Controller
{
    private readonly IConnectionService _connectionService;
    protected int UserId;
    
    public SecurityController(IConnectionService connectionService)
    {
        _connectionService = connectionService;
    }
    
    protected bool Authenticate(string? token)
    {
        var userId = _connectionService.Login(token);
        
        if (userId != null)
            UserId = userId.Value;

        return userId != null;
    }
}