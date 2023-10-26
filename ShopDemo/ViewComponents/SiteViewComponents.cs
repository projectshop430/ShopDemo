using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shopDemo.application.Services.implementation;
using shopDemo.application.Services.Interface;

namespace ShopDemo.ViewComponents
{
    #region site header

    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;
        private readonly IUserService _userService;

        public SiteHeaderViewComponent(ISiteService siteService, IUserService userService)
        {
            _siteService = siteService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.siteSetting = await _siteService.GetDefaultSiteSetting();
            ViewBag.user = await _userService.GetUserByMobile(User.Identity.Name);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.user = await _userService.GetUserByMobile(User.Identity.Name);
            }
            return View("SiteHeader");
        }
    }

    #endregion

    #region site footer

    public class SiteFooterViewComponent : ViewComponent
    {
		private readonly ISiteService _siteService;

		public SiteFooterViewComponent(ISiteService siteService)
		{
			_siteService = siteService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
			ViewBag.siteSetting = await _siteService.GetDefaultSiteSetting();
			return View("SiteFooter");
        }
    }

	#endregion

	#region slider
	public class HomeSliderViewComponent : ViewComponent
	{
		private readonly ISiteService _siteService;

		public HomeSliderViewComponent(ISiteService siteService)
		{
			_siteService = siteService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var sliders = await _siteService.GetAllActiveSliders();
			return View("HomeSlider", sliders);
		}
	}

#endregion
}
