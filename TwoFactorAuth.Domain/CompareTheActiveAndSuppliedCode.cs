using System;
using TwoFactorAuth.Domain.ModelExtensions;
using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain
{
    public class CompareTheActiveAndSuppliedCode
    {
        private Action<MatchedCode> _onCodeMatchCallback;
        private Action<UnmatchedCode> _onCodeNoMatchCallback;

        public CompareTheActiveAndSuppliedCode WhenCodeMatches(Action<MatchedCode> onCodeMatchCallback)
        {
            _onCodeMatchCallback = onCodeMatchCallback;
            return this;
        }

        public CompareTheActiveAndSuppliedCode WhenCodeDoesntMatch(Action<UnmatchedCode> onCodeNoMatchCallback)
        {
            _onCodeNoMatchCallback = onCodeNoMatchCallback;
            return this;
        }

        public void Execute(ActiveCode activeCode, string suppliedCode)
        {
            if (string.Equals(activeCode.AuthCode, suppliedCode, StringComparison.CurrentCultureIgnoreCase))
                _onCodeMatchCallback(activeCode.ToMatchedCode());
            else
                _onCodeNoMatchCallback(activeCode.ToUnmatchedCode());
        }
    }
}