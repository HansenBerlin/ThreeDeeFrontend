

using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using ThreeDeeInfrastructure.ResponseModels;
using ThreeDeeInfrastructure.Services;


namespace ThreeDeeInfrastructure.Repositories;

 

public class Repository<TResponse, TRequest> : IRepository<TResponse, TRequest>
    where TResponse : ResponseBase, new()
    where TRequest : class, new()
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;
    private readonly string _uri;

 


    public Repository(HttpClient httpClient, IEndpointService endpointService)
    {
        _httpClient = httpClient;
        _uri = endpointService.GetMappedUrl(typeof(TResponse));
        _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

 

    public async Task<IEnumerable<TResponse>> GetAll()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<TResponse>>($"{_uri}", _options);
            return response ?? Enumerable.Empty<TResponse>().ToList();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<TResponse> {new() {IsResponseSuccess = false}};
        }
    }
    
    public async Task<IEnumerable<TResponse>> GetAll(string id)
    {
        try
        {
            var response2 = await _httpClient.GetAsync($"{_uri}/{id}");
            var test = await response2.Content.ReadAsStringAsync();
            var response = await _httpClient.GetFromJsonAsync<List<TResponse>>($"{_uri}/{id}", _options);
            return response ?? Enumerable.Empty<TResponse>().ToList();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<TResponse> {new() {IsResponseSuccess = false}};
        }
    }

 

    public async Task<TResponse> Get(int id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<TResponse>($"{_uri}/{id}", _options);
            return response ?? new TResponse();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse
            {
                IsResponseSuccess = false
            };
        }
    }

    
 

    public async Task<TResponse> Insert(TRequest requestModel)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_uri, requestModel);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString) ?? new TResponse();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse
            {
                IsResponseSuccess = false
            };
        }
    }

 

    public async Task<TResponse> Update(TRequest requestModel, int id)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_uri}/{id}", requestModel);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString) ?? new TResponse();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse
            {
                IsResponseSuccess = false
            };
        }
    }

 

    public async Task<bool> Delete(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_uri}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }
}
