using shopDemo.application.Utils;
using ShopDemo.Data.Entity.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.EntitiesExtensions
{
    public static class BannerExtension
    {
        public static string GetBannerMainImageAddress(this SiteBanner banner)
        {
            return PathExtension.BannerOrigin + banner.ImageName;
        }
    }
}
