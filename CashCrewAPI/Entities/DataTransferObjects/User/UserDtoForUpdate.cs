using System;
namespace Entities.DataTransferObjects
{
	public record UserDtoForUpdate : UserDtoForManipulation
	{
        public int ID { get; init; }
        public string IBAN { get; init; }
        public string ProfilePictureURL { get; init; }
    }
}
