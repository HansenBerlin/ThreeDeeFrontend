using ThreeDeeApplication.Models;

namespace ThreeDeeInfrastructure.Repositories;

public class GCodeSettingsRepository : IGCodeSettingsRepository
{
    public List<GCodeSettingsModel> MockData { get; } = new()
    {
        new()
        {
            Id = 1,
            Name = "Banshee",
            SuccessfulPrints = 10,
            FailedPrints = 2
        },
        new ()
        {
            Id = 2,
            Name = "PrinterTest",
            SuccessfulPrints = 5,
            FailedPrints = 1
        },
        new()
        {
            Id = 3,
            Name = "Bolt",
            SuccessfulPrints = 8,
            FailedPrints = 0
        },
         new GCodeSettingsModel
        {
            Id = 4,
            Name = "PrinterTest",
            SuccessfulPrints = 12,
            FailedPrints = 3
        },
        new GCodeSettingsModel
        {
            Id = 5,
            Name = "Banshee",
            SuccessfulPrints = 7,
            FailedPrints = 2
        },
        new GCodeSettingsModel
        {
            Id = 6,
            Name = "Bolt",
            SuccessfulPrints = 9,
            FailedPrints = 1
        },
        new GCodeSettingsModel
        {
            Id = 7,
            Name = "Banshee",
            SuccessfulPrints = 11,
            FailedPrints = 2
        },
        new GCodeSettingsModel
        {
            Id = 8,
            Name = "PrinterTest",
            SuccessfulPrints = 6,
            FailedPrints = 1
        },
        new GCodeSettingsModel
        {
            Id = 9,
            Name = "Bolt",
            SuccessfulPrints = 10,
            FailedPrints = 2
        }
    };
}