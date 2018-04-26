using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain.Data.Command
{
    public interface IInsertActiveCodeCommand
    {
        void Execute(ActiveCode activeCode);
    }
}