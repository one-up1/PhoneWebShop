using PhoneWebShop.Api.Models;
using PhoneWebShop.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PhoneWebShop.BlazorApp
{
    public class ApiClient
    {
        private const string URL = "https://localhost:44337/api";

        private HttpClient client;

        public ApiClient()
        {
            client = new HttpClient();
        }

        public Task<IEnumerable<Phone>> GetPhones()
        {
            return client.GetFromJsonAsync<IEnumerable<Phone>>(URL + "/Phones/Search");
        }

        public Task<Phone> GetPhone(int id)
        {
            return client.GetFromJsonAsync<Phone>(URL + "/Phones/" + id);
        }

        public Task<HttpResponseMessage> DeletePhone(int id, string token)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                URL + "/Phones/Delete/" + id);
            request.Headers.Add("Authorization", "Bearer " + token);
            return client.SendAsync(request);
        }

        public Task<HttpResponseMessage> Register(RegisterUserInputModel userInfo)
        {
            return client.PostAsJsonAsync(URL + "/User/Register", userInfo);
        }

        public Task<HttpResponseMessage> Login(LoginUserInputModel userInfo)
        {
            return client.PostAsJsonAsync(URL + "/User/Login", userInfo);
        }
    }
}
