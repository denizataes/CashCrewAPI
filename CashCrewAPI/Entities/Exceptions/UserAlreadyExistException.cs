namespace Entities.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int id)
            : base($"The user with id : {id} could not found.")
        {
        }
    }
}
