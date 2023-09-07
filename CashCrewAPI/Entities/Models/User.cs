using System;
using Microsoft.AspNetCore.Identity;
namespace Entities.Models
{
    public class User: IdentityUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IBAN { get; set; }
        public string ProfilePictureURL { get; set; }

        public List<UserPasswordAssociation> PasswordAssociations { get; set; }
    }

}

