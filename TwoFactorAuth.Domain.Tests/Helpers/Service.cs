using System.Collections.Generic;
using System.Linq;
using TwoFactorAuth.Domain.Tests.Steps;

namespace TwoFactorAuth.Domain.Tests.Helpers
{
    public class Service
    {
        public string ServiceName { get; set; }

        public CodePolicy CodePolicy { get; set; }

        public List<string> AccountReferences { get; set; } = new List<string>();

        public List<User> Users { get; set; }  = new List<User>();
        public Customer ParentCustomer { get; set; }

        public void UpsertAccountReference(string accountReference)
        {
            var found = AccountReferences.Contains(accountReference);
            if (!found)
                AccountReferences.Add(accountReference);
        }

        public void UpsertPolicy(CodePolicy codePolicy)
        {
            CodePolicy = codePolicy;
        }

        public void LinkUser(User user)
        {
            var existingUser = Users.SingleOrDefault(x => x.Name == user.Name);

            if (existingUser != null)
                return;

            Users.Add(user);
            
            if (user.Services.All(x => x.ServiceName != this.ServiceName))
                user.Services.Add(this);
        }
    }
}