using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreStandart.Services
{
    /// <summary>
    ///   Компонент, посылающий запрос.
    /// </summary>
    public class ServicePalindromCaller : IServicePalindrom
    {
        private readonly HttpClient _httpClient;

        public ServicePalindromCaller()
        {
            _httpClient = new HttpClient();
        }


        private string ResolveUrl() => "http://localhost:5000/api/palindrom";

        public async Task<bool> CheckPalindrom(string text)
        {
            var response = await _httpClient.GetAsync($"{ResolveUrl()}?text={text}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            return bool.Parse(await response.Content.ReadAsStringAsync());
        }
    }
}
