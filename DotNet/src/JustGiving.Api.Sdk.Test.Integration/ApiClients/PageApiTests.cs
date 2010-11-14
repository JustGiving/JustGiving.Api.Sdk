using System;
using System.IO;
using System.Net;
using System.Threading;
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
            var pageShortName = "api-test-" + Guid.NewGuid();
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.OtherCelebration,
                PageShortName = pageShortName,
                PageTitle = "api test",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            var registrationResponse = pageClient.Create(pageCreationRequest);

            Assert.That(registrationResponse.Next.Uri, Is.StringContaining(pageShortName));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenProvidedWithValidAuthenticationAndDetailsAndAnEmptyActivityType_CreatesANewPage(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);
            var pageShortName = "api-test-" + Guid.NewGuid();
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = null,
                PageShortName = pageShortName,
                PageTitle = "api test",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                EventId = 1,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            var registrationResponse = pageClient.Create(pageCreationRequest);

            Assert.That(registrationResponse.Next.Uri, Is.StringContaining(pageShortName));
        }

        /// <summary>
        /// This test assumes that the Valid Credentials in the test context has more than 1 page
        /// Which it will do if you run these integration tests at least once.
        /// Might fail first time around if you change the account.
        /// </summary>
        /// <param name="format"></param>
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void ListPages_WhenProvidedCredentials_ReturnsPages(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);

            var pageData = pageClient.ListAll();

            Assert.That(pageData.Count, Is.GreaterThan(0));
        }

        /// <summary>
        /// Assumes that Rasha25 exists in the given environment.
        /// </summary>
        /// <param name="format"></param>
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePage_WhenProvidedWithAKnownPage_ReturnsPublicPageView(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);

            // Create Page
            var pageShortName = "api-test-" + Guid.NewGuid();
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.OtherCelebration,
                PageShortName = pageShortName,
                PageTitle = "Page Created For Update Story Integration Test",
                EventName = "Story Update Testing",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };
            pageClient.Create(pageCreationRequest);

            // Act
            var pageData = pageClient.Retrieve(pageShortName);

            Assert.NotNull(pageData);
            Assert.That(pageData.PageCreatorName, Is.StringContaining("Test Test"));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePage_WhenProvidedWithABadPage_ThrowsResourceNotFoundException(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var pageClient = new PageApi(client);

            var exception = Assert.Throws<ResourceNotFoundException>(() => pageClient.Retrieve(Guid.NewGuid().ToString()));

            Assert.IsInstanceOf<ResourceNotFoundException>(exception);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveDonationsForPage_WhenProvidedWithAKnownPageAndRequesterIsAnon_ReturnsDonations(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var pageClient = new PageApi(client);

            var pageData = pageClient.RetrieveDonationsForPage("rasha25");

            Assert.That(pageData.Donations.Count, Is.GreaterThan(0));
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
            var pageShortName = "api-test-" + Guid.NewGuid();
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.OtherCelebration,
                PageShortName = pageShortName,
                PageTitle = "Page Created For Update Story Integration Test",
                EventName = "Story Update Testing",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            var registrationResponse = pageClient.Create(pageCreationRequest);

            // Act
            var update = DateTime.Now + ": Unit Test Update";
            pageClient.UpdateStory(pageCreationRequest.PageShortName, update);

            // Assert
            var pageData = pageClient.Retrieve(pageShortName);
            Assert.That(pageData.Story, Is.StringContaining(update));
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
            pageClient.Create(pageCreationRequest);

            var imageName = Guid.NewGuid().ToString();
            pageClient.UploadImage(pageCreationRequest.PageShortName, imageName, File.ReadAllBytes("jpg.jpg"), "image/jpeg");
            
            // Assert
            var pageData = pageClient.Retrieve(pageCreationRequest.PageShortName);
            Assert.That(pageData.Media.Images[0].Caption, Is.StringContaining(imageName));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void UploadImage_WhenProvidedInvaildCredentialsAndValidPageAndImage_ThrowsException(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var pageClient = new PageApi(client);

            var exception = Assert.Throws<ErrorResponseException>(() => pageClient.UploadImage("rasha25", "my image", File.ReadAllBytes("jpg.jpg"), "image/jpeg"));

            Assert.That(exception.Response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void UploadImage_WhenProvidedVaildCredentialsAndInvalidPage_ThrowsException(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var pageClient = new PageApi(client);

            var exception = Assert.Throws<ErrorResponseException>(() => pageClient.UploadImage(Guid.NewGuid().ToString(), "my image", File.ReadAllBytes("jpg.jpg"), "image/jpeg"));

            Assert.That(exception.Response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
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
