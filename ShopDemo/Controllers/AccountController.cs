using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using shopDemo.application.Services.implementation;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.DTOs.Account;
using System.Security.Claims;

namespace ShopDemo.Controllers
{
    public class AccountController : SiteBaseController
    {
        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;

      
		public AccountController(IUserService userService, ICaptchaValidator captchaValidator)
		{
			_userService = userService;
			_captchaValidator = captchaValidator;
		}

		[HttpGet("register")]
        public IActionResult Register()
        {
			if (User.Identity.IsAuthenticated)
			{
				return Redirect("/");
			}
			return View();
        }
        [HttpPost("register"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
			if (!await _captchaValidator.IsCaptchaPassedAsync(registerUserDTO.Captcha))
			{
				TempData[ErrorMessage] = "کد امنیتی صحیح وارد نکردید";
				return View();
			}
			if (ModelState.IsValid)
            {
                var res = await _userService.RegisterUser(registerUserDTO);
                switch(res)
                {
                    case RegisterUserResulte.MobileExit:
                        TempData[ErrorMessage] = "تلفن همراه وارد شده تکراری می باشد";
                        ModelState.AddModelError("Mobile", "تلفن همراه وارد شده تکراری می باشد");
                        break;
                    case RegisterUserResulte.Success:
                        TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد";
                        TempData[InfoMessage] = "کد تلفن همراه برای شما ارسال می گردد";
                        return RedirectToAction("Login");
                        break;
                    case RegisterUserResulte.Error:
                        TempData[ErrorMessage] = "با خطا مواجه شدید";
                        ModelState.AddModelError("", "با خطا مواجه شدید");
                        break;
                }
            }
            return View(registerUserDTO);
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }
		[HttpPost("Login"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginUserDTO Login)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(Login.Captcha))
            {
                TempData[ErrorMessage] = "کد امنیتی صحیح وارد نکردید";
                return View();
            }

            if(ModelState.IsValid)
            {
                var res = await _userService.GetUserForlogin(Login);
                switch (res)
                {
                    case LoginUserDTO.LoginUserResulte.Notfound:
                        TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد";

                        break;
					case LoginUserDTO.LoginUserResulte.NotActivated:
						TempData[WarningMessage] = "حساب کاربری شما فعال نشد";

						break;
					case LoginUserDTO.LoginUserResulte.Success:
						TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";

                        var user = await _userService.GetUserByMobile(Login.Mobile);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.Mobile),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        };
                        var indentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(indentity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = Login.RememberMe
                        };
                        await HttpContext.SignInAsync(principal, properties);



                        return Redirect("/");
						
				}
            }
            return View();
        }
        [HttpGet("log-out")]
		public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


	}
}
