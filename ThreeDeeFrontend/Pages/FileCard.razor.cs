using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor.Utilities;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.Components;
using ThreeDeeInfrastructure.Repositories;
using ThreeDeeInfrastructure.RequestModels;

namespace ThreeDeeFrontend.Pages;

public partial class FileCard
{
    [Parameter]
    public string Id { get; set; }
    
    [Parameter]
    public string UserId { get; set; }
    
    private FileModel _file = new();
    private bool _avoidRendering;
    private bool _isColorPickerOpen;
    private bool _isInitDone;
    private MudColor _color = new("#03A9F4");
    private double _progress;
    private ModelRenderer _modelRendererRef;
    
    [Inject]
    public IRepository<FileModel, FileRequestModel> FileRepository { get; set; }

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
           // _file = await FileRepository.Get($"{Id}/{UserId}");
            _file = await FileRepository.Get(Id);
            _isInitDone = true;
            StateHasChanged();
        }
    }

    private async Task ProgressHasChangedCallback(double progress)
    {
        _progress = progress;
        StateHasChanged();
    }

    private async Task UpdateColor(MudColor color)
    {
        _color = color;
        await _modelRendererRef.ChangeColor(color.Value);
    }
    
    IList<IBrowserFile> _files = new List<IBrowserFile>();
    private void UploadGCode(IReadOnlyList<IBrowserFile> files)
    {
        foreach (var file in files)
        {
            _files.Add(file);
        }
        //TODO upload the files to the server
    }
}