using Microsoft.AspNetCore.Mvc;
using ShopDemo.Data.DTOs.Account;

namespace ShopDemo.Areas.User.Controllers
{
	public class AccountController : UserBaseController
	{
        #region constructor



        #endregion

        #region user dashboard

        [HttpGet("change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }


        [HttpPost("change-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO passwordDto)
        {
            return View();
        }


        #endregion
    }

}
