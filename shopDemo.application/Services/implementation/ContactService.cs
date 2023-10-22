using shopDemo.application.Services.Interface;
using ShopDemo.Data.DTOs.Contacts;
using ShopDemo.Data.Entity.Contacts;
using ShopDemo.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.implementation
{
	public class ContactService : IContactService
	{
		private readonly IGeneruicRepository<ContactUS> _contactUsRepository;

		public ContactService(IGeneruicRepository<ContactUS> contactUsRepository)
		{
			_contactUsRepository = contactUsRepository;
		}

		public async Task CreateContactUs(CreateContactUsDTO contact, string userIp, long? userId)
		{
			ContactUS ContactUS = new ContactUS()
			{
				UserId = userId!=null && userId.Value!=0? userId.Value:(long?) null,
				Subject = contact.Subject,
				Email = contact.Email,
				UserIp = userIp,
				Text = contact.Text,
				FullName = contact.FullName,
			};
			await _contactUsRepository.AddEntity(ContactUS);
			await _contactUsRepository.Savechanges();

		}

		public async ValueTask DisposeAsync()
		{
			await _contactUsRepository.DisposeAsync();	
		}
	}
}
