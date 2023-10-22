using ShopDemo.Data.DTOs.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.Interface
{
	public interface IContactService :IAsyncDisposable
	{
		Task CreateContactUs(CreateContactUsDTO contact, string userIp, long? userId);
	}
}
