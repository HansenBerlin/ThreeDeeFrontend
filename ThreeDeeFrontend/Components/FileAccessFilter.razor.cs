using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;

namespace ThreeDeeFrontend.Components;

public partial class FileAccessFilter
{
    [Parameter] 
    public EventCallback<Filetype> FiletypeHasChanged { get; set; }
    
    [Parameter] 
    public Filetype Status { get; set; }

    private async Task OnStatusButtonClicked(Filetype status)
    {
        Status = status;
        await FiletypeHasChanged.InvokeAsync(Status);
    }
    
}