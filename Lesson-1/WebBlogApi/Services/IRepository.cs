using WebBlogApi.Models;

namespace WebBlogApi.Services;

public interface IRepository
{
    BlogInfo GetById(int id);
}
