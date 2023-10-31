using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace Entities.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string IBAN { get; set; }
        public string ProfilePictureURL { get; set; }
        public string Password { get; set; }
        
    }

}

