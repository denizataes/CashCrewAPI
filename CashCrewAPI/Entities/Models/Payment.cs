using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Payment
    {
        [Key]
        public int ID { get; set; }
        public int VacationID { get; set; }
        public int PaidUserID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime PaidDateTime { get; set; }
        public decimal Price { get; set; }
        public bool IsDebt { get; set; }

        public List<PaymentParticipant> Participants { get; set; }
    }
}

