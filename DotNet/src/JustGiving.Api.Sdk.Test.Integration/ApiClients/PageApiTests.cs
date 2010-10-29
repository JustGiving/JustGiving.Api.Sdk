using System;
using System.IO;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model;
using JustGiving.Api.Sdk.Model.Page;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class PageApiTests : ApiClientTestsBase
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenProvidedWithValidAuthenticationAndDetails_CreatesANewPage(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);

            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.OtherCelebration,
                PageShortName = "api-test-" + Guid.NewGuid(),
                PageTitle = "api test",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            var registrationResponse = pageClient.Create(pageCreationRequest);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenProvidedWithValidAuthenticationAndDetailsAndAnEmptyActivityType_CreatesANewPage(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);

            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = null,
                PageShortName = "api-test-" + Guid.NewGuid(),
                PageTitle = "api test",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                EventId = 1,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            var registrationResponse = pageClient.Create(pageCreationRequest);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void ListPages_WhenProvidedCredentials_ReturnsPages(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);

            var pageData = pageClient.ListAll();
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePage_WhenProvidedWithAKnownPage_ReturnsPublicPageView(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var pageClient = new PageApi(client);

            var pageData = pageClient.Retrieve("rasha25");
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveDonationsForPage_WhenProvidedWithAKnownPageAndRequesterIsThePageOwner_ReturnsDonations(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);

            var pageData = pageClient.RetrieveDonationsForPage("rasha25");
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveDonationsForPage_WhenProvidedWithAKnownPageAndRequesterIsAnon_ReturnsDonations(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var pageClient = new PageApi(client);

            var pageData = pageClient.RetrieveDonationsForPage("rasha25");
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsPageShortNameRegistered_WhenSuppliedKnownExitingPage_ReturnsTrue(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var pageClient = new PageApi(client);

            var exists = pageClient.IsPageShortNameRegistered("rasha25");

            Assert.IsTrue(exists);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void UpdatePageStory_WhenProvidedCredentialsAndValidPage_PostsUpdate(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);

            // Create Page
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.OtherCelebration,
                PageShortName = "api-test-" + Guid.NewGuid(),
                PageTitle = "Page Created For Update Story Integration Test",
                EventName = "Story Update Testing",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            var registrationResponse = pageClient.Create(pageCreationRequest);


            pageClient.UpdateStory(pageCreationRequest.PageShortName, DateTime.Now + ": Unit Test Update");
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void UploadImage_WhenProvidedCredentialsAndValidPageAndImage_UploadsImage(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);
            
            // Create Page
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.OtherCelebration,
                PageShortName = "api-test-" + Guid.NewGuid(),
                PageTitle = "Page Created For Update Story Integration Test",
                EventName = "Story Update Testing",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            var registrationResponse = pageClient.Create(pageCreationRequest);

            pageClient.UploadImage(pageCreationRequest.PageShortName, "my image", File.ReadAllBytes("jpg.jpg"), "image/jpeg");
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void UploadImage_WhenProvidedInvaildCredentialsAndValidPageAndImage_ThrowsException(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var pageClient = new PageApi(client);

            var exception = Assert.Throws<ErrorResponseException>(() => pageClient.UploadImage("rasha25", "my image", File.ReadAllBytes("jpg.jpg"), "image/jpeg"));


        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsPageShortNameRegistered_WhenSuppliedPageNameUnlikelyToExist_ReturnsFalse(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var pageClient = new PageApi(client);

            var exists = pageClient.IsPageShortNameRegistered(Guid.NewGuid().ToString());

            Assert.IsFalse(exists);
        }
    }
}
