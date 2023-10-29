using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Data.DTOs.Paging
{
    public class BasePaging
    {
        public BasePaging()
        {
            PageId = 1;
            TakeEntity = 10;
            HowManyShowPageAfterAndBefore = 3;
        }
        //صفحه که هستیم
        public int PageId { get; set; }

        public int PageCount { get; set; }

        //چند تا محصول یا هر چیزی وجود دارد
        public int AllEntitiesCount { get; set; }

        //دکمه های پایین لیست آغاز
        public int StartPage { get; set; }
        //دکمه های پایین لیست پایان
        public int EndPage { get; set; }

        //نمایش از صفحه اول تا جایی که هستیم فیلتر شود
        public int TakeEntity { get; set; }

        public int SkipEntity { get; set; }

        //چندصفحه قل چند صفحه بعد وجو دارد
        public int HowManyShowPageAfterAndBefore { get; set; }

        public BasePaging GetCurrentPaging()
        {
            return this;
        }
    }
}
