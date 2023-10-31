using ShopDemo.Data.DTOs.Products;
using ShopDemo.Data.Entity.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.Interface
{
    public interface IProductService : IAsyncDisposable
    {
        #region products

        Task<FilterProductDTO> FilterProducts(FilterProductDTO filter);

        #endregion

        #region product categories

        Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId);

        #endregion
    }
}
