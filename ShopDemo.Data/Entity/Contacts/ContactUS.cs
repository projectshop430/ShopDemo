using ShopDemo.Data.Entity.Account;
using ShopDemo.Data.Entity.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopDemo.Data.Entity.Contacts
{
	public class ContactUS:BaseEnity
	{

		public long? UserId { get; set; }

		[Display(Name = "IP کاربر")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string UserIp { get; set; }

		[Display(Name = "ایمیل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Email { get; set; }

		[Display(Name = "نام و نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string FullName { get; set; }

		[Display(Name = "عنوان")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
		public string Subject { get; set; }

		[Display(Name = "متن پیام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Text { get; set; }

		#region relations

		public User User { get; set; }

		#endregion
	}

}
