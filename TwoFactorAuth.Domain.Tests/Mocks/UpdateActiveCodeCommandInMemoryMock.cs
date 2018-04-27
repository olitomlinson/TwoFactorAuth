using System.Collections.Generic;
using TwoFactorAuth.Domain.Interfaces.Data.Command;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain.Tests.Mocks
{
    public class UpdateActiveCodeCommandInMemoryMock : IUpdateActiveCodeCommand
    {
        private readonly List<CodeMock> _state;

        public UpdateActiveCodeCommandInMemoryMock(List<CodeMock> state)
        {
            _state = state;
        }

        public void Execute(ActiveCode activeCode)
        {
            var code = _state.Find(x => x.Id == activeCode.Id);

            code.FailedAttempts = activeCode.FailedAttempts;
        }
    }
}