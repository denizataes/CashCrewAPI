namespace Entities.Exceptions
{
    public sealed class VacationNotExistsException : NotFoundException
    {
        public VacationNotExistsException(int id)
            : base($"The vacation with id : {id} could not found.")
        {
        }
    }
}
