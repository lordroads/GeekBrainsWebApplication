using Microsoft.AspNetCore.Mvc;
using WebBlogApi.Models;
using WebBlogApi.Services;

namespace WebBlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IRepository _repository;
        public PostsController(IRepository repository)
        {
            _repository = repository;

        }


        [HttpGet("/posts/{id}")]
        public async Task<ActionResult<BlogInfo>> GetBlog([FromRoute] int id)
        {
            BlogInfo blog =

            await Task.Run(() =>
            {
               return _repository.GetById(id);
            });

            if (blog == null)
                return BadRequest($"Not found is blog Id - {id}");

            return Ok(blog);
        }
    }
}
