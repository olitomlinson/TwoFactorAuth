using TwoFactorAuth.Domain;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuthHost.Services
{
    public class SmsService : ISendCodeService
    {
        public void SendCode(ActiveCode activeCode)
        {
            //TODO
        }
    }
}
