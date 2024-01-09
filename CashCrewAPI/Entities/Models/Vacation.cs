using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class Vacation
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VacationPictureURL { get; set; }
        public string Password { get; set; }
        public int CreatedUserID { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public ICollection<VacationUserAssociation> VacationUserAssociations { get; set; }
    }
}

