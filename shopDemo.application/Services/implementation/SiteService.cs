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
		private readonly IGeneruicRepository<SiteSetting> _siteSiteSettingRepository;
		private readonly IGeneruicRepository<Slider> _sliderRepository;
        private readonly IGeneruicRepository<SiteBanner> _banerrepository;

        public SiteService(IGeneruicRepository<SiteSetting> siteSiteSettingRepository, IGeneruicRepository<Slider> sliderRepository, IGeneruicRepository<SiteBanner> banerrepository)
        {
            _siteSiteSettingRepository = siteSiteSettingRepository;
            _sliderRepository = sliderRepository;
            _banerrepository = banerrepository;
        }

        public async  ValueTask DisposeAsync()
		{
			if (_siteSiteSettingRepository != null) await _siteSiteSettingRepository.DisposeAsync();
			if (_sliderRepository != null) await _sliderRepository.DisposeAsync();
            if (_banerrepository != null) await _banerrepository.DisposeAsync();
        }

		public async Task<List<Slider>> GetAllActiveSliders()
		{
			return await _sliderRepository.GetQuery().AsQueryable()
			   .Where(s => s.IsActive && !s.IsDeleted).ToListAsync();
		}

		public async Task<SiteSetting> GetDefaultSiteSetting()
		{
			return await _siteSiteSettingRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.IsDefault && x.IsDeleted==false);
		}

        public async Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements)
        {
            return await _banerrepository.GetQuery().AsQueryable().Where(x => placements.Any(y => y == x.BannerPlacement)).ToListAsync();
        }
    }
}
