using System;
namespace Entities.DataTransferObjects
{
	public record UserDto
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string IBAN { get; set; }
		public string ProfilePictureURL { get; set; }
	}
}
