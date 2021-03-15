using System;

namespace TwoFactorAuth.Domain.Core
{
    public class SentCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountRef { get; set; }  
        public DateTime FirstSmsDispatchedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public int FailedAttemptLimit { get; set; }
        public int FailedAttempts { get; set; }
    }
}