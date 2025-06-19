namespace KodtestCodeunit.Services;

using KodtestCodeunit.Mappers;
using KodtestCodeunit.Models;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly ProductMapper _productMapper;

    public ApiService(HttpClient httpClient, ProductMapper productMapper)
    {
        _httpClient = httpClient;
        _productMapper = productMapper;
    }

    public async Task<GetDataResponse> GetDataAsync()
    {
        string credentials = "viggo:qwerty123";
        string base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));

        var request = new HttpRequestMessage(HttpMethod.Get, "https://codeunitdevelopertestapi.azurewebsites.net/api/GetData?code=C0RKIktWjs4N-5a2pns-dcb-cTtZZD52nM_noBmdLIRZAzFure0P8g%3D%3D");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        var result = System.Text.Json.JsonSerializer.Deserialize<GetDataResponse>(jsonString);

        if (result == null)
        {
            throw new Exception("Failed to get data");
        }
        return result;
    }

    public async Task<string> PostDataAsync(GetDataResponse getDataResponse)
    {
        try
        {
            var postModelRequest = _productMapper.MapToPostModel(getDataResponse);

            string credentials = "viggo:qwerty123";
            string base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://codeunitdevelopertestapi.azurewebsites.net/api/PostModel?code=oq8qiw5sKGorXeCfpkEQoQucTITtjDRtKm0Q2Fu74ehgAzFuwM_JFw%3D%3D");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);

            var jsonContent = System.Text.Json.JsonSerializer.Serialize(postModelRequest);
            request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to post data: {ex.Message}");
        }
    }
}