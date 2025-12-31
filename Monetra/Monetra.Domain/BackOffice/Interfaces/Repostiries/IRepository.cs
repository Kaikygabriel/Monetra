using System.Linq.Expressions;
using Monetra.Domain.BackOffice.Entities.Abstraction;

namespace Monetra.Domain.BackOffice.Interfaces.Repostiries;

public interface IRepository<T> where T : Entity
{
     Task<IEnumerable<T>> GetAllAsync();
     Task<T?> GetByPredicate(Expression<Func<T, bool>> predicate);
     void Create(T entity);
     void Update(T entity);
     void Delete(T entity);
}