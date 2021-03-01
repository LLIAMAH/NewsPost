using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NewsPost.Data.Entities;
using NewsPost.Models;

namespace NewsPost.Data.Reps
{
    public interface IRepPosts
    {
        IEnumerable<PostData> Get();
        IPost Get(long id);
        void Post(string title, string body);
        void Put(int id, string title, string body);
        void Delete(int id);
    }

    public class Reps: IRepPosts
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly ILogger<Reps> _logger;

        public Reps(ApplicationDbContext ctx, ILogger<Reps> logger)
        {
            _ctx = ctx;
            _logger = logger;
            var mapperConfig = new MapperConfiguration(args =>
            {
                args.CreateMap<Post, PostData>();
            });

            this._mapper = mapperConfig.CreateMapper();
        }

        public IEnumerable<PostData> Get()
        {
            var data = this._ctx.Posts.ToList();
            var result = this._mapper.Map<List<PostData>>(data);
            return result;
        }

        public IPost Get(long id)
        {
            var data = this._ctx.Posts.SingleOrDefault(o => o.Id == id);
            var result = this._mapper.Map<PostData>(data);
            return result;
        }

        public void Post(string title, string body)
        {
            var existing = this._ctx.Posts.SingleOrDefault(o => o.Title.Equals(title));
            if (existing != null)
                return;

            try
            {
                var item = new Post()
                {
                    Title = title,
                    Body = body,
                    DateCreated = DateTime.UtcNow,
                    DateEdited = null
                };

                this._ctx.Posts.Add(item);
                this._ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Post data unsuccessful: {ex.Message}.{Environment.NewLine}Source: {ex.Source}.{Environment.NewLine}Stack: {ex.StackTrace}");
            }
        }

        public void Put(int id, string title, string body)
        {
            var existing = this._ctx.Posts.SingleOrDefault(o => o.Id == id);
            if (existing == null)
                return;

            try
            {
                existing.Title = title;
                existing.Body = body;
                existing.DateEdited = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Put data unsuccessful: {ex.Message}.{Environment.NewLine}Source: {ex.Source}.{Environment.NewLine}Stack: {ex.StackTrace}");
            }
        }

        public void Delete(int id)
        {
            var existing = this._ctx.Posts.SingleOrDefault(o => o.Id == id);
            if (existing == null)
                return;

            try
            {
                this._ctx.Posts.Remove(existing);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Delete data unsuccessful: {ex.Message}.{Environment.NewLine}Source: {ex.Source}.{Environment.NewLine}Stack: {ex.StackTrace}");
            }
        }
    }
}
