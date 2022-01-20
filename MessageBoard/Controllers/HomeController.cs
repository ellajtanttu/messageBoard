using Microsoft.AspNetCore.Mvc;

namespace MessageBoard.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}