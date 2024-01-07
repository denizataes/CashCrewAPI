using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class DebtDto
    {
        public int ID { get; set; }
        public int VacationID { get; set; }
        public int CreditorUserID { get; set; }
        public int DebtorUserID { get; set; }
        public decimal Amount { get; set; }
    }
}
