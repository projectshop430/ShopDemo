using Microsoft.EntityFrameworkCore;
using ShopDemo.Data.Entity.Account;
using ShopDemo.Data.Entity.Contacts;
using ShopDemo.Data.Entity.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Data.Context
{
    public class ShopDemoContext : DbContext
    {
        public ShopDemoContext(DbContextOptions<ShopDemoContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }

		public DbSet<ContactUS> contactUses { get; set; }
        public DbSet<Slider> sliderUses { get; set; }
        public DbSet<SiteBanner> siteBanners { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(var relationShip in modelBuilder.Model.GetEntityTypes().SelectMany(s=>s.GetForeignKeys()))
            {
                relationShip.DeleteBehavior = DeleteBehavior.Cascade;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
