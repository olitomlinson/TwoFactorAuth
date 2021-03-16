using System;

namespace TwoFactorAuth.Domain.Interfaces.Models
{
    public class Code
    {
        public Guid Id { get; set; }
        public string AuthCode { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountRef { get; set; }
        public int FailedAttemptLimit { get; set; }
        public bool IsConsumed { get; set; }
        public bool MatchedSuccessfully { get; set; }
        public DateTime? MatchedSuccessfullyOn { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public int FailedAttempts { get; set; }
    }
}