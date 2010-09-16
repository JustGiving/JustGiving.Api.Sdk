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
    public class PageApiTests
    {
        [Test]
        public void Register_WhenProvidedWithValidAuthenticationAndDetails_CreatesANewPage()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "password" });
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

        [Test]
        public void ListPages_WhenProvidedCredentials_ReturnsPages()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "password" });
            var pageClient = new PageApi(client);

            var pageData = pageClient.ListAll();
        }

        [Test]
        public void RetrievePage_WhenProvidedWithAKnownPage_ReturnsPublicPageView()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "incorrectPassword" });
            var pageClient = new PageApi(client);

            var pageData = pageClient.RetrievePage("rasha25");
        }

        [Test]
        public void RetrieveDonationsForPage_WhenProvidedWithAKnownPageAndRequesterIsThePageOwner_ReturnsDonations()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "password" });
            var pageClient = new PageApi(client);

            var pageData = pageClient.RetrieveDonationsForPage("david25");
        }

        [Test]
        public void RetrieveDonationsForPage_WhenProvidedWithAKnownPageAndRequesterIsAnon_ReturnsDonations()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1));
            var pageClient = new PageApi(client);

            var pageData = pageClient.RetrieveDonationsForPage("rasha25");
        }

        [Test]
        public void IsPageShortNameRegistered_WhenSuppliedKnownExitingPage_ReturnsTrue()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "incorrectPassword" });
            var pageClient = new PageApi(client);

            var exists = pageClient.IsPageShortNameRegistered("rasha25");

            Assert.IsTrue(exists);
        }

        [Test]
        public void UpdatePageStory_WhenProvidedCredentialsAndValidPage_PostsUpdate()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "password" });
            var pageClient = new PageApi(client);

            pageClient.UpdateStory("api-test-128c6f46-0356-4c06-8988-be7fd6cd2eba", DateTime.Now + ": Unit Test Update");
        }

        [Test]
        public void UploadImage_WhenProvidedCredentialsAndValidPageAndImage_UploadsImage()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "password" });
            var pageClient = new PageApi(client);

            pageClient.UploadImage("api-test-128c6f46-0356-4c06-8988-be7fd6cd2eba", "my image", File.ReadAllBytes("jpg.jpg"), "image/jpeg");
        }

        [Test]
        public void UploadImage_WhenProvidedInvaildCredentialsAndValidPageAndImage_ThrowsException()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "badincorrectPassword" });
            var pageClient = new PageApi(client);

            var exception = Assert.Throws<ErrorResponseException>(() => pageClient.UploadImage("david25", "my image", File.ReadAllBytes("jpg.jpg"), "image/jpeg"));


        }

        [Test]
        public void IsPageShortNameRegistered_WhenSuppliedPageNameUnlikelyToExist_ReturnsFalse()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "incorrectPassword" });
            var pageClient = new PageApi(client);

            var exists = pageClient.IsPageShortNameRegistered(Guid.NewGuid().ToString());

            Assert.IsFalse(exists);
        }
    }
}
