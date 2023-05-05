using Microsoft.AspNetCore.Components;
using MudBlazor;
using ThreeDeeApplication.Enums;

namespace ThreeDeeFrontend.Components;

public partial class TimeLineItem
{
    [Parameter] 
    public string Text { get; set; }
    
    [Parameter] 
    public DateTime Timestamp { get; set; }
    
    [Parameter] 
    public FileIncident State { get; set; }

    private Severity _severity;
    private Color _color;

    protected override void OnParametersSet()
    {
        if (State == FileIncident.Positive)
        {
            _severity = Severity.Success;
            _color = Color.Success;
        }
        if (State == FileIncident.Neutral)
        {
            _severity = Severity.Info;
            _color = Color.Info;
        }
        if (State == FileIncident.Negative)
        {
            _severity = Severity.Warning;
            _color = Color.Warning;
        }
        base.OnParametersSet();
    }
}


