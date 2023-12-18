﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class PaymentParticipant
    {
        [Key]
        public int ID { get; set; }
        public int PaymentID { get; set; }
        public int ParticipantUserID { get; set; }
    }
}

