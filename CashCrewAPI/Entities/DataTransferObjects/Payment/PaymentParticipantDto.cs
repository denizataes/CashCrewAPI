using System;
namespace Entities.DataTransferObjects
{
    public class PaymentParticipantDto
    {
        public int ID { get; set; }
        public int PaymentID { get; set; }
        public int ParticipantUserID { get; set; }
    }
}
