using System.Collections.Generic;
using TwoFactorAuth.Domain.Core.Interfaces.Models;

namespace TwoFactorAuth.Domain.Core
{
    public class ActiveCodeResolutionResult
    {
        public ActiveCode ActiveCode { get; set; }
        public List<UnclassifiableCode> UnclassifiableCodes { get; set; } = new List<UnclassifiableCode>();
    }
}