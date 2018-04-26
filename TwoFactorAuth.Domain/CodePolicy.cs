using System;
using System.Xml.Schema;

namespace TwoFactorAuth.Domain
{
    public class CodePolicy
    {
        private const int MinimumExpiryTimeInMinutes = 1;
        private const int DefaultExpiryTimeInMinutes = 10;
        private const int MaximumExpiryTimeInMinutes = 60;

        private int _expiresInMinutes = DefaultExpiryTimeInMinutes;
        public int ExpiresInMinutes
        {
            get { return _expiresInMinutes; }
            set
            {
                if (value < MinimumExpiryTimeInMinutes || value > MaximumExpiryTimeInMinutes)
                    throw new Exception("Code TTL is outside the allowed range");

                _expiresInMinutes = value;
            }
        }

        private const int MinimumCodeLength = 4;
        private const int DefaultCodeLength = 6;
        private const int MaximumCodeLength = 10;

        private int _codeLength = DefaultCodeLength;

        public int CodeLength
        {
            get { return _codeLength; }
            set
            {
                if (value < MinimumCodeLength || value > MaximumCodeLength)
                    throw new Exception("Code length is outside of allowed range");

                _codeLength = value;
            }
        }

        private const int MinimumFailedAttemptLimit = 1;
        private const int DefaultFailedAttemptLimit = 3;
        private const int MaximumFailedAttemptLimit = 10;

        private int _failedAttemptLimit = DefaultFailedAttemptLimit;

        public int FailedAttemptLimit
        {
            get { return _failedAttemptLimit; }
            set
            {
                if (value < MinimumFailedAttemptLimit || value > MaximumFailedAttemptLimit)
                    throw new Exception("Failed attempt limit is outside of allowed range");

                _failedAttemptLimit = value;
            }
        }
        public CodeType CodeType { get; set; }
    }

    public enum CodeType { NumericOnly, AlphaOnly, AlphaNumeric }
}