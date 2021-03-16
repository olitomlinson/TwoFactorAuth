using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TwoFactorAuthHost.SendCode
{
    public class SendCodeTrigger
    {
        [FunctionName(nameof(SendCodeTrigger))]
        public async Task<HttpResponseMessage> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] SendCodeRequest request,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            string instanceId = $"{request.AccountRef}_{request.PhoneNumber}";
            await starter.StartNewAsync(nameof(SendCodeOrchestration), instanceId);
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(starter.CreateHttpManagementPayload(instanceId).StatusQueryGetUri) };
        }
    }

    public class SendCodeRequest
    {
        public string AccountRef = "EX00001";
        public string PhoneNumber { get; set; }
    }
}
