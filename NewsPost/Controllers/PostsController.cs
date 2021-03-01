using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NewsPost.Data.Reps;
using NewsPost.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsPost.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IRepPosts _rep;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IRepPosts rep, ILogger<PostsController> logger)
        {
            this._rep = rep;
            this._logger = logger;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public IEnumerable<IPost> Get()
        {
            this._logger.LogInformation($"Function 'Get' -> started.");
            try
            {
                return _rep.Get();
            }
            finally
            {
                this._logger.LogInformation($"Function 'Get' -> finished.");
            }
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public IPost Get(long id)
        {
            this._logger.LogInformation($"Function 'Get' -> started.");
            try
            {
                return _rep.Get(id);
            }
            finally
            {
                this._logger.LogInformation($"Function 'Get' -> finished.");
            }
        }

        // POST api/<PostsController>
        [HttpPost]
        public void Post([FromBody] PostData item)
        //public void Post([FromBody] string title, string body)
        {
            this._logger.LogInformation($"Function 'Post' -> started.");
            try
            {
                _rep.Post(item.Title, item.Body);
            }
            finally
            {
                this._logger.LogInformation($"Function 'Post' -> finished.");
            }
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string title, string body)
        {
            this._logger.LogInformation($"Function 'Put' -> started.");
            try
            {
                _rep.Put(id, title, body);
            }
            finally
            {
                this._logger.LogInformation($"Function 'Put' -> finished.");
            }
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this._logger.LogInformation($"Function 'Delete' -> started.");
            try
            {
                _rep.Delete(id);
            }
            finally
            {
                this._logger.LogInformation($"Function 'Delete' -> finished.");
            }
        }
    }
}
