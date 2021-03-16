using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;

namespace TwoFactorAuthHost.SendCode
{
    class SendCodeOrchestration
    {
        [FunctionName(nameof(SendCodeOrchestration))]
        public async Task RunAsync(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var proxy = context.CreateEntityProxy<ISmsAuthenticationEntity>(new EntityId(nameof(SmsAuthenticationEntity), context.InstanceId));
            proxy.SendCode();
        }
    }
}
