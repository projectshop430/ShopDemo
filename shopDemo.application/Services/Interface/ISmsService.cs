﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopDemo.application.Services.Interface
{
	public interface ISmsService
	{
		Task SendVerificationSms(string mobile,string activetioncode);
		Task SendUserPasswordsms(string mobile, string password);
	}
}
