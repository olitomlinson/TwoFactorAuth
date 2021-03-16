using System;
using TwoFactorAuth.Domain.Extensions;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain
{
    public class CheckIfCodeHasExpired
    {
        private Action<ActiveCode> _codeIsActive;
        private Action<InactiveCode> _codeIsInactive;

        public CheckIfCodeHasExpired CodeHasNotExpired(Action<ActiveCode> callback)
        {
            _codeIsActive = callback;
            return this;
        }

        public CheckIfCodeHasExpired CodeHasExpired(Action<InactiveCode> callback)
        {
            _codeIsInactive = callback;
            return this;
        }

        public void Execute(UnconsumedCode unconsumedCode)
        {
            if (unconsumedCode.ExpiresOn > SystemTime.Now() && unconsumedCode.FailedAttempts < unconsumedCode.FailedAttemptLimit)
                _codeIsActive(unconsumedCode.ToActiveCode());
            else
            {
                _codeIsInactive(unconsumedCode.ToInactiveCode());
            }
        }
    }
}