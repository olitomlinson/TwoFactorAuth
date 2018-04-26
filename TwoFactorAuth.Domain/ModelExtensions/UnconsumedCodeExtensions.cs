using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain
{
    public static class UnconsumedCodeExtensions
    {
        public static InactiveCode ToInactiveCode(this UnconsumedCode unconsumedCode)
        {
            return new InactiveCode()
            {
                Id = unconsumedCode.Id,
                AccountRef = unconsumedCode.AccountRef,
                AuthCode = unconsumedCode.AuthCode,
                ExpiresOn = unconsumedCode.ExpiresOn,
                FailedAttemptLimit = unconsumedCode.FailedAttemptLimit,
                FailedAttempts = unconsumedCode.FailedAttempts,
                PhoneNumber = unconsumedCode.PhoneNumber,
                RequestedOn = unconsumedCode.RequestedOn
            };
        }

        public static ActiveCode ToActiveCode(this UnconsumedCode unconsumedCode)
        {
            return new ActiveCode()
            {
                Id = unconsumedCode.Id,
                AccountRef = unconsumedCode.AccountRef,
                AuthCode = unconsumedCode.AuthCode,
                ExpiresOn = unconsumedCode.ExpiresOn,
                FailedAttemptLimit = unconsumedCode.FailedAttemptLimit,
                FailedAttempts = unconsumedCode.FailedAttempts,
                PhoneNumber = unconsumedCode.PhoneNumber,
                RequestedOn = unconsumedCode.RequestedOn
            };
        }
    }
}