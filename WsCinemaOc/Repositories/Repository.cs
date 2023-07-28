using Microsoft.EntityFrameworkCore;
using WsCinemaOc.Interfaces;
using WsCinemaOc.Models;

namespace WsCinemaOc.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>where TEntity : class
    {
        private readonly Cinema_OC_CloudContext _context;

        public Repository(Cinema_OC_CloudContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pelicula>> GetAllMoviesAsync()
        {
            return await _context.Peliculas.Include(p => p.Genero).ToListAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }
}
