using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using ThreeDeeInfrastructure;

namespace ThreeDeeFrontend.Services;

public class AuthenticationValidator
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _httpClient;

    public AuthenticationValidator(AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _httpClient = httpClient;
    }

    public async Task<bool> GetAuthenticationState()
    {
        var user = await GetUser();
        return user.Identity is not null && user.Identity.IsAuthenticated;
    }

    private async Task<ClaimsPrincipal> GetUser()
    {
        var authState = await _authenticationStateProvider
            .GetAuthenticationStateAsync();
        var user = authState.User;
        return user;
    }

    public async Task<string> GetUserId()
    {
        var user = await GetUser();
        string mail = user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        const string endpoint = ResourceUrls.UsersEndpoint;
        string url = $"http://localhost:8000{endpoint}/{mail}";
        var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        var response = await _httpClient.GetFromJsonAsync<UserModel>(url, options);
        return response?.Id ?? string.Empty;
    }

    public class UserModel
    {
        public string Id { get; set; }
    }
}