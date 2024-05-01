namespace TramerQuery.Data.Abstractions
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAll();
        Task<T> FindById(int id);
        Task<T> Create(T entity);
        void Update(T entity);
        Task Delete(int id);

    }
}
