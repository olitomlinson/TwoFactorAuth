using System.Collections.Generic;
using TwoFactorAuth.Domain.Interfaces.Data.Command;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain.Tests.Mocks
{
    public class UpdateMatchedCodeAsConsumedInMemoryMock : IUpdateMatchedCodeAsConsumed
    {
        private readonly List<CodeMock> _state;

        public UpdateMatchedCodeAsConsumedInMemoryMock(List<CodeMock> state)
        {
            _state = state;
        }

        public void Execute(MatchedCode matchedCode)
        {
            var code = _state.Find(x => x.Id == matchedCode.Id);

            code.MatchedSuccessfully = matchedCode.MatchedSuccessfully;
            code.MatchedSuccessfullyOn = matchedCode.MatchedSuccessfullyOn;
            code.IsConsumed = true;
        }
    }
}