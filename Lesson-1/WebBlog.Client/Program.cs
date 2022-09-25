using Newtonsoft.Json;
using WebBlogApi.Models;

int startId = 4;

HttpClient client = new HttpClient();

Console.WriteLine("Press any key to start....");
Console.ReadKey(true);

try
{
    for (int i = startId; i < startId + 10; i++)
    {
        HttpResponseMessage responseMessage = await client.GetAsync($"http://localhost:5277/posts/{i}");

        responseMessage.EnsureSuccessStatusCode();

        string responseBody = await responseMessage.Content.ReadAsStringAsync();

        BlogInfo blog = JsonConvert.DeserializeObject<BlogInfo>(responseBody);

        await File.AppendAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "result.txt"), blog.ToString());
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\n[ERROR]: {ex.Message}");
}







Console.WriteLine("Press any key to exit....");
Console.ReadKey(true);