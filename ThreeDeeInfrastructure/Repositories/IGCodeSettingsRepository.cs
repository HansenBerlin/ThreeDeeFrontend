using ThreeDeeApplication.Models;

namespace ThreeDeeInfrastructure.Repositories;

public interface IGCodeSettingsRepository
{
    List<GCodeSettingsModel> MockData { get; }
}