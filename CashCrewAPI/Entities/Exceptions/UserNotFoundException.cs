namespace Entities.Exceptions
{
    public sealed class UserAlreadyExistException : NotFoundException
    {
        public UserAlreadyExistException()
            : base($"There is another user with this username. Enter another username.")
        {
        }
    }
}
