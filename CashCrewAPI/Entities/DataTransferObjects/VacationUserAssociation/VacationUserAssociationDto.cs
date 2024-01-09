using Entities.Models;
using System;
namespace Entities.DataTransferObjects
{
	public record VacationUserAssociationDto
	{
        public UserDto User { get; set; }

    }
}
