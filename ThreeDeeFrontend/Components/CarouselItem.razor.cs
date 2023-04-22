using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ThreeDeeFrontend.Components;

public partial class CarouselItem
{
    [Parameter] public string Header { get; set; } = "";
    [Parameter] public string Content { get; set; } = "";
    [Parameter] public string ImagePath { get; set; }
    [Parameter] public string ImageBackground { get; set; }
    [Parameter] public Color Color { get; set; } = Color.Primary;
    [Parameter] public bool IsImageBorderless { get; set; } = true;
}