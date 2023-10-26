using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.DTOs.Contacts;
using ShopDemo.Data.DTOs.Paging;
using ShopDemo.Data.Entity.Contacts;
using ShopDemo.Data.Repository;

namespace shopDemo.application.Services.implementation
{
	public class ContactService : IContactService
	{
		private readonly IGeneruicRepository<ContactUS> _contactUsRepository;
        private readonly IGeneruicRepository<Ticket> _ticketRepository;
        private readonly IGeneruicRepository<TicketMessage> _ticketMessageRepository;

        public ContactService(IGeneruicRepository<ContactUS> contactUsRepository, IGeneruicRepository<Ticket> ticketRepository, IGeneruicRepository<TicketMessage> ticketMessageRepository)
        {
            _contactUsRepository = contactUsRepository;
            _ticketRepository = ticketRepository;
            _ticketMessageRepository = ticketMessageRepository;
        }

        public async Task<AddTicketResult> AddUserTicket(AddTicketDTO ticket, long userId)
        {
            if (string.IsNullOrEmpty(ticket.Text)) return AddTicketResult.Error;

            // add ticket
            var newTicket = new Ticket
            {
                OwnerId = userId,
                IsReadByOwner = true,
                TicketPriority = ticket.TicketPriority,
                Title = ticket.Title,
                TicketSection = ticket.TicketSection,
                TicketState = TicketState.UnderProgress
            };
           
             await _ticketRepository.AddEntity(newTicket);
             await _ticketRepository.Savechanges();
            
            // add ticket message
            var newMessage = new TicketMessage
            {
                TicketId = newTicket.Id,
                Text = ticket.Text,
                SenderId = userId,
            };

            await _ticketMessageRepository.AddEntity(newMessage);
            await _ticketMessageRepository.Savechanges();

            return AddTicketResult.Success;
        }

        public async Task<AnswerTicketResult> AnswerTicket(AnswerTicketDTO answer, long userId)
        {
            var ticket = await _ticketRepository.GetEnitybyId(answer.Id);
            if (ticket == null) return AnswerTicketResult.NotFound;
            if (ticket.OwnerId != userId) return AnswerTicketResult.NotForUser;

            var ticketMessage = new TicketMessage
            {
                TicketId = ticket.Id,
                SenderId = userId,
                Text = answer.Text
            };

            await _ticketMessageRepository.AddEntity(ticketMessage);
            await _ticketMessageRepository.Savechanges();

            ticket.IsReadByAdmin = false;
            ticket.IsReadByOwner = true;
            await _ticketRepository.Savechanges();
            return AnswerTicketResult.Success;
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

        public async Task<FilterTicketDTO> FilterTickets(FilterTicketDTO filter)
        {
            var query = _ticketRepository.GetQuery().AsQueryable();

            #region state

            switch (filter.FilterTicketState)
            {
                case FilterTicketState.All:
                    break;
                case FilterTicketState.Deleted:
                    query = query.Where(s => s.IsDeleted);
                    break;
                case FilterTicketState.NotDeleted:
                    query = query.Where(s => !s.IsDeleted);
                    break;
            }

            switch (filter.OrderBy)
            {
                case FilterTicketOrder.CreateDate_ASC:
                    query = query.OrderBy(s => s.CreateDate);
                    break;
                case FilterTicketOrder.CreateDate_DES:
                    query = query.OrderByDescending(s => s.CreateDate);
                    break;
            }

            #endregion

            #region filter

            if (filter.TicketSection != null)
                query = query.Where(s => s.TicketSection == filter.TicketSection.Value);

            if (filter.TicketPriority != null)
                query = query.Where(s => s.TicketPriority == filter.TicketPriority.Value);

            if (filter.UserId != null && filter.UserId != 0)
                query = query.Where(s => s.OwnerId == filter.UserId.Value);

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));

            #endregion

            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetPaging(pager).SetTickets(allEntities);
        }

        public async Task<TicketDetailDTO> GetTicketForShow(long ticketId, long userId)
        {
            var ticket = await _ticketRepository.GetQuery().AsQueryable()
                  .Include(s => s.Owner)
                  .SingleOrDefaultAsync(s => s.Id == ticketId);

            if (ticket == null || ticket.OwnerId != userId) return null;

            return new TicketDetailDTO
            {
                Ticket = ticket,
                TicketMessages = await _ticketMessageRepository.GetQuery().AsQueryable()
                    .OrderByDescending(s => s.CreateDate)
                    .Where(s => s.TicketId == ticketId && !s.IsDeleted).ToListAsync()
            };
        }
    }
}
