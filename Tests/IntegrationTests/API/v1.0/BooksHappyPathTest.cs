﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1._0.Identity;
using Public.DTO.v1._0.Library;
using Xunit.Abstractions;

namespace Tests.IntegrationTests.API.v1._0;

public class BooksHappyPathTest : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly string _signinURL;

    public BooksHappyPathTest(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _signinURL = "/api/v1.0/identity/account/signin";
    }

    [Fact(DisplayName = "GET - get all books")]
    public async Task TestGetAllBooks()
    {
        var adminLoginData = new SignInData
        {
            Email = "admin@opusportal.com",
            Password = "Foo.bar1"
        };
        var adminLoginDataJsonContent = JsonContent.Create(adminLoginData);

        var adminLoginResponse = await _client.PostAsync(_signinURL, adminLoginDataJsonContent);
        var responseObject = await adminLoginResponse.Content.ReadAsStringAsync();
        Assert.NotNull(responseObject);
        _testOutputHelper.WriteLine(responseObject);
        var jwtResponse = System.Text.Json.JsonSerializer.Deserialize<JWTResponse>(responseObject);
        Assert.NotNull(jwtResponse);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var response = await _client.GetAsync("api/v1.0/books");

        response.EnsureSuccessStatusCode();
        
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact(DisplayName = "POST - add a book")]
    public async Task TestAddBook()
    {
        var adminLoginData = new SignInData
        {
            Email = "admin@opusportal.com",
            Password = "Foo.bar1"
        };
        var adminLoginDataJsonContent = JsonContent.Create(adminLoginData);

        var adminLoginResponse = await _client.PostAsync(_signinURL, adminLoginDataJsonContent);
        var responseObject = await adminLoginResponse.Content.ReadAsStringAsync();
        Assert.NotNull(responseObject);
        _testOutputHelper.WriteLine(responseObject);
        var jwtResponse = System.Text.Json.JsonSerializer.Deserialize<JWTResponse>(responseObject);
        Assert.NotNull(jwtResponse);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);

        var bookData = new Book
        {
            Title = "Ujedus ja väärikus",
            Authors = "Dag Solstad"
        };
        var bookDataJsonContent = JsonContent.Create(bookData);

        var booksURL = "/api/v1.0/books";
        var postBookResponse = await _client.PostAsync(booksURL, bookDataJsonContent);
        postBookResponse.EnsureSuccessStatusCode();
    }
}