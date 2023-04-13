using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor.Utilities;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.Components;
using ThreeDeeInfrastructure.Repositories;
using ThreeDeeInfrastructure.ResponseModels;

namespace ThreeDeeFrontend.Pages;

public partial class FileCard
{
    [Parameter]
    public int Id { get; set; }
    

    private FileModel _file = new FileModel();
    private bool _avoidRendering;
    private bool _isColorPickerOpen;
    private bool _isInitDone;
    private MudColor _color = new("#03A9F4");
    private readonly Random _random = new();
    private double _progress;
    private ModelRenderer _modelRendererRef;

    protected override async Task OnParametersSetAsync()
    {
        //_file = await FileRepository.Get(Id);
    }

    private async Task ProgressHasChangedCallback(double progress)
    {
        _progress = progress;
        await InvokeAsync(StateHasChanged);
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