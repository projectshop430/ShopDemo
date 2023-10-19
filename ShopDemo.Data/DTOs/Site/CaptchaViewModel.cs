using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopDemo.Data.DTOs.Site
{
	public class CaptchaViewModel
	{
		
		[Required(ErrorMessage = " لطفا {0} را وارد کنید")]
		public string Captcha { get; set; }
	}
}
