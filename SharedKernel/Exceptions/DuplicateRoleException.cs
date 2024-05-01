namespace SharedKernel.Exceptions
{
    public class DuplicateRoleException : Exception
    {
        public DuplicateRoleException(string name) : base(message: $"{name} ismi daha önceden alınmış.")
        {

        }
    }
}
