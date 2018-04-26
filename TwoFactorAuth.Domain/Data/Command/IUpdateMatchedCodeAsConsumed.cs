using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain.Data.Command
{
    public interface IUpdateMatchedCodeAsConsumed
    {
        void Execute(MatchedCode matchedCode);
    }
}