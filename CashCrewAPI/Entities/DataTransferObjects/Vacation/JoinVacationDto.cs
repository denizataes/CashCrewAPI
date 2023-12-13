using System;
namespace Entities.DataTransferObjects
{
	public record JoinVacationDto
	{
        public int VacationID { get; set; }
        public int UserID { get; set; }
        public string Password { get; set; }
    }
}
