namespace TwoFactorAuth.Domain
{
    public interface ICodeGenerator
    {
        string Create(CodePolicy codePolicy);
    }
}