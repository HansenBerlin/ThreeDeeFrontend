using System.Text.Json.Serialization;

namespace ThreeDeeInfrastructure.RequestModels;

public class FileRequestModel
{
    public string Fullname { get; set; }
    
    [JsonPropertyName("ownerUserId")]
    public string OwnerUserId { get; set; }
    public long Sizebytes { get; set; }
}