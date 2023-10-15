using Microsoft.EntityFrameworkCore;
using ShopDemo.Data.Entity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Data.Context
{
    public class ShopDemoContext : DbContext
    {
       
        public DbSet<User> Users { get; set; }

    }
}
