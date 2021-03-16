using System.Collections.Generic;
using System.Linq;
using TwoFactorAuth.Domain;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain.Tests.mocks
{
    public class MockSendCodeService : ISendCodeService
    {
        public SentMessage LastSentMessage => SentMessagesLog.Count > 1 ? SentMessagesLog.Last() : SentMessagesLog.FirstOrDefault();

        public List<SentMessage> SentMessagesLog { get; set; } = new List<SentMessage>();

        public void SendCode(ActiveCode activeCode)
        {
            SentMessagesLog.Add(new SentMessage()
            {
                AccountRef = activeCode.AccountRef,
                Message = activeCode.AuthCode,
                Phonenumber = activeCode.PhoneNumber
            });
        }
    }

    public class SentMessage
    {
        public string Phonenumber { get; set; }
        public string Message { get; set; }
        public string AccountRef { get; set; }
    }
}