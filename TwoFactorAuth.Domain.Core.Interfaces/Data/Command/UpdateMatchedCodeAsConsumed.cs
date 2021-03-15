using System;
using TwoFactorAuth.Domain.Core.Interfaces.Models;

namespace TwoFactorAuth.Domain.Core.Interfaces.Data.Command
{
    public interface IUpdateInactiveCodeAsConsumed
    {
        void Execute(InactiveCode inactiveCode);
    }

    public interface IUpdateActiveCodeCommand
    {
        void Execute(ActiveCode activeCode);
    }
}