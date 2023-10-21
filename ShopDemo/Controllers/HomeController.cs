using Microsoft.AspNetCore.Mvc;
using shopDemo.application.Services.Interface;
using ShopDemo.Models;
using System.Diagnostics;

namespace ShopDemo.Controllers
{
    public class HomeController : SiteBaseController
    {
		
		public IActionResult Index()
		{
			
			return View();
		}

	}
}