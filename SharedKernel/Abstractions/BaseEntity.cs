namespace SharedKernel.Abstractions
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            IsDeleted = false;
        }

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
