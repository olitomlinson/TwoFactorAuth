﻿using System.Collections.Generic;
using TwoFactorAuth.Domain.Data.Command;
using TwoFactorAuth.Domain.Models;

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
            code.IsConsumed = true;
        }
    }

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