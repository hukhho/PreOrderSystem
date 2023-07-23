//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Extensions.Configuration;
//using PreOrderPlatform.Service.Services.AuthService;
//using System.Net;
//using System.Net.Http.Headers;

//public class GetBusinessByIdTests : IClassFixture<WebApplicationFactory<Program>>
//{
//    private readonly WebApplicationFactory<Program> _factory;
//    private readonly IJwtService _jwtService;

//    public GetBusinessByIdTests(WebApplicationFactory<Program> factory)
//    {
//        _factory = factory.WithWebHostBuilder(builder =>
//        {
//            builder.ConfigureServices(services =>
//            {
//                // configure your services for testing here if needed
//            });
//        });

//        _jwtService = new JwtService(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
//    }

//    [Fact]
//    public async Task GetBusinessById_AsAdmin_ReturnsSuccessAndCorrectContentType()
//    {
//        // Arrange
//        var client = _factory.CreateClient();
//        var userId = "837E161A-7508-45A0-0817-08DB855C3B56"; // Admin 1
//        var businessId = "5793880B-F4FD-4275-61A4-08DB855C3B63"; //  Business 1
//        var userName = "testUser";
//        var role = "ADMIN";
//        var token = _jwtService.GenerateToken(userId, userName, role);
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//        // Act
//        var response = await client.GetAsync($"/api/business/{businessId}");

//        // Assert
//        response.EnsureSuccessStatusCode(); // Status Code 200-299
//        Assert.Equal("application/json; charset=utf-8",
//            response.Content.Headers.ContentType.ToString());
//    }

//    [Fact]
//    public async Task GetBusinessById_AsBusinessOwner_ReturnsSuccessAndCorrectContentType()
//    {
//        // Arrange
//        var client = _factory.CreateClient();
//        var userId = "57CE4E78-E746-4604-0821-08DB855C3B56"; // Owner of business 1
//        var businessId = "5793880B-F4FD-4275-61A4-08DB855C3B63"; // Business 1
//        var userName = "testUser";
//        var role = "BUSINESS_OWNER";
//        var token = _jwtService.GenerateToken(userId, userName, role);
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//        // Act
//        var response = await client.GetAsync($"/api/business/{businessId}");

//        // Assert
//        response.EnsureSuccessStatusCode(); // Status Code 200-299
//        Assert.Equal("application/json; charset=utf-8",
//            response.Content.Headers.ContentType.ToString());
//    }

//    [Fact]
//    public async Task GetBusinessById_AsBusinessStaff_ReturnsForbidden()
//    {
//        // Arrange
//        var client = _factory.CreateClient();
//        var userId = "2805D01F-81DC-4407-0822-08DB855C3B56"; // Staff1
//        var businessId = "5793880B-F4FD-4275-61A4-08DB855C3B63"; // Business 1
//        var userName = "testUser";
//        var role = "BUSINESS_STAFF";
//        var token = _jwtService.GenerateToken(userId, userName, role);
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//        // Act
//        var response = await client.GetAsync($"/api/business/{businessId}");

//        // Assert
//        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
//    }

//    [Fact]
//    public async Task GetBusinessById_AsCustomer_ReturnsForbidden()
//    {
//        // Arrange
//        var client = _factory.CreateClient();
//        var userId = "0212B367-A6C9-4C94-081C-08DB855C3B56"; //Customer 1
//        var businessId = "5793880B-F4FD-4275-61A4-08DB855C3B63"; // Business 1
//        var userName = "testUser";
//        var role = "CUSTOMER";
//        var token = _jwtService.GenerateToken(userId, userName, role);
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//        // Act
//        var response = await client.GetAsync($"/api/business/{businessId}");

//        // Assert
//        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
//    }
//}