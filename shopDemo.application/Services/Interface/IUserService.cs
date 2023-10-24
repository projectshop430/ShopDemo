using ShopDemo.Data.DTOs.Account;
using ShopDemo.Data.Entity.Account;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopDemo.Data.DTOs.Account.LoginUserDTO;

namespace shopDemo.application.Services.Interface
{
    public interface IUserService :IAsyncDisposable
    {
        Task<RegisterUserResulte> RegisterUser(RegisterUserDTO register);
        Task<bool> IsUserExitByMobileNumber(string Mobile);
		Task<LoginUserResulte> GetUserForlogin(LoginUserDTO login);

		Task<ForgotPasswordResulte> RecoveryUerPassword(ForgotPasswordDTO forgot);
		Task<User> GetUserByMobile(string mobile);
		Task<bool> ActivateMobile(ActivateMobileDTO activate);
		Task<bool> ChangeUserPassword(ChangePasswordDTO changePass, long currentUserId);
	}
}
