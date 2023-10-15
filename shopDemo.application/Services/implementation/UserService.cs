using shopDemo.application.Services.Interface;
using ShopDemo.Data.Entity.Account;
using ShopDemo.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.implementation
{
    public class UserService : IUserService
    {
        private readonly IGeneruicRepository<User> _Userrepository;

        public UserService(IGeneruicRepository<User> userrepository)
        {
            _Userrepository = userrepository;
        }

        public async ValueTask DisposeAsync()
        {
           await _Userrepository.DisposeAsync();
        }
    }
}
