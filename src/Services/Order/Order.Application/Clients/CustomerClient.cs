using Order.Application.Contracts;
using System.Text.Json;

namespace Order.Application.Clients
{
    public interface ICustomerClient
    {
        Task<ResponseCustomerData?> GetCustomerById(Guid customerId);
    }

    public class CustomerClient : ICustomerClient
    {
        private readonly HttpClient _httpClient;

        public CustomerClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<ResponseCustomerData?> GetCustomerById(Guid customerId)
        {
            var customerReponse = await _httpClient.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri($"http://apigateway:80/customer/{customerId}"),
                Method = HttpMethod.Get,
            });

            if (!customerReponse.IsSuccessStatusCode)
                return null;

            var str = await customerReponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResponseCustomerData>(str);
        }
    }
}
