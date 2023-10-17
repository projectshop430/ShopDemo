using Microsoft.EntityFrameworkCore;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.DTOs.Account;
using ShopDemo.Data.Entity.Account;
using ShopDemo.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.implementation
{
    public class UserService : IUserService
    {
        private readonly IGeneruicRepository<User> _Userrepository;
        private readonly IPasswordHelper _PasswordHelper;

        public UserService(IGeneruicRepository<User> userrepository, IPasswordHelper passwordHelper)
        {
            _Userrepository = userrepository;
            _PasswordHelper = passwordHelper;
        }

        public async ValueTask DisposeAsync()
        {
           await _Userrepository.DisposeAsync();
        }

        public async Task<bool> IsUserExitByMobileNumber(string Mobile)
        {
            return await _Userrepository.GetQuery().AsQueryable().AnyAsync(x=>x.Mobile == Mobile);
        }

        public async Task<RegisterUserResulte> RegisterUser(RegisterUserDTO register)
        {
            try
            {
                if (!await IsUserExitByMobileNumber(register.Mobile))
                {
                    var user = new User()
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Mobile = register.Mobile,
                        Password = _PasswordHelper.EncodePasswordMD5(register.Password),
                        MobileActiveCode = new Random().Next(10000, 99999).ToString(),
                        EmailActiveCode = Guid.NewGuid().ToString("N"),
                    };
                    await _Userrepository.AddEntity(user);
                    await _Userrepository.Savechanges();
                    //
                    return RegisterUserResulte.Success;
                }
                else
                {
                    return RegisterUserResulte.MobileExit;
                }
            }
            catch(Exception ex)
            {
                return RegisterUserResulte.Error;
            }
           

            return RegisterUserResulte.Success;
           
        }
    }
}
