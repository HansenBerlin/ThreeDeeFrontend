using System.Text.Json.Serialization;

namespace ThreeDeeInfrastructure.RequestModels;

public class UserRequestModel
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    
    [JsonPropertyName("mail")]
    public string Mail { get; set; }
}