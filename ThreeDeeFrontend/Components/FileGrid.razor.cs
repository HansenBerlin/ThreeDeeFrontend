
using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.ViewModels;

namespace ThreeDeeFrontend.Components;

public partial class FileGrid
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    public IFilesGridViewModel Vm { get; set; }
    
    [Parameter]
    public string UserId { get; set; }

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
            Vm.FilesChanged = EventCallback.Factory
                .Create(this, async () => await InvokeAsync(StateHasChanged));
            await Vm.Init(UserId);
        }
    }
    
    private void OnButtonClicked(string fileId)
    {
        NavigationManager.NavigateTo($"/model/{fileId}");
    }
}