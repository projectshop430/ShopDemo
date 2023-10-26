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
        #region contact us
        Task CreateContactUs(CreateContactUsDTO contact, string userIp, long? userId);
        #endregion

        #region ticket

        Task<AddTicketResult> AddUserTicket(AddTicketDTO ticket, long userId);
        Task<FilterTicketDTO> FilterTickets(FilterTicketDTO filter);
        Task<TicketDetailDTO> GetTicketForShow(long ticketId, long userId);

        Task<AnswerTicketResult> AnswerTicket(AnswerTicketDTO answer, long userId);
        #endregion
    }
}
