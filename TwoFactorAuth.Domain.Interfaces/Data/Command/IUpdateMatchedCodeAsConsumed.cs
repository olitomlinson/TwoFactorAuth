using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain.Interfaces.Data.Command
{
    public interface IUpdateMatchedCodeAsConsumed
    {
        void Execute(MatchedCode matchedCode);
    }
}