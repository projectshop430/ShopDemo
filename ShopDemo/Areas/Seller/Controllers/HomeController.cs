using Microsoft.AspNetCore.Mvc;

namespace ShopDemo.Areas.Seller.Controllers
{
    public class HomeController : SellerBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
