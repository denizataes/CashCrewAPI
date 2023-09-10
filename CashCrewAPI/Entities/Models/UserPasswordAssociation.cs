using System;
namespace Entities.Models
{
    public class UserPasswordAssociation
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Password { get; set; }
    }
}

