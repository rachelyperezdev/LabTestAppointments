using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Interfaces.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointments.Data.Repositories.Mocks.Generic
{
    public class MockGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly LabTestAppointmentsContext _dbContext;
        public MockGenericRepository(LabTestAppointmentsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(TEntity entity, int id)
        {
            var entry = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
