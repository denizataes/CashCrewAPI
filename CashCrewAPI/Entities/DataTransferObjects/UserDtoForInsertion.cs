using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record UserDtoForInsertion : UserDtoForManipulation
    {
        public string IBAN { get; init; }
        public string ProfilePictureURL { get; init; }
    }
}
