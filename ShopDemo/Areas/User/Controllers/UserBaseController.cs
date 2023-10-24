using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopDemo.Areas.User.Controllers
{
	[Authorize]
	[Area("User")]
	[Route("user")]
	public class UserBaseController : Controller
	{
		
		
	}
}
