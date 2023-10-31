using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record VacationDtoForManipulation
    {
        [Required(ErrorMessage = "Title is a required field.")]
        [MinLength(2, ErrorMessage = "Title must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Title must consist of at maximum 50 characters")]
        public String Title { get; init; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(2, ErrorMessage = "Password must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Password must consist of at maximum 50 characters")]
        public String Password { get; init; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MinLength(2, ErrorMessage = "Description must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Description must consist of at maximum 50 characters")]
        public String Description { get; init; }

    }
}

