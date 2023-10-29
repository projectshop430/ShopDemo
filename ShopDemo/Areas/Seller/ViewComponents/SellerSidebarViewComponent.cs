using Microsoft.AspNetCore.Mvc;

namespace ShopDemo.Areas.Seller.ViewComponents
{
    public class SellerSidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SellerSidebar");
        }
    }
}
