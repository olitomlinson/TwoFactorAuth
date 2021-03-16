using System.Collections.Generic;
using TwoFactorAuth.Domain.Interfaces.Data.Command;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain.Tests.Mocks
{
    public class UpdateInactiveCodeAsConsumedInMemoryMock : IUpdateInactiveCodeAsConsumed
    {
        private readonly List<CodeMock> _state;

        public UpdateInactiveCodeAsConsumedInMemoryMock(List<CodeMock> state)
        {
            _state = state;
        }

        public void Execute(InactiveCode inactiveCode)
        {
            var code = _state.Find(x => x.Id == inactiveCode.Id);

            code.AccountRef = inactiveCode.AccountRef;
            code.AuthCode = inactiveCode.AuthCode;
            code.FailedAttemptLimit = inactiveCode.FailedAttemptLimit;
            code.FailedAttempts = inactiveCode.FailedAttempts;
        }
    }
}