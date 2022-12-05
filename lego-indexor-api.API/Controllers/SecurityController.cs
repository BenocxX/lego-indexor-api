using System.Security.Claims;
using lego_indexor_api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SecurityController : Controller
{
    protected readonly IConnectionService ConnectionService;
    protected int UserId;
    
    public SecurityController(IConnectionService connectionService)
    {
        ConnectionService = connectionService;
    }
    
    protected bool Authenticate(string? token)
    {
        var userId = ConnectionService.Login(token);
        
        if (userId != null)
            UserId = userId.Value;

        return userId != null;
    }
}