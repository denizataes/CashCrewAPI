using System;
namespace Entities.DataTransferObjects
{
	public record UserDto
	{
		public int ID { get; set; }
		public string Username { get; set; }
		public string IBAN { get; set; }
		public string ProfilePictureURL { get; set; }
        public string Password { get; set; }
    }
}
