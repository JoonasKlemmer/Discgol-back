using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using App.DTO.v1_0;
using App.DTO.v1_0.Identity;
using Base.Test.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace App.Test.Integration;

[Collection("NonParallel")]
public class HappyFlowTests: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;



    private readonly JsonSerializerOptions camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public HappyFlowTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });




    }
   //Register
   //

   
    [Fact]// Main search page, get all discs
    public async Task Test_GetAllDiscs_ShouldReturnOk()
    {
        
        
        // Act
        var response = await _client.GetAsync("api/v1.0/Disc");
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(await response.Content.ReadAsStringAsync());
    }
    
    [Fact]// Get pages where disc was found
    public async Task Test_GetDiscFromPage_ShouldReturnOk()
    {
        // Arrange
        var allDiscsResponse = await _client.GetAsync("api/v1.0/Disc");
        var jsonResponse = await allDiscsResponse.Content.ReadAsStringAsync();
        var discs = JsonConvert.DeserializeObject<List<Disc>>(jsonResponse);
            
        // Assert 
        Assert.NotNull(discs);
        
        // Arrange
        var random = new Random();
        var discNr = random.Next(0, discs.Count);
        var discId = discs[discNr].Id;
        var discName = discs[discNr].Name;
            
        // Act 
        var discFromPageResponse = await _client.GetAsync($"api/v1.0/DiscFromPage/{discId}");
        var jsonResponse2 = await discFromPageResponse.Content.ReadAsStringAsync();
        var discWithDetails = JsonConvert.DeserializeObject<List<DiscWithDetails>>(jsonResponse2);
            
        // Assert 
        Assert.NotNull(discWithDetails);
        
        // Arrange
        var discWithDetailsNr = random.Next(0, discWithDetails!.Count);
        var discWithDetailsName = discWithDetails[discWithDetailsNr].Name;
            
        // Assert
        Assert.Equal(discName,discWithDetailsName);
    }
        
    
    [Fact]
    public async Task Test_PostDiscInWishlist_ShouldReturnUnauthorized()
    {
        // Create a WishlistInfo object with valid data
        var wishlistInfo = new WishlistInfo
        {
            DiscFromPageId = "valid-disc-id",
            WishlistId = "valid-wishlist-id"
        };

        // Serialize the WishlistInfo object to JSON
        var jsonRequest = JsonConvert.SerializeObject(wishlistInfo);

        // Create StringContent with the JSON data
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Send a POST request to the endpoint
        var response = await _client.PostAsync("api/v1.0/DiscsInWishlist/", content);

        // Assert the response status code
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task Test_PostDiscInWishlist_ShouldReturnOk()
    {
    // Perform a GET request to retrieve disc details
    var discFromPageResponse = await _client.GetAsync($"api/v1.0/DiscFromPage/");
    var jsonResponse2 = await discFromPageResponse.Content.ReadAsStringAsync();
    var discWithDetails = JsonConvert.DeserializeObject<List<DiscWithDetails>>(jsonResponse2);
    var discFromPageId = discWithDetails![0].DiscFromPageId;

    // User information
    const string email = "renewal@test.ee";
    const string firstname = "First";
    const string lastname = "Last";
    const string password = "Foo.bar1";
    const int expiresInSeconds = 3;

    // Arrange: Register user and get JWT
    var jwt = await RegisterNewUser(email, password, firstname, lastname, expiresInSeconds);
    var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, camelCaseJsonSerializerOptions);

    // Create the HttpRequestMessage to get the wishlist
    var wishlistRequest = new HttpRequestMessage(HttpMethod.Get, "api/v1.0/Wishlist");

    // Set the Bearer token
    wishlistRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

    // Send the request to get the wishlist
    var response = await _client.SendAsync(wishlistRequest);

    // Read the content of the response
    var responseContent = await response.Content.ReadAsStringAsync();

    // Deserialize the response content to a list of Wishlist objects
    var wishlistResponse = JsonSerializer.Deserialize<List<Wishlist>>(responseContent, camelCaseJsonSerializerOptions);

    // Extract wishlist ID from the response
    var wishlistId = wishlistResponse![0].Id;

    // Create WishlistInfo object for the POST request
    var wishlistInfo = new WishlistInfo()
    {
        DiscFromPageId = discFromPageId.ToString(),
        WishlistId = wishlistId.ToString()
    };

    // Create a POST request to add disc to wishlist
    var addDiscToWishlistRequest = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/DiscsInWishlist");

    // Set the Bearer token
    addDiscToWishlistRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

    // Serialize the wishlistInfo object to JSON and set it as the content of the request
    addDiscToWishlistRequest.Content = new StringContent(JsonSerializer.Serialize(wishlistInfo, camelCaseJsonSerializerOptions), Encoding.UTF8, "application/json");

    // Send the POST request to add disc to wishlist
    var postResponse = await _client.SendAsync(addDiscToWishlistRequest);

    // Log or print out the response
    var postResponseContent = await postResponse.Content.ReadAsStringAsync();
    _testOutputHelper.WriteLine($"POST Response: {postResponseContent}");

    // Ensure that the status code is OK
    Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
}
    
     [Fact]
public async Task Test_DeleteFromWishlist_ShouldReturnOk()
{
    // Get disc information
    var discFromPageResponse = await _client.GetAsync($"api/v1.0/DiscFromPage/");
    var jsonResponse = await discFromPageResponse.Content.ReadAsStringAsync();
    var discWithDetails = JsonConvert.DeserializeObject<List<DiscWithDetails>>(jsonResponse);
    var discFromPageId = discWithDetails![0].DiscFromPageId;

    // Register user and get JWT
    const string email = "renewal@test.ee";
    const string firstname = "First";
    const string lastname = "Last";
    const string password = "Foo.bar1";
    const int expiresInSeconds = 3;
    var jwt = await RegisterNewUser(email, password, firstname, lastname, expiresInSeconds);
    var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, camelCaseJsonSerializerOptions);

    // Get wishlist ID
    var wishlistRequest = new HttpRequestMessage(HttpMethod.Get, "api/v1.0/Wishlist");
    wishlistRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);
    var wishlistResponse = await _client.SendAsync(wishlistRequest);
    var wishlistContent = await wishlistResponse.Content.ReadAsStringAsync();
    var wishlist = JsonSerializer.Deserialize<List<Wishlist>>(wishlistContent, camelCaseJsonSerializerOptions);
    var wishlistId = wishlist![0].Id;

    // Create WishlistInfo object for the POST request
    var wishlistInfo = new WishlistInfo()
    {
        DiscFromPageId = discFromPageId.ToString(),
        WishlistId = wishlistId.ToString()
    };

    // Create a POST request to add disc to wishlist
    var addDiscToWishlistRequest = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/DiscsInWishlist");
    addDiscToWishlistRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);
    addDiscToWishlistRequest.Content = new StringContent(JsonSerializer.Serialize(wishlistInfo, camelCaseJsonSerializerOptions), Encoding.UTF8, "application/json");
    var postResponse = await _client.SendAsync(addDiscToWishlistRequest);
    var postResponseContent = await postResponse.Content.ReadAsStringAsync();
    var addedDiscInWishlist = JsonSerializer.Deserialize<DiscsInWishlist>(postResponseContent, camelCaseJsonSerializerOptions);
    var addedDiscId = addedDiscInWishlist!.Id;

    // Create a DELETE request to remove the added disc from wishlist
    var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"api/v1.0/DiscsInWishlist/{addedDiscId}");
    deleteRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

    // Send the DELETE request
    var deleteResponse = await _client.SendAsync(deleteRequest);

    // Ensure that the status code is NoContent
    Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
}
    
    

    [Fact]
    public async Task Test_RegisterAccount()
    {
        // Arrange 
        const string email = "register@test.ee";
        const string firstname = "firstname";
        const string lastname = "lastname";
        const string password = "Foo.bar1";
        const int expiresInSeconds = 1;
        var registrationData = JsonContent.Create(new RegisterInfo()
        {
            Firstname = firstname,
            Lastname = lastname,
            Email = email,
            Password = password
        });
        // Act
        var response = await _client.PostAsync($"api/v1.0/identity/Account/Register?expiresInSeconds={expiresInSeconds}", registrationData);
        var responseContent = await response.Content.ReadAsStringAsync();
       
        //Assert 
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent,email , firstname, lastname, DateTime.Now.AddSeconds(4).ToUniversalTime());

    }
    
    [Fact(DisplayName = "POST - login user")]
    public async Task Test_LoginUser()
    {
        const string email = "login@test.ee";
        const string firstname = "TestFirst";
        const string lastname = "TestLast";
        const string password = "Foo.bar1";
        const int expiresInSeconds = 1;
        // Arrange
        await RegisterNewUser(email, password, firstname, lastname);


         var url = $"/api/v1/identity/account/login?expiresInSeconds={expiresInSeconds}";

        var loginData = new
        {
            Email = email,
            Password = password
        };

        var data = JsonContent.Create(loginData);

        // Act
        var response = await _client.PostAsync(url, data);

        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, firstname, lastname, DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());
    }
    
    [Fact(DisplayName = "POST - JWT expired")]
    public async Task Test_JwtExpired()
    {
        const string email = "expired@test.ee";
        const string firstname = "TestFirst";
        const string lastname = "TestLast";
        const string password = "Foo.bar1";
        const int expiresInSeconds = 3;

        const string url = "/api/v1.0/DiscsInWishlist";

        // Arrange
        var jwt = await RegisterNewUser(email, password, firstname, lastname);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, camelCaseJsonSerializerOptions);


        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.True(response.IsSuccessStatusCode);

        // Arrange
        await Task.Delay((expiresInSeconds + 2) * 1000);

        var request2 = new HttpRequestMessage(HttpMethod.Get, url);
        request2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

        // Act
        var response2 = await _client.SendAsync(request2);

        // Assert
        Assert.False(response2.IsSuccessStatusCode);
    }

    
    
    
    [Fact(DisplayName = "POST - JWT renewal")]
    public async Task Test_JwtRenewal()
    {
        const string email = "renewal@test.ee";
        const string firstname = "First";
        const string lastname = "Last";
        const string password = "Foo.bar1";
        const int expiresInSeconds = 3;

        const string url = "/api/v1.0/discsinwishlist";

        // Arrange
        var jwt = await RegisterNewUser(email, password, firstname, lastname, expiresInSeconds);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, camelCaseJsonSerializerOptions);
        
        // let the jwt expire
        await Task.Delay((expiresInSeconds + 2) * 1000);

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.False(response.IsSuccessStatusCode);

        // Arrange
        var refreshUrl = $"/api/v1.0/identity/account/refreshTokenData?expiresInSeconds={expiresInSeconds}";
        var tokenRefreshInfo = new TokenRefreshInfo()
        {
            Jwt = jwtResponse.Jwt,
            RefreshToken = jwtResponse.RefreshToken,
        };

        var data =  JsonContent.Create(tokenRefreshInfo);
        
        var response2 = await _client.PostAsync(refreshUrl, data);
        var responseContent2 = await response2.Content.ReadAsStringAsync();
        
        Assert.True(response2.IsSuccessStatusCode);
        
        jwtResponse = JsonSerializer.Deserialize<JWTResponse>(responseContent2, camelCaseJsonSerializerOptions);

        var request3 = new HttpRequestMessage(HttpMethod.Get, url);
        request3.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

        // Act
        var response3 = await _client.SendAsync(request3);
        // Assert
        Assert.True(response3.IsSuccessStatusCode);
    }
    
    [Fact(DisplayName = "POST - Log out")]
    public async Task Test_LogOut()
    {
        const string email = "renewal@test.ee";
        const string firstname = "First";
        const string lastname = "Last";
        const string password = "Foo.bar1";
        const int expiresInSeconds = 3;

        const string url = "/api/v1.0/identity/account/logout";

        // Arrange
        var jwt = await RegisterNewUser(email, password, firstname, lastname, expiresInSeconds);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, camelCaseJsonSerializerOptions);

        // Create the HttpRequestMessage
        var logoutRequest = new HttpRequestMessage(HttpMethod.Post, url);

        // Set the Bearer token
        logoutRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

        // Prepare the logout info body
        var logoutInfo = new LogoutInfo
        {
            RefreshToken = jwtResponse.RefreshToken,
        };

        // Set the content of the request
        logoutRequest.Content = new StringContent(JsonSerializer.Serialize(logoutInfo, camelCaseJsonSerializerOptions), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.SendAsync(logoutRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    private async Task<string> RegisterNewUser(string email, string password, string firstname, string lastname, int expiresInSeconds = 1)
    {
        var URL = $"/api/v1.0/identity/account/register?expiresInSeconds={expiresInSeconds}";

        var registerData = new
        {
            Email = email,
            Password = password,
            Firstname = firstname,
            Lastname = lastname
        };

        var data = JsonContent.Create(registerData);
        // Act
        var response = await _client.PostAsync(URL, data);

        var responseContent = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.True(response.IsSuccessStatusCode);

        VerifyJwtContent(responseContent, email, firstname, lastname,DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());

        return responseContent;
    }
    
    private void VerifyJwtContent(string jwt, string email, string firstname, string lastname,DateTime validToIsSmallerThan)
    {
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt,camelCaseJsonSerializerOptions);
        


        Assert.NotNull(jwtResponse);
        Assert.NotNull(jwtResponse.RefreshToken);
        Assert.NotNull(jwtResponse.Jwt);

        Assert.NotNull(jwtResponse);
        Assert.NotNull(jwtResponse.RefreshToken);
        Assert.NotNull(jwtResponse.Jwt);

        // verify the actual JWT
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtResponse.Jwt);
        Assert.Equal(email, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
        Assert.Equal(firstname, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value);
        Assert.Equal(lastname, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value);
        Assert.True(jwtToken.ValidTo < validToIsSmallerThan);
    }

}
