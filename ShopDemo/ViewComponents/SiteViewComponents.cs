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

		public SiteHeaderViewComponent(ISiteService siteService)
		{
			_siteService = siteService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.siteSetting = await _siteService.GetDefaultSiteSetting();
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
