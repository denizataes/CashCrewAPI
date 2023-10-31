namespace Entities.Exceptions
{
    public sealed class VacationAlreadyExistException : NotFoundException
    {
        public VacationAlreadyExistException(string title, string description)
            : base($"A vacation with the title '{title}' and description '{description}' already exists. Please modify the title or description and try creating it again.")
        {
        }
    }
}
