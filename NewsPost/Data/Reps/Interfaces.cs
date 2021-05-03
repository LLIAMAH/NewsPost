using System.Collections.Generic;
using System.Security.Principal;
using NewsPost.Data.Entities;

namespace NewsPost.Data.Reps
{
    public interface IRep
    {
    }

    public interface IResult<T>
    {
        public string Error { get; }
        public T Data { get; }
    }

    public interface IRep<in TId, T>
    {
        IResult<IEnumerable<T>> Get();
        IResult<T> Get(TId id);
        IResult<bool> Add(T item);
        IResult<bool> Update(TId id, T item);
        IResult<bool> Delete(TId id);
    }

    public interface IRepUsers
    {
        ApplicationUser GetUser(IPrincipal principal);
        string GetUserId(IPrincipal principal);
    }
}
