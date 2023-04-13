using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThreeDeeInfrastructure.Services;

namespace ThreeDeeFrontend.Components;

public partial class ModelRenderer
{
    [Parameter]
    public string ColorValue { get; set; }

    [Parameter]
    public string Name { get; set; }
    
    [Parameter]
    public string Id { get; set; }
    
    [Parameter]
    public EventCallback<double> ProgressHasChanged { get; set; }
    
    [Inject]
    public IJsInteropService<ModelRenderer> JsInteropService { get; set; }
    private DotNetObjectReference<ModelRenderer> objRef;

    public async Task ChangeColor(string newColor)
    {
        await JsInteropService.ChangeColor(newColor[..7], Id);
    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            objRef = DotNetObjectReference.Create(this);
            await JsInteropService.AddFile($"../assets/{Name}.stl", ColorValue, Id, objRef);
        }
    }

    [JSInvokable]
    public async Task ProgressChangedJsCallback(double progress)
    {
        await ProgressHasChanged.InvokeAsync(progress);
    }
    
}