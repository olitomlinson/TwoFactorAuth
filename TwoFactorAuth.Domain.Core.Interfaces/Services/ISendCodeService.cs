using TwoFactorAuth.Domain.Core.Interfaces.Models;

namespace TwoFactorAuth.Domain.Core
{
    public interface ISendCodeService
    {
        void SendCode(ActiveCode activeCode);
    }
}