using System;

namespace TwoFactorAuth.Domain.Core
{
    public class CodeGenerator : ICodeGenerator
    {
        public string Create(CodePolicy codePolicy)
        {
            string code = "";

            if (codePolicy.CodeType == CodeType.NumericOnly)
            {
                var r = new Random();

                for(int i = 0; i < codePolicy.CodeLength; i++)
                {
                    code += r.Next(0, 9).ToString();
                }
            }
            return code;
        }
    }
}