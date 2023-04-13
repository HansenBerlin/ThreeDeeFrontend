using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ThreeDeeInfrastructure.Services;

public interface IJsInteropService<T> where T : class
{
    Task AddFile(string path, string color, string id, DotNetObjectReference<T> dotnetObjRef);
    Task ChangeColor(string newColor, string id); 
}