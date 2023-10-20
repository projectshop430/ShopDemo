using Microsoft.EntityFrameworkCore;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.Entity.Site;
using ShopDemo.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.implementation
{
    public class SiteService : ISiteService
	{
		private readonly IGeneruicRepository<SiteSetting> _siteServiceRepository;

		public SiteService(IGeneruicRepository<SiteSetting> siteServiceRepository)
		{
			_siteServiceRepository = siteServiceRepository;
		}

		public async  ValueTask DisposeAsync()
		{
			await _siteServiceRepository.DisposeAsync();
		}

		public async Task<SiteSetting> GetDefaultSiteSetting()
		{
			return await _siteServiceRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.IsDefault && x.IsDeleted==false);
		}
	}
}
