using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IServicePalindrom
    {
        Task<bool> CheckPalindrom(string text);
    }

    public class ServicePalindrom : IServicePalindrom
    {
        public Task<bool> CheckPalindrom(string text)
            => Task.FromResult(IsPalindrom(text));

        bool IsPalindrom(string str)
        {
            if (str == null) return false;
            str = str.ToLower().Replace(" ", string.Empty);
            return IsPalindromInternal(str);
        }

        bool IsPalindromInternal(string str)
        {
            if (str.Length == 1 || string.IsNullOrEmpty(str)) return true;
            if (!str[0].Equals(str[str.Length - 1])) return false;
            return IsPalindromInternal(str.Substring(1, str.Length - 2));
        }
    }

    public class ServicePalindromCaller : IServicePalindrom
    {
        private readonly HttpClient _httpClient;

        public ServicePalindromCaller()
        {
            _httpClient = new HttpClient();
        }


        private string ResolveUrl() => "http://localhost:5000/api";

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
