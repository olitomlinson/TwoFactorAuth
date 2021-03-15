﻿using TwoFactorAuth.Domain.Core.Interfaces.Models;

namespace TwoFactorAuth.Domain.Core.Interfaces.Data.Command
{
    public interface IUpdateMatchedCodeAsConsumed
    {
        void Execute(MatchedCode matchedCode);
    }
}