using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using shopDemo.application.Services.implementation;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.DTOs.Contacts;
using ShopDemo.Data.Entity.Site;
using ShopDemo.Models;
using ShopDemo.PresentationExtensions;
using System.Diagnostics;

namespace ShopDemo.Controllers
{
    public class HomeController : SiteBaseController
    {
		
		private readonly IContactService _contactService;
		private readonly ICaptchaValidator _captchaValidator;
		private readonly ISiteService _siteService;

        public HomeController(IContactService contactService, ICaptchaValidator captchaValidator, ISiteService siteService)
        {
            _contactService = contactService;
            _captchaValidator = captchaValidator;
            _siteService = siteService;
        }

        public async Task<IActionResult> Index()
		{
			var baners = await _siteService.GetSiteBannersByPlacement(new List<BannerPlacement>
			{
				BannerPlacement.Home_1,
				BannerPlacement.Home_2,
				BannerPlacement.Home_3,
			});
			return View();
		}

		[HttpGet("contact-us")]
		public IActionResult ContactUs()
		{
			return View();
		}



		[HttpPost("contact-us"), ValidateAntiForgeryToken]
		public async Task<IActionResult> ContactUs(CreateContactUsDTO contact)
		{
			if (!await _captchaValidator.IsCaptchaPassedAsync(contact.Captcha))
			{
				TempData[ErrorMessage] = "کد کپچای شما تایید نشد";
				return View(contact);
			}

			if (ModelState.IsValid)
			{
				var ip = HttpContext.GetUserIp();
				await _contactService.CreateContactUs(contact, HttpContext.GetUserIp(), User.GetUserId());
				TempData[SuccessMessage] = "پیام شما با موفقیت ارسال شد";
				return RedirectToAction("ContactUs");
			}

			return View(contact);
		}

	}
}