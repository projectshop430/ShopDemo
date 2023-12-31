﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using shopDemo.application.Extensions;
using shopDemo.application.Services.Interface;
using shopDemo.application.Utils;
using ShopDemo.Data.DTOs.Common;
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
        private readonly IGeneruicRepository<ProductColor> _productColorRepository;

        public ProductService(IGeneruicRepository<Product> productRepository, IGeneruicRepository<ProductCategory> productCategoryRepository, IGeneruicRepository<ProductSelectedCategory> productSelectedCategoryRepository, IGeneruicRepository<ProductColor> productColorRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
            _productColorRepository = productColorRepository;
        }

        public async Task<bool> AcceptSellerProduct(long productId)
        {
            var product = await _productRepository.GetEnitybyId(productId);
            if (product != null)
            {
                product.ProductAcceptanceState = ProductAcceptanceState.Accepted;
                product.ProductAcceptOrRejectDescription = $"محصول مورد نظر در تاریخ {DateTime.Now.ToShamsi()} مورد تایید سایت قرار گرفت";
                _productRepository.EditEnity(product);
                await _productRepository.Savechanges();
                return true;
            }

            return false;
        }

        public async Task<CreateProductResult> CreateProduct(CreateProductDTO product, long sellerId, IFormFile productImage)
        {
            if (productImage == null) return CreateProductResult.HasNoImage;

            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productImage.FileName);

            var res = productImage.AddImageToServer(imageName, PathExtension.ProductImageImageServer, 150, 150, PathExtension.ProductThumbnailImageImageServer);

            if (res)
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
                    ProductAcceptanceState = ProductAcceptanceState.UnderProgress
                };

                await _productRepository.AddEntity(newProduct);
                await _productRepository.Savechanges();

                // create product categories
                var productSelectedCategories = new List<ProductSelectedCategory>();

                foreach (var categoryId in product.SelectedCategories)
                {
                    productSelectedCategories.Add(new ProductSelectedCategory
                    {
                        ProductCategoryId = categoryId,
                        ProductId = newProduct.Id
                    });
                }

                await _productSelectedCategoryRepository.AddRangeEntities(productSelectedCategories);
                await _productSelectedCategoryRepository.Savechanges();

                // create product colors
                var productSelectedColors = new List<ProductColor>();

                foreach (var productColor in product.ProductColors)
                {
                    productSelectedColors.Add(new ProductColor
                    {
                        ColorName = productColor.ColorName,
                        Price = productColor.Price,
                        ProductId = newProduct.Id
                    });
                }

                await _productColorRepository.AddRangeEntities(productSelectedColors);
                await _productSelectedCategoryRepository.Savechanges();

                return CreateProductResult.Success;
            }

            return CreateProductResult.Error;
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
                case FilterProductState.All:
                    break;
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

        public async Task<bool> RejectSellerProduct(RejectItemDTO reject)
        {
            var product = await _productRepository.GetEnitybyId(reject.Id);
            if (product != null)
            {
                product.ProductAcceptanceState = ProductAcceptanceState.Rejected;
                product.ProductAcceptOrRejectDescription = reject.RejectMessage;
                _productRepository.EditEnity(product);
                await _productRepository.Savechanges();

                return true;
            }

            return false;
        }

        #endregion
    }
}


