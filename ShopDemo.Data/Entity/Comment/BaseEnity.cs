using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Data.Entity.Comment
{
    public class BaseEnity<TEntity>
    { 
        public TEntity Id { get; set; }
    }
}
