using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopDemo.Data.DTOs
{
	public class RegisterUserDTO
	{
		[Display(Name = "موبایل")]
		[Required(ErrorMessage = " لطفا {0} را وارد کنید")]
		[MaxLength(11, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
		public string Mobile { set; get; }

		[Display(Name = "نام")]
		[Required(ErrorMessage = " لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
		public string FirstName { get; set; }

		[Display(Name = "نام خانوادگی")]
		[Required(ErrorMessage = " لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
		public string LastName { get; set; }

		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = " لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
		public string Password { get; set; }

		[Display(Name = "تکرار کلمه عبور")]
		[Required(ErrorMessage = " لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
		[Compare("Password",ErrorMessage ="کلمه عبور مغایرت دارد")]
		public string ConfrimPassword { get; set; }


	}
	public enum RegisterUserResulte
	{
		Success,
		MobileExit,
		Error

	}
}
