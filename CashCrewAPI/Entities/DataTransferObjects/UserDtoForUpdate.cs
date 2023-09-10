using System;
namespace Entities.DataTransferObjects
{
	public record UserDtoForUpdate(
								string FirstName,
								string LastName,
								string IBAN,
								string profilePictureURL
                                );
}
