using Client.Models;
using CoreStandart.Services;
using System;
using System.Threading.Tasks;

namespace Client.Helpers
{
    public class RequestHelper
    {
        private readonly IServicePalindrom _servicePalindrom;

        public RequestHelper(IServicePalindrom servicePalindrom)
        {
            _servicePalindrom = servicePalindrom;
        }

        public async Task<ResponseResult> SendRequest(string text, Guid id)
        {
            var haveResult = false;
            bool result = false;
            //Отправляем запросы, пока на все не получим ответ
            while (!haveResult)
            {
                try
                {
                    result = await _servicePalindrom.CheckPalindrom(text);
                    haveResult = true;
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                catch
                { }
            }

            return new ResponseResult { DocumentId = id, Answer = result };
        }

     

    }
}
