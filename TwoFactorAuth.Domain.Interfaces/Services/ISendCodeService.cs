using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain
{
    public interface ISendCodeService
    {
        void SendCode(ActiveCode activeCode);
    }
}