using Microsoft.AspNetCore.Mvc;
using shopDemo.application.Services.Interface;

namespace ShopDemo.Areas.Seller.Controllers
{

    public class ProductController : SellerBaseController
    {
        #region constructor

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region list

        //[HttpGet("")]
        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}

        #endregion
    }
}
