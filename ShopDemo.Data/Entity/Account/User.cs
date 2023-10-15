using ShopDemo.Data.Entity.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Data.Entity.Account
{
    public class User :BaseEnity
    {
        #region property
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
     
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        public string EmailActiveCode { get; set; }

        [Display(Name = "فعال /غیر فعال")]
        public bool IsEmailActive { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        public string Mobile { set; get; }

        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        public string MobileActiveCode { get; set;}

        [Display(Name = "فعال /غیر فعال")]
        public bool IsMobileActive { get;set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        public string Password { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        public string LastName { get; set; }
      

        [Display(Name = " تصویر آواتار")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0}نمی توانید بشتر از {1} کاراکتر واردکنید")]
        public string Avatar { get; set; }

        public bool IsBlock { get; set; }
        #endregion
        #region relation
        #endregion
    }
}
