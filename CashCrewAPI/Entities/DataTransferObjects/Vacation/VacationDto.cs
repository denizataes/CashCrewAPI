using System;
namespace Entities.DataTransferObjects
{
	public record VacationDto
	{
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VacationPictureURL { get; set; }
        public string Password { get; set; }
        public int CreatedUserID { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
