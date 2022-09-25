using WebBlogApi.Models;

namespace WebBlogApi.Services.Implementations;

public class BlogRepository : IRepository
{
    private List<BlogInfo> _blogInfos;

    public BlogRepository()
    {
        _blogInfos = new List<BlogInfo>
        {
			new BlogInfo{
				UserId = 13,
				Id = 1,
				Title = "ipsum",
				Body = "vulputate ullamcorper magna. Sed eu eros. Nam consequat dolor vitae dolor."
			},
			new BlogInfo{
				UserId = 10,
				Id = 2,
				Title = "leo.",
				Body = "placerat eget, venenatis a, magna. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Etiam laoreet, libero"
			},
			new BlogInfo{
				UserId = 12,
				Id = 3,
				Title = "ullamcorper",
				Body = "ut, pharetra sed, hendrerit a, arcu. Sed et libero. Proin"
			},
			new BlogInfo{
				UserId = 3,
				Id = 4,
				Title = "pellentesque.",
				Body = "primis in faucibus orci luctus et ultrices posuere cubilia Curae Donec tincidunt. Donec vitae erat vel"
			},
			new BlogInfo{
				UserId = 10,
				Id = 5,
				Title = "Aliquam",
				Body = "enim. Etiam imperdiet dictum magna. Ut tincidunt orci quis lectus. Nullam suscipit, est ac facilisis facilisis, magna"
			},
			new BlogInfo{
				UserId = 1,
				Id = 6,
				Title = "consequat",
				Body = "Cras pellentesque. Sed dictum. Proin eget odio. Aliquam vulputate ullamcorper magna. Sed eu eros. Nam consequat dolor"
			},
			new BlogInfo{
				UserId = 13,
				Id = 7,
				Title = "faucibus",
				Body = "purus, in molestie tortor nibh sit amet orci. Ut sagittis lobortis"
			},
			new BlogInfo{
				UserId = 1,
				Id = 8,
				Title = "sodales",
				Body = "ut ipsum ac mi eleifend egestas. Sed pharetra, felis eget varius"
			},
			new BlogInfo{
				UserId = 15,
				Id = 9,
				Title = "purus,",
				Body = "risus. Nunc ac sem ut dolor dapibus gravida. Aliquam tincidunt, nunc ac mattis ornare, lectus ante dictum"
			},
			new BlogInfo{
				UserId = 11,
				Id = 10,
				Title = "ullamcorper,",
				Body = "hendrerit a, arcu. Sed et libero. Proin mi. Aliquam gravida mauris ut mi. Duis risus odio, auctor vitae, aliquet"
			},
			new BlogInfo{
				UserId = 8,
				Id = 11,
				Title = "augue",
				Body = "porttitor interdum. Sed auctor odio a purus. Duis elementum, dui quis accumsan convallis, ante lectus convallis est, vitae"
			},
			new BlogInfo{
				UserId = 7,
				Id = 12,
				Title = "magna.",
				Body = "Suspendisse ac metus vitae velit egestas lacinia. Sed congue, elit sed consequat auctor, nunc nulla vulputate dui, nec"
			},
			new BlogInfo{
				UserId = 15,
				Id = 13,
				Title = "libero",
				Body = "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere"
			},
			new BlogInfo{
				UserId = 4,
				Id = 14,
				Title = "dapibus",
				Body = "ante lectus convallis est, vitae sodales nisi magna sed dui. Fusce aliquam, enim nec tempus scelerisque, lorem ipsum"
			},
			new BlogInfo{
				UserId = 15,
				Id = 15,
				Title = "dui",
				Body = "eu nulla at sem molestie sodales. Mauris blandit enim consequat purus. Maecenas libero est,"
			}
		};
    }
    public BlogInfo? GetById(int id)
    {
		return _blogInfos.FirstOrDefault(x => x.Id == id);
    }
}
