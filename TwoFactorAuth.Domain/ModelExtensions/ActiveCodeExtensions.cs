using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain.ModelExtensions
{
    public static class ActiveCodeExtensions
    {
        public static MatchedCode ToMatchedCode(this ActiveCode activeCode)
        {
            return new MatchedCode()
            {
                Id = activeCode.Id,
                AccountRef = activeCode.AccountRef,
                AuthCode = activeCode.AuthCode,
                ExpiresOn = activeCode.ExpiresOn,
                FailedAttempts = activeCode.FailedAttempts,
                FailedAttemptLimit = activeCode.FailedAttemptLimit,
                PhoneNumber = activeCode.PhoneNumber,
                MatchedSuccessfully = true,
                MatchedSuccessfullyOn = SystemTime.Now(),
            };
        }

        public static UnmatchedCode ToUnmatchedCode(this ActiveCode activeCode)
        {
            return new UnmatchedCode()
            {
                Id = activeCode.Id,
                AccountRef = activeCode.AccountRef,
                AuthCode = activeCode.AuthCode,
                ExpiresOn = activeCode.ExpiresOn,
                FailedAttempts = ++activeCode.FailedAttempts,
                FailedAttemptLimit = activeCode.FailedAttemptLimit,
                PhoneNumber = activeCode.PhoneNumber,
            };
        }
    }
}