using System.Text.Json.Serialization;

namespace ThreeDeeInfrastructure.RequestModels;

public class FileRequestModel
{
    [JsonPropertyName("fullname")]
    public string Fullname { get; set; }
    
    [JsonPropertyName("ownerUserId")]
    public string OwnerUserId { get; set; }
    
    [JsonPropertyName("sizebytes")]
    public long Sizebytes { get; set; }
}