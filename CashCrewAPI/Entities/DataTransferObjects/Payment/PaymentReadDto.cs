using System;
namespace Entities.DataTransferObjects
{
    public record PaymentReadDto
    {
        public int ID { get; set; }
        public int VacationID { get; set; }
        public UserInfoDto PaidUser { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime PaidDateTime { get; set; }
        public decimal Price { get; set; }
        public bool IsDebt { get; set; }
        public List<PaymentParticipantReadDto> Participants { get; set; }
    }
}
