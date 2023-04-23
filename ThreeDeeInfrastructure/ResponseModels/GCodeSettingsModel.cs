using ThreeDeeInfrastructure.ResponseModels;

namespace ThreeDeeApplication.Models;

public class GCodeSettingsModel : ResponseBase
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Author { get; set; } = "";
    public DateTime DateCreated { get; set; }
    public int SuccessfulPrints { get; set; }
    public int FailedPrints { get; set; }
    public float FilamentLengthUsed { get; set; }
    public int PrintingTimeMinutes { get; set; }
    public List<int> Prints { get; set; }
}