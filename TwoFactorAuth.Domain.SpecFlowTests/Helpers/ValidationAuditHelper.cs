using System.Collections.Generic;
using System.Linq;
using TwoFactorAuth.Domain.Tests.Steps;

namespace TwoFactorAuth.Domain.Tests.Helpers
{
    public static class ValidationAuditHelper
    {
        private static List<Attempt> _state = new List<Attempt>();

        public static void Upsert(string attemptName, bool result, string username)
        {
            var attempt = _state.SingleOrDefault(x => x.AttemptName == attemptName);
            if (attempt != null)
            {
                attempt.Result = result;
                attempt.UserName = username;
            }
            else
            {
                _state.Add(new Attempt()
                {
                    AttemptName = attemptName,
                    UserName = username,
                    Result = result
                });
            }
        }

        public static bool GetResult(string attemptName)
        {
            return _state.SingleOrDefault(x => x.AttemptName == attemptName).Result;
        }

        public static void ResetAudit()
        {
            _state = new List<Attempt>();
        }
        public static void ResetAudit(string username)
        {
            _state.RemoveAll(x => x.UserName == username);
        }
    }
}