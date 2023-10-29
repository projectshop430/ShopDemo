using shopDemo.application.Services.Interface;
using ShopDemo.Data.Entity.Products;
using ShopDemo.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.implementation
{
    public class ProductService : IProductService
    {
        #region constructor

        private readonly IGeneruicRepository<Product> _productRepository;
        private readonly IGeneruicRepository<ProductCategory> _productCategoryRepository;
        private readonly IGeneruicRepository<ProductSelectedCategory> _productSelectedCategoryRepository;

        public ProductService(IGeneruicRepository<Product> productRepository, IGeneruicRepository<ProductCategory> productCategoryRepository, IGeneruicRepository<ProductSelectedCategory> productSelectedCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
        }


        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _productCategoryRepository.DisposeAsync();
            await _productRepository.DisposeAsync();
            await _productSelectedCategoryRepository.DisposeAsync();
        }

        #endregion
    }
}


