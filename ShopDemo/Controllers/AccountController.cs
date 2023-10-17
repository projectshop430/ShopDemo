using Microsoft.AspNetCore.Mvc;
using shopDemo.application.Services.implementation;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.DTOs;

namespace ShopDemo.Controllers
{
    public class AccountController : SiteBaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد";
            TempData[ErrorMessage] = "ثبت نام شما با موفقیت انجام شد";
            TempData[InfoMessage] = "ثبت نام شما با موفقیت انجام شد";
            TempData[WarningMessage] = "ثبت نام شما با موفقیت انجام شد";
            return View();
        }
        [HttpPost("register"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {

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
       
        public IActionResult Login()
        {
            return View();
        }
    }
}
