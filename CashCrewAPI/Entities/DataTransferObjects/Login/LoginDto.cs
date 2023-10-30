using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record LoginDto : LoginDtoForManipulation
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
