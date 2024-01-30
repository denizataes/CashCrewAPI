using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class VacationUserAssociation
    {
        [Key]
        public int Id { get; set; }
        public int VacationID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; } 
    }

}

