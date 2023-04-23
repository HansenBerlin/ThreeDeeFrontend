using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.RequestModels;
using ThreeDeeInfrastructure.ResponseModels;

namespace ThreeDeeInfrastructure.Services;

public class EndpointService : IEndpointService
{
    private readonly string _serviceUrl;

    public EndpointService()
    {
        string? url = Environment.GetEnvironmentVariable("SERVICE_URL");
        _serviceUrl = url ?? ResourceUrls.FallbackUrl;
    }
    
    public string GetMappedUrl(Type responseModel)
    {
        if (ReferenceEquals(responseModel, typeof(FileModel))) return _serviceUrl + ResourceUrls.Files;
        if (ReferenceEquals(responseModel, typeof(UserResponseModel))) return _serviceUrl + ResourceUrls.Users;
        if (ReferenceEquals(responseModel, typeof(GCodeSettingsModel))) return _serviceUrl + ResourceUrls.GCodeFilesInfo;
        return _serviceUrl;
    }
}