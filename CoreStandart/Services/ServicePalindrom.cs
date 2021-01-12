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
            for (int i = 0; i < str.Length / 2; i++)

                if (str[i] != str[str.Length - i - 1])
                    return false;
            return true;
        }

    }
}
