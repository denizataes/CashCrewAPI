using System;
namespace Entities.Models
{
    public class Vacation
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VacationPictureURL { get; set; }
        public int CreatedUserID { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public List<VacationUserAssociation> UserAssociations { get; set; }
        public List<VacationPasswordAssociation> PasswordAssociations { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Debt> Debts { get; set; }
    }
}

