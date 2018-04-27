using System.Collections.Generic;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain
{
    public class ActiveCodeResolutionResult
    {
        public ActiveCode ActiveCode { get; set; }
        public List<UnclassifiableCode> UnclassifiableCodes { get; set; } = new List<UnclassifiableCode>();
    }
}