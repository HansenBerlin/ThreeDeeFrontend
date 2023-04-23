using ThreeDeeApplication.Models;

namespace ThreeDeeInfrastructure.Repositories;

public interface IFileRepository
{
    Task<List<FileModel>> GetAll();
    Task<FileModel> Get(int id);
    Task<bool> Create(FileModel file);
    Task<bool> Update(FileModel file);
    Task<bool> Delete(int id);
    List<FileModel> MockData { get; }

}