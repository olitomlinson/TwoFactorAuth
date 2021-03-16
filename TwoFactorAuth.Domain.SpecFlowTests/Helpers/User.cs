using System.Collections.Generic;
using System.Linq;
using TwoFactorAuth.Domain.Tests.Steps;

namespace TwoFactorAuth.Domain.Tests.Helpers
{
    public class User
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public List<Service> Services { get; set; } = new List<Service>();

        public Service ActiveServiceContext { get; private set; }

        public void SetContext(string serviceName)
        {
            ActiveServiceContext = Services.Single(x => x.ServiceName == serviceName);
        }
    }
}