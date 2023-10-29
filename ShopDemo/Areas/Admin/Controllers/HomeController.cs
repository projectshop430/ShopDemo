using Microsoft.AspNetCore.Mvc;

namespace ShopDemo.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region index

        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
