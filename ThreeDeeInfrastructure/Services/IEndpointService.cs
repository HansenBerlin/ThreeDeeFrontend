namespace ThreeDeeInfrastructure.Services;

public interface IEndpointService
{
    string GetMappedUrl(Type responseModel);
}