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
    }
}
