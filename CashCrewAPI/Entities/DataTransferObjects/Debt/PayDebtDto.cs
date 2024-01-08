using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class PayDebtDto
    {
        public int CreditorUserID { get; set; } // alacaklı
        public int DebtorUserID { get; set; } // borçlu
        public decimal Amount { get; set; }
        public int VacationID { get; set; }
    }
}
