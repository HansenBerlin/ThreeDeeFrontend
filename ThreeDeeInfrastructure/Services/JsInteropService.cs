using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ThreeDeeInfrastructure.Services;

public class JsInteropService<T> : IJsInteropService<T> where T : class
{
    private readonly Lazy<Task<IJSObjectReference>> _jsApiModule;
    
    public JsInteropService(IJSRuntime jsRuntimeService)
    {
        _jsApiModule = new Lazy<Task<IJSObjectReference>>(() => jsRuntimeService
            .InvokeAsync<IJSObjectReference>("import", "./js/renderController.js").AsTask());
    }

    public async Task AddFile(string path, string color, int id, DotNetObjectReference<T> dotnetObjRef)
    {
        IJSObjectReference module = await _jsApiModule.Value;
        await module.InvokeVoidAsync("addModel", path, color, id, dotnetObjRef);
    }

    public async Task ChangeColor(string newColor, int id)
    {
        IJSObjectReference module = await _jsApiModule.Value;
        await module.InvokeVoidAsync("changeToColor", newColor, id);
    }
}