using System.Text.Json.Serialization;

namespace ThreeDeeInfrastructure.RequestModels;

public class FileRequestModel
{
    //[JsonPropertyName("owner_user_id")]
    
    public string fullname { get; set; }
    public string owner_user_id { get; set; }
    public long sizebytes { get; set; }
}