using ShopDemo.Data.Entity.Comment;
using ShopDemo.Data.Entity.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Data.Repository
{
    public interface IGeneruicRepository<TEntity> : IAsyncDisposable where TEntity : BaseEnity
    
    {
        IQueryable<TEntity> GetQuery();

        Task AddRangeEntities(List<TEntity> entities);
        Task AddEntity(TEntity entity);
        Task<TEntity> GetEnitybyId(long enityId);
       void EditEnity(TEntity entity);
        void DeleteEntity(TEntity entity);
        Task DeleteEntity(long entityID);

        void DeletePermanent(TEntity entity);
        Task DeletePermanent(long entityID);

        Task Savechanges();
      
    }
}
