namespace TaskTodo.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        Task AddData(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task Save();

    }
}
