using RestSharp;

namespace ApiTestingLab3.Support;

public class ApiContext
{
    public RestClient Client { get; } = new RestClient("https://restful-booker.herokuapp.com");
    public string? Token { get; set; }
    public int BookingId { get; set; }
    public RestResponse? Response { get; set; }
}