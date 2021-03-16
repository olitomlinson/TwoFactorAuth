using System.Collections.Generic;
using System.Linq;
using TwoFactorAuth.Domain.Interfaces.Data.Command;
using TwoFactorAuth.Domain.Interfaces.Data.Query;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuthHost.Entity
{
    public class DomainContext : IInsertActiveCodeCommand, IUnconsumedCodeQuery, IUpdateMatchedCodeAsConsumed, IUpdateActiveCodeCommand, IUpdateInactiveCodeAsConsumed
    {
        private List<Code> _codes;

        public DomainContext(List<Code> codes)
        {
            this._codes = codes;
        }

        void IInsertActiveCodeCommand.Execute(ActiveCode activeCode)
        {
            if (_codes == null)
                _codes = new List<Code>();
            _codes.Add(activeCode);
        }

        List<UnconsumedCode> IUnconsumedCodeQuery.Execute(string accountref, string phonenumber)
        {
            if (_codes == null)
                return new List<UnconsumedCode>();

            var UnconsumedCodes = new List<UnconsumedCode>();

            foreach (var code in _codes.Where(x => x.IsConsumed == false).ToList())
            {
                UnconsumedCodes.Add(new UnconsumedCode()
                {
                    AccountRef = code.AccountRef,
                    AuthCode = code.AuthCode,
                    ExpiresOn = code.ExpiresOn,
                    FailedAttemptLimit = code.FailedAttemptLimit,
                    FailedAttempts = code.FailedAttempts,
                    Id = code.Id,
                    IsConsumed = code.IsConsumed,
                    MatchedSuccessfully = code.MatchedSuccessfully,
                    MatchedSuccessfullyOn = code.MatchedSuccessfullyOn,
                    PhoneNumber = code.PhoneNumber,
                    RequestedOn = code.RequestedOn
                });
            }
            return UnconsumedCodes;
        }

        void IUpdateMatchedCodeAsConsumed.Execute(MatchedCode matchedCode)
        {
            _codes.Remove(_codes.Find(x => x.Id == matchedCode.Id));
            _codes.Add(matchedCode);
        }

        void IUpdateActiveCodeCommand.Execute(ActiveCode activeCode)
        {
            _codes.Remove(_codes.Find(x => x.Id == activeCode.Id));
            _codes.Add(activeCode);
        }

        void IUpdateInactiveCodeAsConsumed.Execute(InactiveCode inactiveCode)
        {
            _codes.Remove(_codes.Find(x => x.Id == inactiveCode.Id));
            _codes.Add(inactiveCode);
        }
    }
}
