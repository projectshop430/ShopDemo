﻿using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using shopDemo.application.Extensions;
using shopDemo.application.Services.Interface;
using shopDemo.application.Utils;
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
		public readonly ISmsService _smsService;

		public UserService(IGeneruicRepository<User> userrepository, IPasswordHelper passwordHelper, ISmsService smsService)
		{
			_Userrepository = userrepository;
			_PasswordHelper = passwordHelper;
			_smsService = smsService;
		}

		public async ValueTask DisposeAsync()
        {
           await _Userrepository.DisposeAsync();
        }

		public async Task<User> GetUserByMobile(string mobile)
		{
            return await _Userrepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.Mobile == mobile);
         }

		public async Task<bool> IsUserExitByMobileNumber(string Mobile)
        {
            return await _Userrepository.GetQuery().AsQueryable().AnyAsync(x=>x.Mobile == Mobile);
        }

		public async Task<LoginUserDTO.LoginUserResulte> GetUserForlogin(LoginUserDTO login)
		{
            var user =await _Userrepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.Mobile == login.Mobile);
            if(user == null || user.Password!= _PasswordHelper.EncodePasswordMD5(login.Password)) 
				return LoginUserDTO.LoginUserResulte.Notfound;
	
            if (!user.IsMobileActive)
                return LoginUserDTO.LoginUserResulte.NotActivated;

			return LoginUserDTO.LoginUserResulte.Success;
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
                    await _smsService.SendVerificationSms(user.Mobile, user.MobileActiveCode);
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

		public async Task<ForgotPasswordResulte> RecoveryUerPassword(ForgotPasswordDTO forgot)
		{
            try
            {

                var user = await _Userrepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.Mobile == forgot.Mobile);
                if (user == null) return ForgotPasswordResulte.NotFound;
                var newPassword = new Random().Next(1000000, 9999999).ToString();
                user.Password = _PasswordHelper.EncodePasswordMD5(newPassword);
                _Userrepository.EditEnity(user);
                //sms send
                await _smsService.SendUserPasswordsms(user.Mobile, newPassword);
                await _Userrepository.Savechanges();
                return ForgotPasswordResulte.Success;
            }
            catch(Exception ex)
            {
                return ForgotPasswordResulte.Eroor;
            }
		}

		public async Task<bool> ActivateMobile(ActivateMobileDTO activate)
		{
			var user = await _Userrepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.Mobile == activate.Mobile);
            if (user != null) 
            {
                if (user.MobileActiveCode==activate.MobileActiveCode)
                {
                    user.IsMobileActive = true;
                    user.MobileActiveCode = new Random().Next(1000000, 9999999).ToString();
                    await _Userrepository.Savechanges();
                    return true;
                }
                
            };
            return false; 
		}

		public async Task<bool> ChangeUserPassword(ChangePasswordDTO changePass, long currentUserId)
		{
			var user = await _Userrepository.GetEnitybyId(currentUserId);
			if (user != null)
			{
				var newPassword = _PasswordHelper.EncodePasswordMD5(changePass.NewPassword);
				if (newPassword != user.Password)
				{
					user.Password = newPassword;
					_Userrepository.EditEnity(user);
					await _Userrepository.Savechanges();

					return true;
				}
			}

			return false;
		}


        public async Task<EditUserProfileResult> EditUserProfile(EditUserProfileDTO profile, long userId, IFormFile avatarImage)
        {
            var user = await _Userrepository.GetEnitybyId(userId);
            if (user == null) return EditUserProfileResult.NotFound;

            if (user.IsBlocked) return EditUserProfileResult.IsBlocked;
            if (!user.IsMobileActive) return EditUserProfileResult.IsNotActive;

            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;

            if (avatarImage != null && avatarImage.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(avatarImage.FileName);
                avatarImage.AddImageToServer(imageName, PathExtension.UserAvatarOriginServer, 100, 100, PathExtension.UserAvatarThumbServer, user.Avatar);
                user.Avatar = imageName;
            }

            _Userrepository.EditEnity(user);
            await _Userrepository.Savechanges();

            return EditUserProfileResult.Success;
        }

        public async Task<EditUserProfileDTO> GetProfileForEdit(long userId)
        {
            var user = await _Userrepository.GetEnitybyId(userId);
            if (user == null) return null;

            return new EditUserProfileDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar
            };
        }
    }
}
