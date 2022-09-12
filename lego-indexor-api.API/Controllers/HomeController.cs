using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    public IEnumerable<string> Index()
    {
        return new[] { "Hello", "World" };
    }
}