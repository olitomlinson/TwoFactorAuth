using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;

namespace TwoFactorAuthHost.SendCode
{
    class ValidateCodeOrchestration
    {
        [FunctionName(nameof(ValidateCodeOrchestration))]
        public static async Task<string> RunAsync(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var proxy = context.CreateEntityProxy<ISmsAuthenticationEntity>(new EntityId(nameof(SmsAuthenticationEntity), context.InstanceId));
            var isValid = await proxy.ValidateCode(context.GetInput<string>());
            return isValid ? "supplied code IS valid!" : "supplied code IS NOT valid!";
        }
    }
}
