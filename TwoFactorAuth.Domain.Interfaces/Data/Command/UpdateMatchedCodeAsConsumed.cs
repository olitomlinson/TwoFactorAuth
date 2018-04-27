using System;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain.Interfaces.Data.Command
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