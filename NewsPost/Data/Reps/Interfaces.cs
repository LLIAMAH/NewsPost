using System;
using System.Collections.Generic;
using System.Security.Principal;
using NewsPost.Data.Entities;

namespace NewsPost.Data.Reps
{
    public interface IRep: IRepUsers, IRepNews, IRepPosts { }

    public interface IResult<out T>
    {
        string Error { get; }
        T Data { get; }
    }

    public interface IRepUsers
    {
        ApplicationUser GetUser(IPrincipal principal);
        string GetUserId(IPrincipal principal);
    }

    public interface IRepNews
    {
        IResult<IEnumerable<News>> GetNewsDaily(DateTime date);
    }

    public interface IRepPosts
    {
        IResult<IEnumerable<Post>> GetPosts();
    }
}
