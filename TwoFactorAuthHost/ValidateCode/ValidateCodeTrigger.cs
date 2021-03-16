using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TwoFactorAuthHost.SendCode
{
    public class ValidateCodeTrigger
    {
        [FunctionName(nameof(ValidateCodeTrigger))]
        public static async Task<HttpResponseMessage> RunAsync(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")] ValidateCodeRequest request,
           [DurableClient] IDurableOrchestrationClient starter)
        {
            string instanceId = $"{request.AccountRef}_{request.PhoneNumber}";
            await starter.StartNewAsync(nameof(ValidateCodeOrchestration), instanceId, request.Code);

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(starter.CreateHttpManagementPayload(instanceId).StatusQueryGetUri) };
        }
    }

    public class ValidateCodeRequest
    {
        public string AccountRef = "EX00001";
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
    }
}
