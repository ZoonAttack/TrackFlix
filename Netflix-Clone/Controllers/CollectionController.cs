using Microsoft.AspNetCore.Mvc;

namespace Netflix_Clone.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
