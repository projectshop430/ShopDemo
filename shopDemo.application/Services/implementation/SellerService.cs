using Microsoft.EntityFrameworkCore;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.DTOs.Paging;
using ShopDemo.Data.DTOs.Seller;
using ShopDemo.Data.Entity.Account;
using ShopDemo.Data.Entity.Store;
using ShopDemo.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.implementation
{
    public class SellerService : ISellerService
    {

        #region constcutor

        private readonly IGeneruicRepository<Seller> _sellerRepository;
        private readonly IGeneruicRepository<User> _userRepository;

        public SellerService(IGeneruicRepository<Seller> sellerRepository, IGeneruicRepository<User> userRepository)
        {
            _sellerRepository = sellerRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AcceptSellerRequest(long requestId)
        {
            var sellerRequest = await _sellerRepository.GetEnitybyId(requestId);
            if (sellerRequest != null)
            {
                sellerRequest.StoreAcceptanceState = StoreAcceptanceState.Accepted;
                _sellerRepository.EditEnity(sellerRequest);
                await _sellerRepository.Savechanges();

                return true;
            }

            return false;
        }

        #endregion

        #region seller

        public async Task<RequestSellerResult> AddNewSellerRequest(RequestSellerDTO seller, long userId)
        {
            var user = await _userRepository.GetEnitybyId(userId);

            if (user.IsBlocked) return RequestSellerResult.HasNotPermission;

            var hasUnderProgressRequest = await _sellerRepository.GetQuery().AsQueryable().AnyAsync(s =>
                s.UserId == userId && s.StoreAcceptanceState == StoreAcceptanceState.UnderProgress);

            if (hasUnderProgressRequest) return RequestSellerResult.HasUnderProgressRequest;

            var newSeller = new Seller
            {
                UserId = userId,
                StoreName = seller.StoreName,
                Address = seller.Address,
                Phone = seller.Phone,
                StoreAcceptanceState = StoreAcceptanceState.UnderProgress
            };

            await _sellerRepository.AddEntity(newSeller);
            await _sellerRepository.Savechanges();

            return RequestSellerResult.Success;
        }

        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _sellerRepository.DisposeAsync();
        }

        public async Task<EditRequestSellerResult> EditRequestSeller(EditRequestSellerDTO request, long currentUserId)
        {
            var seller = await _sellerRepository.GetEnitybyId(request.Id);
            if (seller == null || seller.UserId != currentUserId) return EditRequestSellerResult.NotFound;

            seller.Phone = request.Phone;
            seller.Address = request.Address;
            seller.StoreName = request.StoreName;
            seller.StoreAcceptanceState = StoreAcceptanceState.UnderProgress;
            _sellerRepository.EditEnity(seller);
            await _sellerRepository.Savechanges();

            return EditRequestSellerResult.Success;
        }

        public async Task<FilterSellerDTO> FilterSellers(FilterSellerDTO filter)
        {
            var query = _sellerRepository.GetQuery()
               .Include(s => s.User)
               .AsQueryable();

            #region state

            switch (filter.State)
            {
                case FilterSellerState.All:
                    query = query.Where(s => !s.IsDeleted);
                    break;
                case FilterSellerState.Accepted:
                    query = query.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.Accepted && !s.IsDeleted);
                    break;

                case FilterSellerState.UnderProgress:
                    query = query.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.UnderProgress && !s.IsDeleted);
                    break;
                case FilterSellerState.Rejected:
                    query = query.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.Rejected && !s.IsDeleted);
                    break;
            }

            #endregion

            #region filter

            if (filter.UserId != null && filter.UserId != 0)
                query = query.Where(s => s.UserId == filter.UserId);

            if (!string.IsNullOrEmpty(filter.StoreName))
                query = query.Where(s => EF.Functions.Like(s.StoreName, $"%{filter.StoreName}%"));

            if (!string.IsNullOrEmpty(filter.Phone))
                query = query.Where(s => EF.Functions.Like(s.Phone, $"%{filter.Phone}%"));

            if (!string.IsNullOrEmpty(filter.Mobile))
                query = query.Where(s => EF.Functions.Like(s.Mobile, $"%{filter.Mobile}%"));

            if (!string.IsNullOrEmpty(filter.Address))
                query = query.Where(s => EF.Functions.Like(s.Address, $"%{filter.Address}%"));

            #endregion

            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetPaging(pager).SetSellers(allEntities);
        }

        public async Task<EditRequestSellerDTO> GetRequestSellerForEdit(long id, long currentUserId)
        {
            var seller = await _sellerRepository.GetEnitybyId(id);
            if (seller == null || seller.UserId != currentUserId) return null;

            return new EditRequestSellerDTO
            {
                Id = seller.Id,
                Phone = seller.Phone,
                Address = seller.Address,
                StoreName = seller.StoreName
            };
        }

        #endregion
    }
}
