using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain.Interfaces.Data.Command
{
    public interface IInsertActiveCodeCommand
    {
        void Execute(ActiveCode activeCode);
    }
}