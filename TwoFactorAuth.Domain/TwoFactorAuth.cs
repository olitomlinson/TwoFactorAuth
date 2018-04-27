using System;
using TwoFactorAuth.Domain.Extensions;
using TwoFactorAuth.Domain.Interfaces.Data.Command;
using TwoFactorAuth.Domain.Interfaces.Data.Query;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain
{
    public class TwoFactorAuth
    {
        private readonly IInsertActiveCodeCommand _insertActiveCodeCommand;
        private readonly IUnconsumedCodeQuery _unconsumedCodeQuery;
        private readonly ISendCodeService _sendCodeService;
        private readonly ICodeGenerator _codeGenerator;
        private readonly IUpdateMatchedCodeAsConsumed _updateMatchedCodeAsConsumed;
        private readonly IUpdateActiveCodeCommand _updateActiveCodeCommand;
        private readonly IUpdateInactiveCodeAsConsumed _updateInactiveCodeAsConsumed;
        private readonly Action<UnconsumedCode, Action<ActiveCode>, Action<InactiveCode>> _checkCodeHasExpired;

        public TwoFactorAuth(IInsertActiveCodeCommand insertActiveCodeCommand, 
            IUnconsumedCodeQuery unconsumedCodeQuery, 
            ISendCodeService sendCodeService, 
            ICodeGenerator codeGenerator, 
            IUpdateMatchedCodeAsConsumed updateMatchedCodeAsConsumed, 
            IUpdateActiveCodeCommand updateActiveCodeCommand, 
            IUpdateInactiveCodeAsConsumed updateInactiveCodeAsConsumed)
        {
            _insertActiveCodeCommand = insertActiveCodeCommand;
            _unconsumedCodeQuery = unconsumedCodeQuery;
            _sendCodeService = sendCodeService;
            _codeGenerator = codeGenerator;
            _updateMatchedCodeAsConsumed = updateMatchedCodeAsConsumed;
            _updateActiveCodeCommand = updateActiveCodeCommand;
            _updateInactiveCodeAsConsumed = updateInactiveCodeAsConsumed;
        }

        public void SendCode(string accountref, string phonenumber, CodePolicy codePolicy)
        {
            _unconsumedCodeQuery
                .Execute(accountref, phonenumber)
                .Tee(unconsumedCodes =>
                {
                    new IsThereAnActiveCodePendingValidation()
                    .ActiveCodeFound(result =>
                    {
                        _sendCodeService.SendCode(result.ActiveCode);
                    })
                    .CouldNotFindAnActiveCode(result =>
                    {
                        var activeCode = CreateActiveCode(accountref, phonenumber, codePolicy);

                        _insertActiveCodeCommand.Execute(activeCode);

                        _sendCodeService.SendCode(activeCode);

                        RemoveAnyExpiredAndUnclassifiedCodes(result);
                    })
                    .Execute(unconsumedCodes);
                });
        }

        public bool ValidateCode(string accountref, string phonenumber, string suppliedCode)
        {
            var validateResult = false;

            _unconsumedCodeQuery
                .Execute(accountref, phonenumber)
                .Tee(unconsumedCodes =>
                {
                    new IsThereAnActiveCodePendingValidation()
                        .ActiveCodeFound(result =>
                        {
                            new CompareTheActiveAndSuppliedCode()
                                .WhenCodeMatches(matchedCode =>
                                {
                                    validateResult = true;
                                    _updateMatchedCodeAsConsumed.Execute(matchedCode);
                                })
                                .WhenCodeDoesntMatch(unmatchedCode =>
                                {
                                    new CheckIfCodeHasExpired()
                                        .CodeHasNotExpired(_updateActiveCodeCommand.Execute)
                                        .CodeHasExpired(_updateInactiveCodeAsConsumed.Execute)
                                        .Execute(unmatchedCode);
                                })
                                .Execute(result.ActiveCode, suppliedCode);
                            RemoveAnyExpiredAndUnclassifiedCodes(result);
                        })
                        .CouldNotFindAnActiveCode(RemoveAnyExpiredAndUnclassifiedCodes)
                        .Execute(unconsumedCodes);
                });

            return validateResult;
        }


        private ActiveCode CreateActiveCode(string accountref, string phonenumber, CodePolicy codePolicy)
        {
            var activeCode = new ActiveCode()
            {
                Id = Guid.NewGuid(),
                AccountRef = accountref,
                PhoneNumber = phonenumber,
                AuthCode = _codeGenerator.Create(codePolicy),
                ExpiresOn = SystemTime.Now().AddMinutes(codePolicy.ExpiresInMinutes),
                FailedAttempts = 0,
                FailedAttemptLimit = codePolicy.FailedAttemptLimit,
                RequestedOn = SystemTime.Now(),
                IsConsumed = false
            };
            return activeCode;
        }

        private void RemoveAnyExpiredAndUnclassifiedCodes(CodeClassificationResult result)
        {
            result.InactiveCodes.ForEach(_updateInactiveCodeAsConsumed.Execute);
            result.UnclassifiedCodes.ForEach(_updateInactiveCodeAsConsumed.Execute);
        }
    }
}
