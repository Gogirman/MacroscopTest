using CoreStandart.Services;
using System.Threading.Tasks;

namespace CoreStandart.Services
{
    /// <summary>
    ///   Алгоритм
    /// </summary>
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
}
