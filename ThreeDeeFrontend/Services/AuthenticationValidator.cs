using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using ThreeDeeInfrastructure.Repositories;
using ThreeDeeInfrastructure.RequestModels;
using ThreeDeeInfrastructure.ResponseModels;

namespace ThreeDeeFrontend.Services;

public class AuthenticationValidator
{
    public bool IsAuthenticated { get; private set;}
    public EventCallback AuthenticationStateHasChanged { get; set; }
    private string _userId = "";
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IRepository<UserResponseModel, UserRequestModel> _repository;

    
    public AuthenticationValidator(AuthenticationStateProvider authenticationStateProvider, 
        IRepository<UserResponseModel, UserRequestModel> repository)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _repository = repository;
    }

    public async Task<bool> GetAuthenticationState()
    {
        var user = await GetUser();
        bool isAuthenticated = user.Identity is not null && user.Identity.IsAuthenticated;
        if (IsAuthenticated != isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
            await AuthenticationStateHasChanged.InvokeAsync();
        }
        return IsAuthenticated;
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
        if (_userId != "" && IsAuthenticated)
        {
            return _userId;
        }
        var user = await GetUser();
        string mail = user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        var response = await _repository.Get(mail);
        if (response.IsResponseSuccess == false)
        {
            var payload = new UserRequestModel
            {
                UserName = "User " + new Random().Next(100, 1000),
                Mail = mail
            };
            response = await _repository.Insert(payload);

        }
        _userId = response.IsResponseSuccess ? response.Id : string.Empty;
        return _userId;
    }

    
    
    
}