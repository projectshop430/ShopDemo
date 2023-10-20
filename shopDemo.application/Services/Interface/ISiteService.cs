using ShopDemo.Data.Entity.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.Interface
{
    public interface ISiteService : IAsyncDisposable
	{
		Task<SiteSetting> GetDefaultSiteSetting();
	}
}
