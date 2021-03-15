namespace TwoFactorAuth.Domain.Core
{
    public interface ICodeGenerator
    {
        string Create(CodePolicy codePolicy);
    }
}