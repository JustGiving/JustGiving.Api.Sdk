using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Page;
using JustGiving.Api.Sdk.Test.Common.Configuration;
using JustGiving.Api.Sdk.Test.Integration.Configuration;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class SmsApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePageSmsCode_WhenProvidedKnownFundraisingPage_ReturnSmsCode(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientValidCredentials(format);
            var smsResources = new SmsApi(client.HttpChannel);
            var fundraisingResources = new PageApi(client.HttpChannel);
            var validRegisterRequest = ValidRegisterPageRequest();
            fundraisingResources.Create(validRegisterRequest);

            //act
            var result = smsResources.RetrievePageSmsCode(validRegisterRequest.PageShortName);

            //assert
            Assert.IsNotNullOrEmpty(result.Urn);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void CheckSmsCodeAvailability_WhenProvidedCode_ReturnResponse(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientValidCredentials(format);
            var smsResources = new SmsApi(client.HttpChannel);
            string validSmsCode = GenerateRandomSmsCode();

            //act
            var result = smsResources.CheckSmsCodeAvailability(validSmsCode);

            //assert
            Assert.IsTrue(result.IsAvailable);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void UpdatePageSmsCode_WhenProvidedValidRequestAndValidCredentials_ReturnTrue(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientValidCredentials(format);
            var smsResources = new SmsApi(client.HttpChannel);
            var fundraisingResources = new PageApi(client.HttpChannel);
            var validRegisterRequest = ValidRegisterPageRequest();
            fundraisingResources.Create(validRegisterRequest);
            var randomSmsCodeToUpdate = GenerateRandomSmsCode();
            var validRequest = new SmsApi.SmsUpdate {Urn = randomSmsCodeToUpdate};

            //act
            var result = smsResources.UpdatePageSmsCode(validRegisterRequest.PageShortName, validRequest);

            //assert
            Assert.IsTrue(result);
        }

        private static RegisterPageRequest ValidRegisterPageRequest()
        {
            return new RegisterPageRequest
            {
                ActivityType = null,
                PageShortName = "test-frp-" + Guid.NewGuid(),
                PageTitle =
                    "When Provided With Valid Authentication Details And An Empty Activity Type - Creates New Page",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                EventId = TestConfigurationsHelper.GetProperty<ITestConfigurations, int>(x => x.ValidEventId),
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };
        }

        private string GenerateRandomSmsCode()
        {
            string validSmsCode = string.Empty;
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                validSmsCode += (char)rnd.Next(97, 122);
            }
            validSmsCode += rnd.Next(47, 99);
            return validSmsCode;
        }
    }
}
