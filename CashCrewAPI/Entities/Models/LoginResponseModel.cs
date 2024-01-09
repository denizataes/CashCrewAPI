using Entities.DataTransferObjects;
using System;
namespace Entities.Models
{
	public class LoginResponseModel
	{
		public string Token { get; set; }

		public string Message { get; set; }

		public bool IsSuccess { get { return Message == "Success" ? true : false; } }

		public UserDto User { get; set; }
		
	}
}

