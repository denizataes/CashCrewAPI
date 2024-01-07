using System;
namespace Entities.DataTransferObjects
{
    public class PaymentParticipantWriteDto
    {
        public int PaymentID { get; set; }
        public int ParticipantUserID { get; set; }
    }
}
