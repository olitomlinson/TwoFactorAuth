using System;
using System.Collections.Generic;
using TwoFactorAuth.Domain.Interfaces.Data.Command;
using TwoFactorAuth.Domain.Interfaces.Models;
using TwoFactorAuth.Domain.Tests.Mocks;

namespace TwoFactorAuth.Domain.Tests.mocks
{
    public class InsertActiveCodeCommandInMemoryMock : IInsertActiveCodeCommand
    {
        private readonly List<CodeMock> _state;

        public InsertActiveCodeCommandInMemoryMock(List<CodeMock> state )
        {
            this._state = state;
        }

        public void Execute(ActiveCode activeCode)
        {
            _state.Add(new CodeMock()
            {
                Id = Guid.NewGuid(),
                AuthCode = activeCode.AuthCode,
                PhoneNumber = activeCode.PhoneNumber,
                AccountRef = activeCode.AccountRef,
                RequestedOn = activeCode.RequestedOn,
                ExpiresOn = activeCode.ExpiresOn,
                FailedAttemptLimit = activeCode.FailedAttemptLimit,
                FailedAttempts = activeCode.FailedAttempts
            });
        }
    }
}