using System;
namespace Entities.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public string VacationID { get; set; }
        public int PaidUserID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime PaidDateTime { get; set; }
        public decimal Amount { get; set; }

        public List<PaymentParticipant> Participants { get; set; }
    }
}

