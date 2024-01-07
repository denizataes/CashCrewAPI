using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class DebtReadDto
    {
        public int ID { get; set; }
        public int VacationID { get; set; }
        public UserInfoDto CreditorUser { get; set; }
        public UserInfoDto DebtorUser { get; set; }
        public decimal Amount { get; set; }
    }
}
