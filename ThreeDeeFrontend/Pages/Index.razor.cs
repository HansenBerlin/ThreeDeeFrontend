using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.Services;
using ThreeDeeFrontend.ViewModels;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Pages;

public partial class Index
{
    [Inject]
    public AuthenticationValidator AuthenticationValidator { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private bool _isInitDone;
    private string _userId = "";
    private bool _isAuthenticated;

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
            _isAuthenticated = await AuthenticationValidator.GetAuthenticationState();
            if (_isAuthenticated)
            {
                _userId = await AuthenticationValidator.GetUserId();
            }
            else
            {
                NavigationManager.NavigateTo("/landing-page");
            }

            if (_userId != "")
            {
                _isInitDone = true;
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                // add user to db
            }
        }
    }
}