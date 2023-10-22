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
		#region site settings
		Task<SiteSetting> GetDefaultSiteSetting();
		#endregion

		#region slider

		Task<List<Slider>> GetAllActiveSliders();

        #endregion

        #region site banners

        Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements);

        #endregion
    }
}
