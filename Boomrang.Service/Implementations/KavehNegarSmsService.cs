using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kavenegar;
using Boomrang.Service.Contracts;

namespace Boomrang.Service.Implementations
{
    public class KavehNegarOptions
    {
        public Uri Url { get; set; }
        public string ApiKey { get; set; }
    }

    public class KavehNegarSmsService : ISmsService
    {
        public IHttpClientFactory HttpClientFactory { get; set; }

        public IOptions<KavehNegarOptions> KavehNegarOptions { get; set; }

        public async Task SendSms(string phoneNumber, string text, CancellationToken cancellationToken)
        {
            try
            {
                var kavenegarApi = new KavenegarApi(KavehNegarOptions.Value.ApiKey);
                var result = kavenegarApi.Send("1000400440", phoneNumber, text);
            }
            catch (Kavenegar.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (Kavenegar.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
    }
}
