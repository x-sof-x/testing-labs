using System.Text.Json;
using ApiTestingLab3.Support;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace ApiTestingLab3.StepDefinitions;

[Binding]
public class PostsSteps
{
    private readonly PostsContext _ctx;

    public PostsSteps(PostsContext ctx) => _ctx = ctx;

    // Спільний метод: виконує запит і зберігає відповідь
    private async Task Send(RestRequest request)
    {
        request.AddHeader("Accept", "application/json");
        _ctx.Response = await _ctx.Client.ExecuteAsync(request);
    }

    // ---------- WHEN: CREATE ----------

    [When(@"I create a post with title ""(.*)"" and body ""(.*)"" for user (\d+)")]
    public async Task WhenICreateAPost(string title, string body, int userId)
    {
        var request = new RestRequest("posts", Method.Post)
            .AddJsonBody(new { title, body, userId });
        await Send(request);
    }

    // ---------- WHEN: READ ----------

    [When(@"I get the post with id (\d+)")]
    public async Task WhenIGetThePost(int id)
        => await Send(new RestRequest($"posts/{id}", Method.Get));

    [When("I get all posts")]
    public async Task WhenIGetAllPosts()
        => await Send(new RestRequest("posts", Method.Get));

    [When(@"I get posts of user (\d+)")]
    public async Task WhenIGetPostsOfUser(int userId)
    {
        var request = new RestRequest("posts", Method.Get)
            .AddQueryParameter("userId", userId);
        await Send(request);
    }

    // ---------- WHEN: UPDATE ----------

    [When(@"I replace the post (\d+) with title ""(.*)"" and body ""(.*)"" for user (\d+)")]
    public async Task WhenIReplaceThePost(int id, string title, string body, int userId)
    {
        var request = new RestRequest($"posts/{id}", Method.Put)
            .AddJsonBody(new { id, title, body, userId });
        await Send(request);
    }

    [When(@"I patch the post (\d+) with title ""(.*)""")]
    public async Task WhenIPatchThePost(int id, string title)
    {
        var request = new RestRequest($"posts/{id}", Method.Patch)
            .AddJsonBody(new { title });
        await Send(request);
    }

    // ---------- WHEN: DELETE ----------

    [When(@"I delete the post (\d+)")]
    public async Task WhenIDeleteThePost(int id)
        => await Send(new RestRequest($"posts/{id}", Method.Delete));

    // ---------- THEN ----------

    [Then(@"the JSONPlaceholder status code should be (\d+)")]
    public void ThenStatusCodeShouldBe(int expected)
    {
        Assert.That(_ctx.Response, Is.Not.Null, "Запит не було виконано");
        Assert.That((int)_ctx.Response!.StatusCode, Is.EqualTo(expected),
            $"Тіло відповіді: {_ctx.Response.Content}");
    }

    [Then(@"the response field ""(.*)"" should be ""(.*)""")]
    public void ThenFieldShouldBe(string field, string expected)
    {
        using var doc = JsonDocument.Parse(_ctx.Response!.Content!);
        Assert.That(doc.RootElement.GetProperty(field).GetString(), Is.EqualTo(expected));
    }

    [Then(@"the response number ""(.*)"" should be (\d+)")]
    public void ThenNumberShouldBe(string field, int expected)
    {
        using var doc = JsonDocument.Parse(_ctx.Response!.Content!);
        Assert.That(doc.RootElement.GetProperty(field).GetInt32(), Is.EqualTo(expected));
    }

    [Then(@"the response should contain fields ""(.*)""")]
    public void ThenShouldContainFields(string fieldsCsv)
    {
        using var doc = JsonDocument.Parse(_ctx.Response!.Content!);
        foreach (var field in fieldsCsv.Split(',', StringSplitOptions.TrimEntries))
        {
            Assert.That(doc.RootElement.TryGetProperty(field, out _), Is.True,
                $"У відповіді немає поля '{field}'");
        }
    }

    [Then(@"the response array should contain (\d+) items")]
    public void ThenArrayShouldContain(int expected)
    {
        using var doc = JsonDocument.Parse(_ctx.Response!.Content!);
        Assert.That(doc.RootElement.GetArrayLength(), Is.EqualTo(expected));
    }

    [Then("the response body should be an empty object")]
    public void ThenBodyShouldBeEmptyObject()
    {
        using var doc = JsonDocument.Parse(_ctx.Response!.Content!);
        Assert.That(doc.RootElement.ValueKind, Is.EqualTo(JsonValueKind.Object));
        Assert.That(doc.RootElement.EnumerateObject().Count(), Is.EqualTo(0));
    }

    [Then(@"every post in the response should have userId (\d+)")]
    public void ThenEveryPostShouldHaveUserId(int expected)
    {
        using var doc = JsonDocument.Parse(_ctx.Response!.Content!);
        var items = doc.RootElement.EnumerateArray().ToList();
        Assert.That(items, Is.Not.Empty);
        foreach (var item in items)
        {
            Assert.That(item.GetProperty("userId").GetInt32(), Is.EqualTo(expected));
        }
    }
}