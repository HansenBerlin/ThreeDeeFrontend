

using System.Diagnostics;
using System.Net.Http.Headers;
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
            var response = await _httpClient.GetFromJsonAsync<List<TResponse>>($"{_uri}/{id}", _options);
            return response ?? Enumerable.Empty<TResponse>().ToList();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<TResponse> {new() {IsResponseSuccess = false}};
        }
    }

 

    public async Task<TResponse> Get(string id)
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
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.PostAsJsonAsync(_uri, requestModel, _options);
            var responseString = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(responseString, _options) ?? new TResponse();
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

    public async Task<TResponse> Update(TRequest requestModel, string id)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_uri}/{id}", requestModel);
            var responseString = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(responseString, _options) ?? new TResponse();
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

    public async Task<bool> Delete(string id)
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