using System.Text.Json;
using ApiTestingLab3.Support;
using RestSharp;
using TechTalk.SpecFlow;

namespace ApiTestingLab3.StepDefinitions;

[Binding]
public class BookerSteps
{
    private readonly ApiContext _api;

    public BookerSteps(ApiContext api) => _api = api;

    private static object BuildBooking(string first, string last, int price) => new
    {
        firstname = first,
        lastname = last,
        totalprice = price,
        depositpaid = true,
        bookingdates = new { checkin = "2026-12-01", checkout = "2026-12-10" },
        additionalneeds = "Breakfast"
    };

    [Given("I have an auth token")]
    public async Task GivenIHaveAnAuthToken()
    {
        var req = new RestRequest("auth", Method.Post)
            .AddJsonBody(new { username = "admin", password = "password123" });
        var res = await _api.Client.ExecuteAsync(req);

        Assert.That((int)res.StatusCode, Is.EqualTo(200));
        using var doc = JsonDocument.Parse(res.Content!);
        _api.Token = doc.RootElement.GetProperty("token").GetString();
        Assert.That(_api.Token, Is.Not.Null.And.Not.Empty);
    }

    [Given(@"a booking exists for ""(.*)"" ""(.*)"" with total price (\d+)")]
    [When(@"I create a booking for ""(.*)"" ""(.*)"" with total price (\d+)")]
    public async Task CreateBooking(string first, string last, int price)
    {
        var req = new RestRequest("booking", Method.Post)
            .AddHeader("Accept", "application/json")
            .AddJsonBody(BuildBooking(first, last, price));
        _api.Response = await _api.Client.ExecuteAsync(req);

        if (_api.Response.IsSuccessful)
        {
            using var doc = JsonDocument.Parse(_api.Response.Content!);
            _api.BookingId = doc.RootElement.GetProperty("bookingid").GetInt32();
        }
    }

    [When("I get the booking by id")]
    public async Task WhenIGetTheBookingById()
    {
        var req = new RestRequest($"booking/{_api.BookingId}")
            .AddHeader("Accept", "application/json");
        _api.Response = await _api.Client.ExecuteAsync(req);
    }

    [When(@"I update the booking with firstname ""(.*)"" and total price (\d+)")]
    public async Task WhenIUpdateTheBooking(string first, int price)
    {
        var req = new RestRequest($"booking/{_api.BookingId}", Method.Put)
            .AddHeader("Accept", "application/json")
            .AddHeader("Cookie", $"token={_api.Token}")
            .AddJsonBody(BuildBooking(first, "Brown", price));
        _api.Response = await _api.Client.ExecuteAsync(req);
    }

    [When("I delete the booking")]
    public async Task WhenIDeleteTheBooking()
    {
        var req = new RestRequest($"booking/{_api.BookingId}", Method.Delete)
            .AddHeader("Cookie", $"token={_api.Token}");
        _api.Response = await _api.Client.ExecuteAsync(req);
    }

    [When("I delete the booking without a token")]
    public async Task WhenIDeleteWithoutToken()
    {
        var req = new RestRequest($"booking/{_api.BookingId}", Method.Delete);
        _api.Response = await _api.Client.ExecuteAsync(req);
    }

    [Then(@"the response status code should be (\d+)")]
    public void ThenStatusCodeShouldBe(int expected)
        => Assert.That((int)_api.Response!.StatusCode, Is.EqualTo(expected));

    [Then("the response should contain a booking id")]
    public void ThenResponseContainsBookingId()
        => Assert.That(_api.BookingId, Is.GreaterThan(0));

    [Then(@"the booking firstname should be ""(.*)""")]
    public void ThenFirstnameShouldBe(string expected)
    {
        using var doc = JsonDocument.Parse(_api.Response!.Content!);
        var root = doc.RootElement;
        if (root.TryGetProperty("booking", out var inner)) root = inner;
        Assert.That(root.GetProperty("firstname").GetString(), Is.EqualTo(expected));
    }
}