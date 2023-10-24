using Microsoft.AspNetCore.Mvc;

namespace ShopDemo.Areas.User.Controllers
{
	public class AccountController : UserBaseController
	{
		#region constructor



		#endregion

		#region user dashboard

		[HttpGet("")]
		public async Task<IActionResult> Dashboard()
		{
			return View();
		}

		#endregion
	}
}
