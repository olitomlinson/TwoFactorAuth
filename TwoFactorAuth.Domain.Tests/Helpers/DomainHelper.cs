using System.Collections.Generic;
using TwoFactorAuth.Domain.Tests.mocks;
using TwoFactorAuth.Domain.Tests.Mocks;
using TwoFactorAuth.Domain.Tests.Steps;

namespace TwoFactorAuth.Domain.Tests.Helpers
{
    public static class DomainHelper
    {
        public static List<CodeMock> State = new List<CodeMock>();
        public static MockCodeGenerator MockCodeGenerator;
        public static MockSendCodeService MockSendCodeService;

        public static TwoFactorAuth Create()
        {
            var insertActiveCodeCommand = new InsertActiveCodeCommandInMemoryMock(State);
            MockCodeGenerator = new MockCodeGenerator(new CodeGenerator());
            MockSendCodeService = new MockSendCodeService();
            var unconsumedCodeQueryInMemory = new UnconsumedCodeQueryInMemoryMock(State);
            var updateMatchedCodeAsConsumed = new UpdateMatchedCodeAsConsumedInMemoryMock(State);
            var updateActiveCodeCommand = new UpdateActiveCodeCommandInMemoryMock(State);
            var updateInactiveCodeAsConsumed = new UpdateInactiveCodeAsConsumedInMemoryMock(State);

            return new TwoFactorAuth(insertActiveCodeCommand,
                unconsumedCodeQueryInMemory,
                MockSendCodeService,
                MockCodeGenerator,
                updateMatchedCodeAsConsumed,
                updateActiveCodeCommand,
                updateInactiveCodeAsConsumed);
        }
    }
}