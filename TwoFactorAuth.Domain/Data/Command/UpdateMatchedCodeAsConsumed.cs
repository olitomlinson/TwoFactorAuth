using System;
using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain.Data.Command
{
    public class UpdateMatchedCodeAsConsumed : IUpdateMatchedCodeAsConsumed
    {
        public UpdateMatchedCodeAsConsumed()
        {
            //pass in a session obj   
        }
        public void Execute(MatchedCode matchedCode)
        {
            throw new NotImplementedException();
        }
    }

    public class UpdateActiveCodeCommand : IUpdateActiveCodeCommand
    {
        public UpdateActiveCodeCommand()
        {
            //pass in a session obj   
        }
        public void Execute(ActiveCode activeCode)
        {
            throw new NotImplementedException();
        }
    }

    public class UpdateInactiveCodeAsConsumed : IUpdateInactiveCodeAsConsumed
    {
        public void Execute(InactiveCode inactiveCode)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUpdateInactiveCodeAsConsumed
    {
        void Execute(InactiveCode inactiveCode);
    }

    public interface IUpdateActiveCodeCommand
    {
        void Execute(ActiveCode activeCode);
    }
}