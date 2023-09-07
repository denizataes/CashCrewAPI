using System;
namespace Entities.Models
{
    public class Debt
    {
        public int ID { get; set; }
        public string VacationID { get; set; }
        public int CreditorUserID { get; set; }
        public int DebtorUserID { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
    }
}

