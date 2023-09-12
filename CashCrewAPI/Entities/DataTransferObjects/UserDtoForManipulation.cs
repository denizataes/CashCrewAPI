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
        [Required(ErrorMessage = "FirstName is a required field.")]
        [MinLength(2, ErrorMessage = "FirstName must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "FirstName must consist of at maximum 50 characters")]
        public String FirstName { get; init; }

        [Required(ErrorMessage = "LastName is a required field.")]
        [MinLength(2, ErrorMessage = "LastName must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "LastName must consist of at maximum 50 characters")]
        public String LastName { get; init; }

    }
}

