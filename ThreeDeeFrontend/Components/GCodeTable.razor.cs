using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Components;

public partial class GCodeTable
{
    [Inject]
    private IRepository<GCodeSettingsModel, GCodeSettingsModel> GCodeSettingsRepository { get; set; }
    
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Parameter]
    public string Id { get; set; }
    
    private IEnumerable<GCodeSettingsModel> _gCodes = new List<GCodeSettingsModel>();


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _gCodes = await GCodeSettingsRepository.GetAll(Id);
            await InvokeAsync(StateHasChanged);
        }
    }


    private void OnButtonClicked(string fileName)
    {
        NavigationManager.NavigateTo($"/analyzer/{fileName}");
    }
}