using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record UserDtoForManipulation
    {
        [Required(ErrorMessage = "Username is a required field.")]
        [MinLength(2, ErrorMessage = "Username must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Username must consist of at maximum 50 characters")]
        public String Username { get; init; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(2, ErrorMessage = "Password must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Password must consist of at maximum 50 characters")]
        public String Password { get; init; }

    }
}

