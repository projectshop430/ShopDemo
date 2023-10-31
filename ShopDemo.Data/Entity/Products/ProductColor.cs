using ShopDemo.Data.Entity.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopDemo.Data.Entity.Products
{
    public class ProductColor : BaseEnity
    {
        #region properties

        public long ProductId { get; set; }

        [Display(Name = "رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ColorName { get; set; }

        public int Price { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        #endregion
    }
}

