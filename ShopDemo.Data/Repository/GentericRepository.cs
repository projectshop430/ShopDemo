using Microsoft.EntityFrameworkCore;
using ShopDemo.Data.Context;
using ShopDemo.Data.Entity.Comment;
using System;
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
            await this._dbset.AddAsync(enity);
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
            entity.LastUpdateDate=DateTime.Now
            _dbset.Update(entity);
        }

        public async Task<TEnity> GetEnitybyId(long enityId)
        {
            return await this._dbset.SingleOrDefaultAsync(x => x.Id == enityId );
        }
    }
}
