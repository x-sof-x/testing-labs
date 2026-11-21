using RestSharp;

namespace ApiTestingLab3.Support;

public class PostsContext
{
    public RestClient Client { get; } = new RestClient("https://jsonplaceholder.typicode.com");
    public RestResponse? Response { get; set; }
}