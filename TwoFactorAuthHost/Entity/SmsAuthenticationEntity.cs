using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;
using TwoFactorAuth.Domain;
using TwoFactorAuth.Domain.Interfaces.Models;
using TwoFactorAuthHost.Entity;
using TwoFactorAuthHost.Services;

namespace TwoFactorAuthHost
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SmsAuthenticationEntity : ISmsAuthenticationEntity
    {
        private TwoFactorAuth.Domain.TwoFactorAuth _domain;
        private string _accountId;
        private string _phoneNumber;

        [JsonProperty("Codes")]
        public List<Code> Codes = new List<Code>();


        [FunctionName(nameof(SmsAuthenticationEntity))]
        public static Task Run([EntityTrigger] IDurableEntityContext ctx)
        {
            return ctx.DispatchAsync<SmsAuthenticationEntity>(ctx);
        }

        public SmsAuthenticationEntity(IDurableEntityContext ctx)
        {
            var parts = ctx.EntityKey.Split("_");
            _accountId = parts[0];
            _phoneNumber = parts[1];

            var domainContext = new DomainContext(Codes);
            _domain = new TwoFactorAuth.Domain.TwoFactorAuth(
                insertActiveCodeCommand: domainContext,
                unconsumedCodeQuery: domainContext,
                sendCodeService: new SmsService(),
                codeGenerator: new CodeGenerator(),
                updateMatchedCodeAsConsumed: domainContext,
                updateActiveCodeCommand: domainContext,
                updateInactiveCodeAsConsumed: domainContext
             );  
        }

        public void SendCode()
        {
            _domain.SendCode(_accountId, _phoneNumber, new CodePolicy());
        }

        public Task<bool> ValidateCode(string code)
        {
            var isValid = _domain.ValidateCode(_accountId, _phoneNumber, code);
            return Task.FromResult(isValid);
        }
    }

    public interface ISmsAuthenticationEntity 
    {
        public void SendCode();

        public Task<bool> ValidateCode(string code);
    }
}