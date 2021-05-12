using System;
using System.Collections.Generic;
using System.Security.Principal;
using NewsPost.Areas.Admin.Models;
using NewsPost.Data.Entities;

namespace NewsPost.Data.Reps
{
    public interface IRep: IRepNews, IRepPosts, IRepUsers { }

    public interface IResult<out T>
    {
        string Error { get; }
        T Data { get; }
    }

    public interface IRepUser
    {
        ApplicationUser GetUser(IPrincipal principal);
        string GetUserId(IPrincipal principal);
        bool HasAdminUser();
    }

    public interface IRepUsers
    {
        IResult<IEnumerable<UserRoles>> GetUserWithRoles();
    }

    public interface IRepNews: IRepUser
    {
        IResult<IEnumerable<Article>> GetNewsDaily(DateTime dateTime);
    }

    public interface IRepPosts : IRepUser
    {
        IResult<IEnumerable<Article>> GetUnApprovedArticles(string userId);
        IResult<bool> Create(Article article);
        IResult<bool> Approve(long articleId, string userId);
    }
}
