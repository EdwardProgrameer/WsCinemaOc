using WsCinemaOc.Models;

namespace WsCinemaOc.Interfaces
{
    public interface IRepository <TEntity>
    {
        
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IEnumerable<Pelicula>> GetAllMoviesAsync();
        Task<TEntity?> GetByIdAsync(int id);    

    }
}
