using Microsoft.EntityFrameworkCore;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.DTOs.Paging;
using ShopDemo.Data.DTOs.Products;
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

        public async Task<CreateProductResult> CreateProduct(CreateProductDTO product, string imageName, long sellerId)
        {
            // create product
            var newProduct = new Product
            {
                Title = product.Title,
                Price = product.Price,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                IsActive = product.IsActive,
                SellerId = sellerId,
                ImageName = imageName,
            };

            await _productRepository.AddEntity(newProduct);
            await _productRepository.Savechanges();

            // create product categories


            // create product colors

            return CreateProductResult.Success;
        }


        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _productCategoryRepository.DisposeAsync();
            await _productRepository.DisposeAsync();
            await _productSelectedCategoryRepository.DisposeAsync();
        }

        public async Task<FilterProductDTO> FilterProducts(FilterProductDTO filter)
        {
            var query = _productRepository.GetQuery().AsQueryable();

            #region state

            switch (filter.FilterProductState)
            {
                case FilterProductState.Active:
                    query = query.Where(s => s.IsActive && s.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.NotActive:
                    query = query.Where(s => !s.IsActive && s.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.Accepted:
                    query = query.Where(s => s.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.Rejected:
                    query = query.Where(s => s.ProductAcceptanceState == ProductAcceptanceState.Rejected);
                    break;
                case FilterProductState.UnderProgress:
                    query = query.Where(s => s.ProductAcceptanceState == ProductAcceptanceState.UnderProgress);
                    break;
            }

            #endregion

            #region filter

            if (!string.IsNullOrEmpty(filter.ProductTitle))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.ProductTitle}%"));

            if (filter.SellerId != null && filter.SellerId != 0)
                query = query.Where(s => s.SellerId == filter.SellerId.Value);

            #endregion

            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetProducts(allEntities).SetPaging(pager);
        }

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await _productCategoryRepository.GetQuery().AsQueryable()
                 .Where(s => s.IsActive && !s.IsDeleted).ToListAsync();
        }

        public async Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId)
        {
            if (parentId == null || parentId == 0)
            {
                return await _productCategoryRepository.GetQuery()
                    .AsQueryable()
                    .Where(s => !s.IsDeleted && s.IsActive)
                    .ToListAsync();
            }

            return await _productCategoryRepository.GetQuery()
                .AsQueryable()
                .Where(s => !s.IsDeleted && s.IsActive && s.ParentId == parentId)
                .ToListAsync();
        }

        #endregion
    }
}


