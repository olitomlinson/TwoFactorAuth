using System;
using System.Collections.Generic;
using System.Linq;
using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain
{
    public class IsThereAnActiveCodePendingValidation
    {
        private Action<CodeClassificationResult> _activeCodeFound;
        private Action<CodeClassificationResult> _couldNotFindAnActiveCode;

        public void Execute(List<UnconsumedCode> unconsumedCodes)
        {
            var inactiveCodes = new List<InactiveCode>();
            var activeCodes = new List<ActiveCode>();

            unconsumedCodes.ForEach(code =>
            {
                new CheckIfCodeHasExpired()
                    .CodeHasNotExpired(activeCodes.Add)
                    .CodeHasExpired(inactiveCodes.Add)
                    .Execute(code);
            });

            switch (activeCodes.Count)
            {
                case 0:
                    _couldNotFindAnActiveCode(new CodeClassificationResult()
                    {
                        InactiveCodes = inactiveCodes
                    });
                    return;
                case 1:
                    _activeCodeFound(new CodeClassificationResult()
                    {
                        ActiveCode = activeCodes.Single(),
                        InactiveCodes = inactiveCodes
                    });
                    return;
                default:
                    if (activeCodes.Count > 1)
                    {
                        //This shouldn't happen but here is some code for it just in case
                        var resolvedCodes = _resolveMultipleActiveCodes(activeCodes);

                        _activeCodeFound(new CodeClassificationResult()
                        {
                            ActiveCode = resolvedCodes.ActiveCode,
                            InactiveCodes = inactiveCodes,
                            UnclassifiedCodes = resolvedCodes.UnclassifiableCodes
                        });
                    }
                    break;
            }
        }

        private Func<List<ActiveCode>, ActiveCodeResolutionResult> _resolveMultipleActiveCodes = activeCodes =>
        {
            var now = SystemTime.Now();
            var orderedActiveCodes = activeCodes.OrderBy(code => code.ExpiresOn - now);

            return new ActiveCodeResolutionResult()
            {
                ActiveCode = orderedActiveCodes.First(),
                UnclassifiableCodes = orderedActiveCodes.Skip(1).Select(x => x.ToInactiveCode().ToUnclassifiable()).ToList()
            };
        };

        public IsThereAnActiveCodePendingValidation ActiveCodeFound(Action<CodeClassificationResult> callback)
        {
            _activeCodeFound = callback;
            return this;
        }

        public IsThereAnActiveCodePendingValidation CouldNotFindAnActiveCode(Action<CodeClassificationResult> callback)
        {
            _couldNotFindAnActiveCode = callback;
            return this;
        }
    }

    public class CodeClassificationResult
    {
        public ActiveCode ActiveCode { get; set; }
        public List<InactiveCode> InactiveCodes { get; set; }
        public List<UnclassifiableCode> UnclassifiedCodes { get; set; } = new List<UnclassifiableCode>();
    }

    public static class InactiveCodeExtensions
    {
        public static UnclassifiableCode ToUnclassifiable(this InactiveCode activeCode)
        {
            return new UnclassifiableCode()
            {
                Id = activeCode.Id,
                AccountRef = activeCode.AccountRef,
                AuthCode = activeCode.AuthCode,
                ExpiresOn = activeCode.ExpiresOn,
                FailedAttemptLimit = activeCode.FailedAttemptLimit,
                FailedAttempts = activeCode.FailedAttempts,
                PhoneNumber = activeCode.PhoneNumber,
                RequestedOn = activeCode.RequestedOn
            };
        }
    }
}