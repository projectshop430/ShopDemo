using Microsoft.AspNetCore.Mvc;

namespace ShopDemo.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
