using System;
namespace Entities.DataTransferObjects
{
    public record PaymentDto
    {
        public int ID { get; set; }
        public int VacationID { get; set; }
        public int PaidUserID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime PaidDateTime { get; set; }
        public decimal Price { get; set; }

        public List<PaymentParticipantDto> Participants { get; set; }
    }
}
