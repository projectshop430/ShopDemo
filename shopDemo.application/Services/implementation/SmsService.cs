using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shopDemo.application.Services.Interface;

namespace shopDemo.application.Services.implementation
{
	public class SmsService :ISmsService
	{

		private string apikey = "sLI6vchWcpoCesrsdrLvj22ZaXeafctpKuv2GRQC406tUev0cxt7N4IOCxPAI8BU";

		public async Task SendVerificationSms(string mobile, string activetioncode)
		{
			SmsIr smsIr = new SmsIr(apikey);

			var bulkSendResult = await smsIr.VerifySendAsync(mobile, 100000, new VerifySendParameter[] { new("Code", activetioncode) });
		}
	}
}
