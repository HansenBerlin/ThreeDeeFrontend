using System.Text.Json.Serialization;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeInfrastructure.ResponseModels;

public class ResponseBase : IResponseBase
{
    [JsonIgnore] 
    public bool IsResponseSuccess { get; init; } = true;
}