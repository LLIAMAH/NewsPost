﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using NewsPost.Data.Entities;

namespace NewsPost.Data.Reps
{
    public interface IRep: IRepUser, IRepNews { }

    public interface IResult<out T>
    {
        string Error { get; }
        T Data { get; }
    }

    public interface IRepUser
    {
        ApplicationUser GetUser(IPrincipal principal);
        string GetUserId(IPrincipal principal);
    }

    public interface IRepNews
    {
        IResult<IEnumerable<Article>> GetNewsDaily(DateTime dateTime);
    }
}