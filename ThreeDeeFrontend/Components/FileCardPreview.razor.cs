using System;
using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;
using ThreeDeeApplication.Models;

namespace ThreeDeeFrontend.Components;

public partial class FileCardPreview
{
    [Parameter] 
    public FileModel File { get; set; }
    
    [Parameter] 
    public EventCallback<string> ButtonClicked { get; set; }
    

}