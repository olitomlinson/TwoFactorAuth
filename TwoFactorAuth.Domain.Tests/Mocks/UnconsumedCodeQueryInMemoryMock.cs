using System.Collections.Generic;
using System.Linq;
using TwoFactorAuth.Domain.Data.Query;
using TwoFactorAuth.Domain.Interfaces.Data.Query;
using TwoFactorAuth.Domain.Interfaces.Models;
using TwoFactorAuth.Domain.Tests.Mocks;

namespace TwoFactorAuth.Domain.Tests.mocks
{
    public class UnconsumedCodeQueryInMemoryMock : IUnconsumedCodeQuery
    {
        private readonly List<CodeMock> _state;

        public UnconsumedCodeQueryInMemoryMock(List<CodeMock> state)
        {
            this._state = state;
        }

        public List<UnconsumedCode> Execute(string accountref, string phonenumber)
        {
            return _state.Where(x => x.AccountRef == accountref
                                     && x.PhoneNumber == phonenumber)
                .Select(y => new UnconsumedCode()
                {
                    Id = y.Id,
                    AccountRef = y.AccountRef,
                    AuthCode = y.AuthCode,
                    ExpiresOn = y.ExpiresOn,
                    FailedAttempts = y.FailedAttempts,
                    FailedAttemptLimit = y.FailedAttemptLimit,
                    PhoneNumber = y.PhoneNumber,
                    RequestedOn = y.RequestedOn
                }).ToList();
        }
    }
}