using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain
{
    public interface ISendCodeService
    {
        void SendCode(ActiveCode activeCode);
    }
}