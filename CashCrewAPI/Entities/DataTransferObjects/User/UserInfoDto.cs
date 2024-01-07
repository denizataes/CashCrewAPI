using System;
namespace Entities.DataTransferObjects
{
	public record UserInfoDto
	{
        public int ID { get; set; }
        public string Username { get; set; }
        public string IBAN { get; set; }
        public string ProfilePictureURL { get; set; }
    }
}
