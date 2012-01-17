using System;
using System.IO;
using System.Net;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model;
using JustGiving.Api.Sdk.Model.Page;
using JustGiving.Api.Sdk.Model.Remember;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class PageApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenProvidedWithValidAuthenticationAndDetails_CreatesANewPage(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
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
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
            var pageShortName = "api-test-" + Guid.NewGuid();
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = null,
                PageShortName = pageShortName,
                PageTitle = "When Provided With Valid Authentication Details And An Empty Activity Type - Creates New Page",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                EventId = 1,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            var registrationResponse = pageClient.Create(pageCreationRequest);

            Assert.That(registrationResponse.Next.Uri, Is.StringContaining(pageShortName));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenProvidedWithANonDefaultDomain_CreatesANewPageOnThatDomain(WireDataFormat format)
        {
            const string domain = "rfl.staging.justgiving.com";

            var client = TestContext.CreateClientValidCredentials(format);
            client.SetWhiteLabelDomain(domain);

			var pageClient = new PageApi(client.HttpChannel);
            
            var pageShortName = "api-test-" + Guid.NewGuid();
            
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = null,
                Attribution =  null,
                CharityId = 2050,
                PageShortName = pageShortName,
                PageTitle = "Page created on domain " + domain + " by an integration test",
                EventDate = null,
                EventName = null,
                EventId = 1,
                TargetAmount = null
            };

            var registrationResponse = pageClient.Create(pageCreationRequest);

            Assert.That(registrationResponse.Next.Uri, Is.StringContaining(domain));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RegisterWhenProvidedWithADomainThatDoesNotExist_ThrowsException(WireDataFormat format)
        {
            const string domainThatDoesNotExistOnJustGiving = "Incorrect.com";

            var client = TestContext.CreateClientValidCredentials(format);
            client.SetWhiteLabelDomain(domainThatDoesNotExistOnJustGiving);

			var pageClient = new PageApi(client.HttpChannel);
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

            var exception = Assert.Throws<ErrorResponseException>(() => pageClient.Create(pageCreationRequest));

            Assert.That(exception.Response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenProvidedWithValidAuthenticationAndDetailsAndAnEmptyActivityType_TheResponseContainsThePageId(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
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

            Assert.That(registrationResponse.PageId != 0);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_SuppliedValidAuthenticationAndValidRegisterPageRequestWithCompanyAppealId_CanRetrieveCompanyId(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
            var pageShortName = "api-test-" + Guid.NewGuid();
            const int companyAppealId = 200002;
            var pageCreationRequest = new RegisterPageRequest
            {
                CompanyAppealId = companyAppealId,
                ActivityType = null,
                PageShortName = pageShortName,
                PageTitle = "api test",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                EventId = 1,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            pageClient.Create(pageCreationRequest);
            Assert.That(pageClient.Retrieve(pageShortName).CompanyAppealId, Is.EqualTo(companyAppealId));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_SuppliedValidAuthenticationAndValidRegisterPageRequestWithInMemName_CanRetrieveNameFromAttribution(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
            var pageShortName = "api-test-" + Guid.NewGuid();
            const string inMemName = "Matheu";
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.InMemory,
                Attribution = inMemName,
                PageShortName = pageShortName,
                PageTitle = "api test InMem Name",
                EventName = "The InMem ApiTest",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            pageClient.Create(pageCreationRequest);
            FundraisingPage page = pageClient.Retrieve(pageShortName);
            Assert.That(page.Attribution, Is.EqualTo(inMemName));
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
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

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
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

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
        	                          		EventDate = DateTime.Now.AddDays(5),
        	                          		CustomCodes =
        	                          			new PageCustomCodes
        	                          				{
        	                          					CustomCode1 = "code1",
        	                          					CustomCode2 = "code2",
        	                          					CustomCode3 = "code3",
        	                          					CustomCode4 = "code4",
        	                          					CustomCode5 = "code5",
        	                          					CustomCode6 = "code6"
        	                          				}
        	                          	};
            pageClient.Create(pageCreationRequest);

            // Act
            var pageData = pageClient.Retrieve(pageShortName);

            Assert.NotNull(pageData);
            Assert.That(pageData.PageCreatorName, Is.StringContaining("Test Test"));
            Assert.AreEqual(pageData.PageShortName, pageCreationRequest.PageShortName);
            Assert.AreEqual(pageData.PageTitle, pageCreationRequest.PageTitle);
            Assert.AreEqual(pageData.EventName, pageCreationRequest.EventName);
            Assert.AreEqual(pageData.TargetAmount, pageCreationRequest.TargetAmount);
            Assert.IsNotNullOrEmpty(pageData.SmsCode);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePage_WhenProvidedWithABadPage_ThrowsResourceNotFoundException(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

            var exception = Assert.Throws<ResourceNotFoundException>(() => pageClient.Retrieve(Guid.NewGuid().ToString()));

            Assert.IsInstanceOf<ResourceNotFoundException>(exception);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveDonationsForPage_WhenProvidedWithAKnownPageAndRequesterIsAnon_ReturnsDonations(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
            
            var pageData = pageClient.RetrieveDonationsForPage("rasha25");

            Assert.That(pageData.Donations.Count, Is.GreaterThan(0));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveImagesForPage_WhenProvidedWithAKnownPageAndRequesterIsAnon_ReturnsImages(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
            var pageClient = new PageApi(client.HttpChannel);

            var pageData = pageClient.GetImages(new GetFundraisingPageImagesRequest() { PageShortName = "rasha25" });

            Assert.That(pageData.Count, Is.GreaterThan(0));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveVideosForPage_WhenProvidedWithAKnownPageAndRequesterIsAnon_ReturnsVideos(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
            var pageClient = new PageApi(client.HttpChannel);

            var pageData = pageClient.GetVideos(new GetFundraisingPageVideosRequest() { PageShortName = "rasha25" });

            Assert.That(pageData.Count, Is.GreaterThan(0));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsPageShortNameRegistered_WhenSuppliedKnownExistingPage_ReturnsTrue(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

            var exists = pageClient.IsPageShortNameRegistered("rasha25");

            Assert.IsTrue(exists);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void UpdatePageStory_WhenProvidedCredentialsAndValidPage_PostsUpdate(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

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
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
            
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
            var client = TestContext.CreateClientInvalidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

            var exception = Assert.Throws<ErrorResponseException>(() => pageClient.UploadImage("rasha25", "my image", File.ReadAllBytes("jpg.jpg"), "image/jpeg"));

            Assert.That(exception.Response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void UploadImage_WhenProvidedVaildCredentialsAndInvalidPage_ThrowsException(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

            var exception = Assert.Throws<ErrorResponseException>(() => pageClient.UploadImage(Guid.NewGuid().ToString(), "my image", File.ReadAllBytes("jpg.jpg"), "image/jpeg"));

            Assert.That(exception.Response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsPageShortNameRegistered_WhenSuppliedPageNameUnlikelyToExist_ReturnsFalse(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

            var exists = pageClient.IsPageShortNameRegistered(Guid.NewGuid().ToString());

            Assert.IsFalse(exists);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsPageShortNameRegistered_WhenSuppliedPageNameUnlikelyToExistOnNonDefaultDomain_ReturnsFalse(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

            var exists = pageClient.IsPageShortNameRegistered("rasha25", "rfl.staging.justgiving.com");

            Assert.IsFalse(exists);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsPageShortNameRegistered_WhenSuppliedUnknownDomain_ThrowsException(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
            const string unknownDomain = "unknownDomain.justgiving.com";

            var exception = Assert.Throws<ErrorResponseException>(() => pageClient.IsPageShortNameRegistered("rasha25", unknownDomain));

            Assert.That(exception.Response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
		[Ignore("Not yet live")]
        public void AddFundraisingPageImage_WhenCredentialsValidAndRequestNotValid_ThrowsException(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

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

            var addImageRequest = new AddFundraisingPageImageRequest {Url = "", Caption = "", PageShortName=pageCreationRequest.PageShortName};
            var response = Assert.Throws<ErrorResponseException>(()=>pageClient.AddImage(addImageRequest));
            Assert.That(response.Response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
		[Ignore("Not yet live")]
        public void AddFundraisingPageVideo_WhenCredentialsValidAndRequestNotValid_ThrowsException(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

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

            var addVideoRequest = new AddFundraisingPageVideoRequest { Url = "", Caption = "", PageShortName = pageCreationRequest.PageShortName };
            var response = Assert.Throws<ErrorResponseException>(() => pageClient.AddVideo(addVideoRequest));
            Assert.That(response.Response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
		[Ignore("Not yet live")]
        public void AddFundraisingPageImage_WhenCredentialsValidAndRequestValid_ReturnsSuccessful(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

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

            var addImageRequest = new AddFundraisingPageImageRequest { Url = "http://placehold.it/350x150", Caption = "test image", PageShortName = pageCreationRequest.PageShortName };
            pageClient.AddImage(addImageRequest);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
		[Ignore("Not yet live")]
        public void AddFundraisingPageVideo_WhenCredentialsValidAndRequestValid_ReturnsSuccessful(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);

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

            var addVideoRequest = new AddFundraisingPageVideoRequest { Url = "http://www.youtube.com/watch?v=MSxjbF18BBM", Caption = "neckbrace", PageShortName = pageCreationRequest.PageShortName };
            pageClient.AddVideo(addVideoRequest);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Cannot_Create_Page_For_Event_Using_Event_Reference_And_Id(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
            var pageShortName = "api-test-" + Guid.NewGuid();
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = null,
                PageShortName = pageShortName,
                PageTitle = "When Provided With Valid Authentication Details And An Empty Activity Type - Creates New Page",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                EventId = 1,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            var ex = Assert.Throws<ErrorResponseException>(() => pageClient.Create("foo", pageCreationRequest));
            Assert.That(ex.Response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));           
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Can_Create_Page_For_Event_Using_Event_Reference(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
            var pageShortName = "api-test-" + Guid.NewGuid();
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = null,
                PageShortName = pageShortName,
                PageTitle = "When Provided With Valid Authentication Details And An Empty Activity Type - Creates New Page",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };

            pageClient.Create("341_RFL2010", pageCreationRequest);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Can_Create_Page_With_Custom_Theme(WireDataFormat format)
        {
            Create_Page_With_Custom_Theme(format);
        }

        public string Create_Page_With_Custom_Theme(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var pageClient = new PageApi(client.HttpChannel);
            var pageShortName = "api-test-" + Guid.NewGuid();
            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.OtherCelebration,
                PageShortName = pageShortName,
                PageTitle = "Page with custom theme",
                EventName = "Test",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5),
                Theme = new PageTheme { BackgroundColour = "#FF0000", ButtonColour = "#FFFF00", ButtonTextColour = "#00FF00", TitleColour = "#0000FF" }
            };

            pageClient.Create(pageCreationRequest);

            return pageShortName;
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_SuppliedValidAuthenticationAndValidRegisterPageRequestWithRememberedPersonId_CanRetrievePageWithRememberedPersonData(WireDataFormat format)
        {
            var guid = Guid.NewGuid();
            var client = TestContext.CreateClientValidCredentials(format);
            var pageClient = new PageApi(client.HttpChannel);
            var pageShortName = "api-test-" + guid;

            var firstName = "FirstName-api-test";
            var lastName = string.Format("Last-{0}", guid);

            string inMemNameAttribution = String.Format("{0} {1}{2}", firstName, lastName, guid).Trim();

            var rememberedPersonReference = new RememberedPersonReference
                                       {
                                           Relationship = "Other",
                                           RememberedPerson = new RememberedPerson
                                                                  {
                                                                      Id = 132,
                                                                  },
                                       };

            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.InMemory,
                Attribution = inMemNameAttribution,
                PageShortName = pageShortName,
                PageTitle = "api test InMem Name",
                EventName = "The InMem ApiTest",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5),
                RememberedPersonReference = rememberedPersonReference,
            };

            pageClient.Create(pageCreationRequest);
            FundraisingPage page = pageClient.Retrieve(pageShortName);

            Assert.NotNull(page.RememberedPersonSummary.Name);
            Assert.That(page.RememberedPersonSummary.Next.Uri, Is.StringContaining(String.Format("remember/{0}", page.RememberedPersonSummary.Id)));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_SuppliedValidAuthenticationAndValidRegisterPageRequestWithNewRememberedPersonDetails_CanRetrievePageWithRememberedPersonData(WireDataFormat format)
        {
            var guid = Guid.NewGuid();
            var client = TestContext.CreateClientValidCredentials(format);
            var pageClient = new PageApi(client.HttpChannel);
            var pageShortName = "api-test-" + guid;

            var firstName = "FirstName-api-test";
            var lastName = string.Format("Last-{0}", guid);

            string inMemNameAttribution = String.Format("{0} {1}{2}", firstName, lastName, guid).Trim();

            var rememberedPersonReference = new RememberedPersonReference
            {
                Relationship = "Other",
                RememberedPerson = new RememberedPerson
                                       {
                                           FirstName = firstName,
                                           LastName = lastName,
                                           Gender = 1,
                                           Town = String.Format("town-{0}", guid),
                                           DateOfBirth = DateTime.Now.AddYears(-50),
                                           DateOfDeath = DateTime.Now.AddDays(-1),
                                       }
            };

            var pageCreationRequest = new RegisterPageRequest
            {
                ActivityType = ActivityType.InMemory,
                Attribution = inMemNameAttribution,
                PageShortName = pageShortName,
                PageTitle = "api test InMem Name",
                EventName = "The InMem ApiTest",
                CharityId = 2050,
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5),
                RememberedPersonReference = rememberedPersonReference,
            };

            pageClient.Create(pageCreationRequest);
            FundraisingPage page = pageClient.Retrieve(pageShortName);

            Assert.NotNull(page.RememberedPersonSummary.Name);
            Assert.That(page.RememberedPersonSummary.Next.Uri, Is.StringContaining(String.Format("remember/{0}", page.RememberedPersonSummary.Id)));
        }
    }
}
