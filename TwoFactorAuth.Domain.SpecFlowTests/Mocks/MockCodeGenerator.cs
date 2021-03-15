using TwoFactorAuth.Domain.Core;

namespace TwoFactorAuth.Domain.Tests.Steps
{
    public class MockCodeGenerator : ICodeGenerator
    {
        private readonly ICodeGenerator _codeGenerator;
        public string Override { get; set; } = string.Empty;

        public MockCodeGenerator(ICodeGenerator codeGenerator)
        {
            _codeGenerator = codeGenerator;
        }

        public string Create(CodePolicy codePolicy)
        {
            return Override != string.Empty ? Override : _codeGenerator.Create(codePolicy);
        }
    }
}