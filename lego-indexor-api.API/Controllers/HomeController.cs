using lego_indexor_api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IAccountBroker _accountBroker;

    public HomeController(IAccountBroker accountBroker)
    {
        _accountBroker = accountBroker;
    }
    
    [HttpGet]
    public IEnumerable<string> Index()
    {
        return new[] { "Hello", "World" };
    }
    
    [HttpGet("users")]
    public IEnumerable<string> GetUsers()
    {
        return new[] { "Mathis", "Antoine", "Edouard" };
    }
    
    [HttpGet("accounts")]
    public ActionResult<int> GetAccounts()
    {
        return Ok(_accountBroker.GetAccounts().First().UserId);
    }
}