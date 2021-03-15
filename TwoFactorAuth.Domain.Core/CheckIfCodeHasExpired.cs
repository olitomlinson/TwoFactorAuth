using System;
using TwoFactorAuth.Domain.Core.Extensions;
using TwoFactorAuth.Domain.Core.Interfaces.Models;

namespace TwoFactorAuth.Domain.Core
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