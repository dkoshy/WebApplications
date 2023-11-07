using App.DvdRental.Data.Models;

namespace App.DvdRental.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<QueryResult<T>> GetByIdAsync<TKey>(TKey Id);
        Task<QueryResult<IEnumerable<T>>> GetAllAsync();
    }
}
