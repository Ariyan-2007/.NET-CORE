using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{

    private readonly IMyService _myService;

    public HomeController(IMyService myService)
    {
        _myService = myService;
    }

    public IActionResult Index()
    {
        var greeting = _myService.GetGreeting();
        return View("Index", greeting);
    }
}