using Microsoft.AspNetCore.Mvc;

namespace ShopDemo.Areas.User.ViewComponents
{
    public class UserSidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("UserSidebar");
        }
    }
}
