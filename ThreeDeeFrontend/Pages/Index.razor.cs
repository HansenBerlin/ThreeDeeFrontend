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

    private bool _isInitDone;
    private string _userId = "";
    private bool _isAuthenticated;

    protected override async Task OnInitializedAsync()
    {
        _isAuthenticated = await AuthenticationValidator.GetAuthenticationState();
        if (_isAuthenticated)
        {
            _userId = await AuthenticationValidator.GetUserId();
        }

        _isInitDone = true;
    }
}