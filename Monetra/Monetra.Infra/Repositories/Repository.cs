using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Monetra.Domain.BackOffice.Entities.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Infra.Data.Context;

namespace Monetra.Infra.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async virtual Task<T?> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public void Create(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Remove(entity);
    }
}