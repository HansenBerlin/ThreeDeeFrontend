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
    public FileModel File { get; set; }
    
    private List<GCodeSettingsModel> _gCodes = new List<GCodeSettingsModel>();


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            //_gCodes = await GCodeSettingsRepository.GetAll(File.Id);
            int random = new Random().Next(3, 30);
            for (int i = 0; i < random; i++)
            {
                _gCodes.Add(new()
                {
                    Author = "Someone",
                    DateCreated = DateTime.Today - TimeSpan.FromDays(new Random().Next(0, 100)),
                    FailedPrints = new Random().Next(10, 100),
                    SuccessfulPrints = new Random().Next(30, 300),
                    PrintingTimeMinutes = new Random().Next(6, 600),
                    FilamentLengthUsed = new Random().Next(1, 1000)
                });
            }
            await InvokeAsync(StateHasChanged);
        }
    }


    private void OnButtonClicked(string fileName)
    {
        NavigationManager.NavigateTo($"/analyzer/{File.FullName}");
    }
}