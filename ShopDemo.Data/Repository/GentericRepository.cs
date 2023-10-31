using Microsoft.EntityFrameworkCore;
using ShopDemo.Data.Context;
using ShopDemo.Data.Entity.Comment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Data.Repository
{
    public class GentericRepository<TEnity> : IGeneruicRepository<TEnity> where TEnity : BaseEnity
    {
        private readonly ShopDemoContext _context;
        private readonly DbSet<TEnity> _dbset;
     
        public GentericRepository(ShopDemoContext context)
        {
            _context = context;
            this._dbset = _context.Set<TEnity>();
           

		}
        public async Task AddEntity(TEnity enity)
        {
            enity.CreateDate= DateTime.Now;
            enity.LastUpdateDate= enity.CreateDate;
            await this._dbset.AddAsync(enity);
           
        }

        public async Task AddRangeEntities(List<TEnity> entities)
        {

            foreach (var entity in entities)
            {
                await AddEntity(entity);
            }
        }

        public void DeleteEntity(TEnity entity)
        {
            entity.IsDeleted = true;
            EditEnity(entity);

        }

        public async Task DeleteEntity(long entityID)
        {
            TEnity enity = await GetEnitybyId(entityID);
            if (enity != null)
                DeleteEntity(enity);
        }

        public void DeletePermanent(TEnity entity)
        {
            _dbset.Remove(entity);
        }

        public async Task DeletePermanent(long entityID)
        {
            TEnity enity = await GetEnitybyId(entityID);
            if (enity != null)
                DeletePermanent(enity);
          
            
        }

        public async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
         
        }

        public void EditEnity(TEnity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            _dbset.Update(entity);
        }

        public async Task<TEnity> GetEnitybyId(long enityId)
        {
            return await this._dbset.SingleOrDefaultAsync(x => x.Id == enityId );
        }

        public IQueryable<TEnity> GetQuery()
        {
            return _dbset.AsQueryable();
        }

        public async Task Savechanges()
        {
           await  _context.SaveChangesAsync();
        }
    }
}
