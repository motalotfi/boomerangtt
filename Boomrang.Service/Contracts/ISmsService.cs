using System.Threading;
using System.Threading.Tasks;

namespace Boomrang.Service.Contracts
{
    public interface ISmsService
    {
        Task SendSms(string phoneNumber, string text, CancellationToken cancellationToken);
    }
}
