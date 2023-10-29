using Microsoft.EntityFrameworkCore;
using ShopDemo.Data.Entity.Account;
using ShopDemo.Data.Entity.Contacts;
using ShopDemo.Data.Entity.Site;
using ShopDemo.Data.Entity.Store;
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

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(var relationShip in modelBuilder.Model.GetEntityTypes().SelectMany(s=>s.GetForeignKeys()))
            {
                relationShip.DeleteBehavior = DeleteBehavior.Restrict;
            }
         

            base.OnModelCreating(modelBuilder);
        }
    }
}
