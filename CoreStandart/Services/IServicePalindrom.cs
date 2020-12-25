using System.Threading.Tasks;

namespace CoreStandart.Services
{
    /// <summary>
    ///   Интерфейс, реализующий алгоритм, проверяющий строку на палиндром.
    /// </summary>
    public interface IServicePalindrom
    {
        Task<bool> CheckPalindrom(string text);
    }
}
