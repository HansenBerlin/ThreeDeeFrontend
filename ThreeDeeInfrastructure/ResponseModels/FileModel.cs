using ThreeDeeInfrastructure.ResponseModels;

namespace ThreeDeeApplication.Models;

public class FileModel : ResponseBase
{
    public string Id { get; set; }
    public string FullName { get; set; } = "";
    public DateTime Created { get; set; }
    public long SizeBytes { get; set; }
    public int Downloads { get; set; }
    public float AverageRating { get; set; } 
    
    public string Owner { get; set; } = "";
    public string Permission { get; set; } = "";
    //public List<int> GCodeIds { get; set; } = new();
    //public Filetype Filetype { get; set; }
}