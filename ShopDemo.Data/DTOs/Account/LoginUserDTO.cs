using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopDemo.Data.DTOs.Account
{
    public class LoginUserDTO
    {
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        public string Mobile { set; get; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        public string Password
        {
            get; set;
        }
    }
}
