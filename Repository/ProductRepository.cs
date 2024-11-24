using ConnectingAPIs.DTO;
using ConnectingAPIs.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace ConnectingAPIs.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly AppSettings _appSettings;
        public ProductRepository(IHttpClientFactory htttpClient, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _httpClient = htttpClient;
        }

        public async Task<ResponseDetail> AddProduct(AddProductDTO _adddto)
        {
            var response = new ResponseDetail();
            try
            {
                //Create client
                var client = _httpClient.CreateClient();
                //Serialise object to json
                var jsonBody = JsonConvert.SerializeObject(_adddto);

                var content = new StringContent(jsonBody,Encoding.UTF8,"application/json");
                var url = $"{_appSettings.ThirdPartyBaseUrl}{_appSettings.AddProductUrl}";

                //Call the deployement username and password
                var username = _appSettings.Username;
                var password = _appSettings.Password;

                //Using Basic Auth
                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                //Adding the Auth to the header 
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",authToken);
                

                var request = await client.PostAsync(url,content);
                if (!request.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
                var responseContent = await request.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<ResponseDetail>(responseContent);
                if(jsonResponse is null)
                {
                    throw new JsonSerializationException(responseContent);
                }
                response = jsonResponse;

                return response;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseDetail> DeleteProduct(int id)
        {
            var response = new ResponseDetail();
            try
            {
                var client = _httpClient.CreateClient();
                var url = $"{_appSettings.ThirdPartyBaseUrl}{_appSettings.GetProductUrlbyID}{id}";
                var username = _appSettings.Username;
                var password = _appSettings.Password;
                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var request = await client.DeleteAsync(url);
                if (request.IsSuccessStatusCode == false || request == null)
                {
                    throw new HttpRequestException();
                }
                var responseContent = await request.Content.ReadAsStringAsync();

                var jsonResponse = JsonConvert.DeserializeObject<ResponseDetail>(responseContent);
                if (jsonResponse == null)
                {
                    throw new Exception(responseContent);
                }
                response = jsonResponse;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
                return response;
        }

        public async Task<List<Result>> GetAllProducts()
        { 
            var response = new List<Result>();
            try
            {
                var client = _httpClient.CreateClient();
                var url = $"{_appSettings.ThirdPartyBaseUrl}{_appSettings.GetAllProductUrl}";
                var username = _appSettings.Username;
                var password = _appSettings.Password;
                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var request = await client.GetAsync(url);
                if (!request.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();

                }
                var responseContent = await request.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<List<Result>>(responseContent);
                if (jsonResponse == null)
                {
                    throw new Exception(responseContent);
                }
                response =  jsonResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
                return response;
        }

        public async Task<ResponseDetail> GetProductByID(int id)
        {
            var response = new ResponseDetail();
            try
            {
                var client = _httpClient.CreateClient();
               // var jsonBody = JsonSerializer.Serialize(id);
             //   var stringContent = new StringContent(jsonBody,Encoding.UTF8,"application/json");
                var url = $"{_appSettings.ThirdPartyBaseUrl}{_appSettings.GetProductUrlbyID}{id}";
                var username = _appSettings.Username;
                var password = _appSettings.Password;
                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var request = await client.GetAsync(url);
                if (!request.IsSuccessStatusCode || request == null) 
                {
                    throw new HttpRequestException();
                }
                var responseContent = await request.Content.ReadAsStringAsync();

                var jsonResponse = JsonConvert.DeserializeObject<ResponseDetail>(responseContent);
                if(jsonResponse == null)
                {
                    throw new Exception(responseContent);
                }
                response = jsonResponse;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
                return response;
        }

        public async Task<ResponseDetail> UpdateProduct(UpdateProductDTO _updatedto)
        {
            var response = new ResponseDetail();
            try
            {
                var client = _httpClient.CreateClient();
                var jsonBody = JsonConvert.SerializeObject(_updatedto);
                var content = new StringContent(jsonBody,Encoding.UTF8,"application/json");
                var url = $"{_appSettings.ThirdPartyBaseUrl}{_appSettings.GetAllProductUrl}";
                var username = _appSettings.Username;
                var password = _appSettings.Password;
                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var request = await client.PutAsync(url,content);
                if (!request.IsSuccessStatusCode || request == null)
                {
                    throw new HttpRequestException();
                }
                var responseContent = await request.Content.ReadAsStringAsync();
                

                var jsonResponse = JsonConvert.DeserializeObject<ResponseDetail>(responseContent);
                if (jsonResponse == null)
                {
                    throw new JsonSerializationException(responseContent);
                }
                response = jsonResponse;
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
