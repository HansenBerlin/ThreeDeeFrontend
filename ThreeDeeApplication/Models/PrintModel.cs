namespace ThreeDeeApplication.Models;

public class PrintModel
{
    public string Material { get; set; } = "";
    public string MaterialType { get; set; } = "";
    public string PrinterModel { get; set; } = "";
    public int SuccessfulPrints { get; set; }
    public int FailedPrints { get; set; }
    public string Notes { get; set; } = "";
    public float NozzleSize { get; set; }
    public int ExtruderTemperature { get; set; }
    public int BedTemperature { get; set; }
}