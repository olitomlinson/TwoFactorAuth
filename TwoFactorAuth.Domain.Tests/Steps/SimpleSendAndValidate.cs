using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TwoFactorAuth.Domain.Tests.Helpers;
using Assert = NUnit.Framework.Assert;

namespace TwoFactorAuth.Domain.Tests.Steps
{
    public class Customer
    {
        public string CustomerName { get; set; }

        public List<string> AccountReferences { get; set; }

        public List<Service> Services { get; set; } = new List<Service>();


        public Service GetService(string serviceName)
        {
            return this.Services.SingleOrDefault(x => x.ServiceName == serviceName);
        }
    }


    [Binding]
    public class SimpleSendAndValidate
    {

        [Given(@"The customer (.*) uses the two-factor-auth solution to secure their (.*) service")]
        public void GivenTheCustomerSpecsaversUsesTheTwo_Factor_AuthSolutionToSecureTheirCRMService(string customerName, string serviceName)
        {
            UserHelper.DeleteServiceForCustomer( customerName, serviceName);
            UserHelper.UpsertCustomerAndService(customerName, serviceName);
        }


        [Given(@"(.*) (.*) service uses the Esendex account (.*) for sending Sms")]
        public void GivenServiceUsesTheAccountForSendingSms(string customerName, string serviceName, string accountReference)
        {
            var customer = UserHelper.GetCustomer(customerName);
            var service = customer.GetService(serviceName);
            service.UpsertAccountReference(accountReference);
        }

        [Given(@"(.*) (.*) service requires the (.*) code policy")]
        public void GivenServiceRequiresTheTwo_FactorAuthCodePolicy(string customerName, string serviceName,string policy)
        {
            var customer = UserHelper.GetCustomer(customerName);
            var service = customer.GetService(serviceName);

            if (policy == "default")
            {
                service.UpsertPolicy(new CodePolicy());
            }
        }

        [Given(@"(.*) is required to complete two-factor auth to use the (.*) (.*) service")]
        public void GivenTheUserIsRequiredToCompleteTwo_FactorAuthToUseTheService(string userName, string customerName, string serviceName)
        {
            var customer = UserHelper.GetCustomer(customerName);
            var service = customer.GetService(serviceName);

            var userx = UserHelper.Users.Single(x => x.Name == userName);
            service.LinkUser(userx);
        }

        [Given(@"(.*) has a telephone number of (.*)")]
        public void GivenHasATelephoneNumberOf(string userName, string phonenumber)
        {
            UserHelper.UpsertUser(userName, phonenumber);           
        }

        [Given(@"(.*) attempted to log in to the (.*) (.*) Service")]
        public void GivenAttemptedToLogInToTheService(string userName, string customerName, string serviceName)
        {
            var user = UserHelper.GetUser(userName);
            user.SetContext(serviceName);
        }


        [When(@"(.*) (.*) service intiates the two factor authorization process for (.*)")]
        public void WhenServiceIntiatesTheTwoFactorAuthorizationProcessFor(string customerName, string serviceName, string userName)
        {
            var domain = DomainHelper.Create();

            var user = UserHelper.GetUser(userName);

            domain.SendCode(user.ActiveServiceContext.AccountReferences.First(), user.PhoneNumber, user.ActiveServiceContext.CodePolicy);
        }

        [Then(@"(.*) receives an Sms")]
        public void ThenReceivesAnSms(string userName)
        {
            var user = UserHelper.GetUser(userName);
            var sentSms = DomainHelper.MockSendCodeService.LastSentMessage;

            Assert.AreEqual(user.PhoneNumber, sentSms.Phonenumber);
        }

        [Then(@"the code that (.*) received is (.*) characters long")]
        public void ThenTheCodeThatReceivedIsCharactersLong(string userName, int length)
        {
            var user = UserHelper.GetUser(userName);
            var sentSms = DomainHelper.MockSendCodeService.LastSentMessage;

            Assert.AreEqual(user.PhoneNumber, sentSms.Phonenumber);
            Assert.AreEqual(length, sentSms.Message.Length);
        }

        [Then(@"the code that (.*) received only contains numbers")]
        public void ThenTheCodeThatReceivedOnlyContainsNumbers(string userName)
        {
            var user = UserHelper.GetUser(userName);
            var sentSms = DomainHelper.MockSendCodeService.LastSentMessage;

            var isMatch = System.Text.RegularExpressions.Regex.IsMatch(sentSms.Message, "^[0-9]*$");

            Assert.AreEqual(user.PhoneNumber, sentSms.Phonenumber);
            Assert.True(isMatch);
        }

        [Given(@"(.*) has never been sent a two-factor-auth Sms before")]
        public void GivenThatAUserHasNeverBeenValidatedBefore(string name)
        {
            var user = UserHelper.GetUser(name);
            ValidationAuditHelper.ResetAudit(name);
            DomainHelper.State.RemoveAll(x => x.PhoneNumber == user.PhoneNumber);
        }

        [Given(@"the current time is (.*)")]
        public void GivenTheCurrentTimeIs(string dateTime)
        {
            SystemTime.SetDateTime(DateTime.Parse(dateTime, null, System.Globalization.DateTimeStyles.RoundtripKind));
        }


        [Given(@"(.*) was sent the code (.*) in an Sms at (.*) (.*)")]
        public void GivenHasBeenSentTheCode(string name, string code, string time, string date)
        {
            SystemTime.SetDateTime(DateTimeHelper.Fathom(time,date));
            var domain = DomainHelper.Create();
            DomainHelper.MockCodeGenerator.Override = code;
            var user = UserHelper.GetUser(name);

            domain.SendCode(user.ActiveServiceContext.AccountReferences.First(), user.PhoneNumber, user.ActiveServiceContext.CodePolicy);
        }

        [When(@"(.*) requests a validation code (.*) minutes later")]
        public void WhenAUserHasBeenSentTheCodeSomeMinutesLater(string name, int minutesLater)
        {
            var now = SystemTime.Now();
            SystemTime.SetDateTime(now.AddMinutes(minutesLater));

            var domain = DomainHelper.Create();
            var user = UserHelper.GetUser(name);

            domain.SendCode(user.ActiveServiceContext.AccountReferences.First(), user.PhoneNumber, user.ActiveServiceContext.CodePolicy);
        }

        [When(@"the current time is (.*)")]
        public void WhenTheCurrentTimeIs(string dateTime)
        {
            SystemTime.SetDateTime(DateTime.Parse(dateTime, null, System.Globalization.DateTimeStyles.RoundtripKind));
        }


        [When(@"(.*) supplies the (.*) code (.*) to access the (.*) service (.*) minutes later")]
        public void WhenGavin_On_EXSuppliesTheFirstCodeToAccessTheMinutesLater(string name, string attemptName, string suppliedCode, string serviceName, int minutesLater)
        {
            var now = SystemTime.Now();
            SystemTime.SetDateTime(now.AddMinutes(minutesLater));

            var domain = DomainHelper.Create();
            var user = UserHelper.GetUser(name);
            user.SetContext(serviceName);

            var result = domain.ValidateCode(user.ActiveServiceContext.AccountReferences.First(), user.PhoneNumber, suppliedCode);

            ValidationAuditHelper.Upsert(attemptName, result, name);
        }




        [When(@"(.*) supplies the (.*) code (.*) for validation (.*) minutes later")]
        public void WhenMalcomSuppliesTheCodeForValidation(string name, string attemptName, string suppliedCode, int minutesLater)
        {
            var now = SystemTime.Now();
            SystemTime.SetDateTime(now.AddMinutes(minutesLater));

            var domain = DomainHelper.Create();
            var user = UserHelper.GetUser(name);

            var result = domain.ValidateCode(user.ActiveServiceContext.AccountReferences.First(), user.PhoneNumber, suppliedCode);

            ValidationAuditHelper.Upsert(attemptName, result, name);
        }

        [Then(@"the (.*) code validates successfully")]
        public void ThenTheCodeValidatesSuccessfully(string attemptName)
        {
           var result = ValidationAuditHelper.GetResult(attemptName);
            Assert.True(result);
        }

        [Then(@"the (.*) code fails validation")]
        public void ThenTheCodeFailsValidation(string attemptName)
        {
            var result = ValidationAuditHelper.GetResult(attemptName);
            Assert.False(result);
        }

        [Then(@"(.*) receives the validation code (.*)")]
        public void ThenTheValidationCodeIs(string name, int code)
        {
            var user = UserHelper.GetUser(name);
            var sentMessage = DomainHelper.MockSendCodeService.LastSentMessage;

            Assert.AreEqual(user.ActiveServiceContext.AccountReferences.First(), sentMessage.AccountRef);
            Assert.AreEqual(user.PhoneNumber, sentMessage.Phonenumber );
            Assert.AreEqual(code.ToString(), sentMessage.Message);
        }


        [Then(@"(.*) receives a validation code that is different to (.*)")]
        public void ThenAValidationCodeThatIsDifferentTo(string name, int code)
        {
            var user = UserHelper.GetUser(name);
            var sentMessage = DomainHelper.MockSendCodeService.LastSentMessage;

            Assert.AreEqual(user.ActiveServiceContext.AccountReferences.First(), sentMessage.AccountRef);
            Assert.AreEqual(user.PhoneNumber, sentMessage.Phonenumber);
            Assert.AreNotEqual(code.ToString(), sentMessage.Message);
        }
    }
}
