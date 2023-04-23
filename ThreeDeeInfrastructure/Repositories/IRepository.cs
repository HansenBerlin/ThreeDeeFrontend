namespace ThreeDeeInfrastructure.Repositories;

public interface IRepository<TResponse, in TRequest> where TResponse : class where TRequest : class
{
    Task<IEnumerable<TResponse>> GetAll();
    Task<IEnumerable<TResponse>> GetAll(string id);
    Task<TResponse> Get(string id);
    Task<TResponse> Insert(TRequest requestModel);
    Task<TResponse> Update(TRequest requestModel, string id);
    Task<bool> Delete(string id);
}