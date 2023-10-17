using ShopDemo.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.Interface
{
    public interface IUserService :IAsyncDisposable
    {
        Task<RegisterUserResulte> RegisterUser(RegisterUserDTO register);
        Task<bool> IsUserExitByMobileNumber(string Mobile);
    }
}
