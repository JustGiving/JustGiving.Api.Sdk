using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;
using JustGiving.Api.Sdk.Model;
using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class AccountApi : ApiClientBase, IAccountApi
    {
        public override string ResourceBase
        {
            get { return "{apiKey}/v{apiVersion}/account"; }
        }

        public AccountApi(HttpChannel channel)
            : base(channel)
        {
        }

        public string Create(CreateAccountRequest request)
        {
            return HttpChannel.Put<CreateAccountRequest, AccountRegistrationConfirmation>(ResourceBase, request).Email;
        }

        public void CreateAsync(CreateAccountRequest request, Action<string> callback)
        {
            HttpChannel.PutAsync<CreateAccountRequest, AccountRegistrationConfirmation>(ResourceBase, request, response => callback(response.Email));
        }

        private string ListAllPagesLocationFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email", "Email cannot be null or empty.");
            }

            return ResourceBase + "/" + email + "/pages";
        }

        public FundraisingPageSummaries ListAllPages(string email)
        {
            var locationFormat = ListAllPagesLocationFormat(email);
            return HttpChannel.Get<FundraisingPageSummaries>(locationFormat);
        }

        public void ListAllPagesAsync(string email, Action<FundraisingPageSummaries> callback)
        {
            var locationFormat = ListAllPagesLocationFormat(email);
            HttpChannel.GetAsync(locationFormat, callback);
        }

        private string IsEmailRegisteredLocationFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email", "Email cannot be null or empty.");

            return ResourceBase + "/" + email;
        }

        public bool IsEmailRegistered(string email)
        {
            var locationFormat = IsEmailRegisteredLocationFormat(email);
            var response = HttpChannel.PerformRawRequest("HEAD", locationFormat);
            return ProcessIsEmailRegisteredResponse(response);
        }

        public void IsEmailRegisteredAsync(string email, Action<bool> callback)
        {
            var locationFormat = IsEmailRegisteredLocationFormat(email);
            HttpChannel.PerformRawRequestAsync("HEAD", locationFormat, response => callback(ProcessIsEmailRegisteredResponse(response)));
        }

        private static bool ProcessIsEmailRegisteredResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.NotFound:
                    return false;
                default:
                    throw ErrorResponseExceptionFactory.CreateException(response, null);
            }
        }

        private string RequestPasswordReminderLocationFormat(string email)
        {
            return ResourceBase + "/" + email + "/requestpasswordreminder";
        }

        public void RequestPasswordReminder(string email)
        {
            if (string.IsNullOrEmpty(email)) { throw new ArgumentNullException("email", "Email cannot be null or empty."); }

            var locationFormat = RequestPasswordReminderLocationFormat(email);
            var response = HttpChannel.PerformRawRequest("GET", locationFormat);
            ProcessRequestPasswordReminder(response);
        }

        public void RequestPasswordReminderAsync(string email)
        {
            var locationFormat = RequestPasswordReminderLocationFormat(email);
            HttpChannel.PerformRawRequestAsync("GET", locationFormat, ProcessRequestPasswordReminder);
        }

        private string RetrieveAccountLocationFormat()
        {
            if (string.IsNullOrEmpty(HttpChannel.ClientConfiguration.Username) || string.IsNullOrEmpty(HttpChannel.ClientConfiguration.Password))
            {
                throw new Exception("Authentication required to retrieve account details.");
            }

            return ResourceBase + "/";
        }

        public AccountDetails RetrieveAccount()
        {
            var locationFormat = RetrieveAccountLocationFormat();
            return HttpChannel.PerformRequest<AccountDetails>("GET", locationFormat);
        }

        private static void ProcessRequestPasswordReminder(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }

            throw ErrorResponseExceptionFactory.CreateException(response, null);
        }

        public bool AreCredentialsValid(string email, string password)
        {
            var request = new ValidateUser { Email = email, Password = password };
            var response = HttpChannel.PerformRequest<ValidateUser, ValidateUserCommandResponse>("POST", ResourceBase + "/validate", request);
            return response.IsValid;
        }

        public void AreCredentialsValidAsync(string email, string password, Action<bool> callback)
        {
            var request = new ValidateUser { Email = email, Password = password };
            HttpChannel.PerformRequestAsync<ValidateUser, ValidateUserCommandResponse>("POST",
                                                                                       ResourceBase + "/validate",
                                                                                       request,
                                                                                       response => callback(response.IsValid));
        }

        private string ChangePasswordLocationFormat()
        {
            return ResourceBase + "/changePassword";
        }

        public bool ChangePassword(ChangePasswordRequest changePasswordRequest)
        {

            var locationFormat = ChangePasswordLocationFormat();
            var response = HttpChannel.PerformRequest<ChangePasswordRequest, ChangePasswordResponse>("POST",
                                                                                                     locationFormat,
                                                                                                     changePasswordRequest);
            return response.Success;
        }

        private string ContentRatingHistoryResourceEndpoint()
        {
            return ResourceBase + "/rating";
        }

        public ContentRatingHistoryResponse ContentRatingHistory()
        {
            var resourceEndpoint = ContentRatingHistoryResourceEndpoint();
            return HttpChannel.PerformRequest<ContentRatingHistoryResponse>("GET", resourceEndpoint);
        }

        public void ContentRatingHistoryAsync(Action<ContentRatingHistoryResponse> callback)
        {
            var resourceEndpoint = ContentRatingHistoryResourceEndpoint();
            HttpChannel.GetAsync(resourceEndpoint, callback);
        }

        private string RateContentResourceEndpoint()
        {
            return ResourceBase + "/rating";
        }

        public bool RateContent(RateContentRequest rateContentRequest)
        {
            var resourceEndpoint = RateContentResourceEndpoint();
            var result = HttpChannel.PerformRawRequest("POST", resourceEndpoint, rateContentRequest);
            if (result.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string ContentFeedResourceEndpoint()
        {
            return ResourceBase + "/feed";
        }

        public ContentFeedResponse ContentFeed()
        {
            var resourceEndpoint = ContentFeedResourceEndpoint();
            var result = HttpChannel.PerformRequest<ContentFeedResponse>("GET", resourceEndpoint);
            return result;
        }

        private string InterestResourceEndpoint()
        {
            return ResourceBase + "/interest";
        }

        public InterestResponse Interest()
        {
            var resourceEndpoint = InterestResourceEndpoint();
            var result = HttpChannel.PerformRequest<InterestResponse>("GET", resourceEndpoint);
            return result;
        }

        public void InterestAsync(Action<AccountApi.InterestResponse> callback)
        {
            var resourceEndpoint = InterestResourceEndpoint();
            HttpChannel.GetAsync(resourceEndpoint, callback);
        }

        public bool AddInterest(UserInterest interest)
        {
            var resourceEndpoint = InterestResourceEndpoint();
            var result = HttpChannel.PerformRawRequest("POST", resourceEndpoint, interest);
            if (result.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [DataContract(Name = "interests", Namespace = "")]
        public class UserInterest
        {
            [DataMember(Name = "interest")]
            public string Interest { get; set; }
        }

        [CollectionDataContract(Name = "interests", ItemName = "interest", Namespace = "")]
        public class InterestResponse : List<string>
        {
            
        }

        [DataContract(Namespace = "", Name = "contentFeedResponse")]
        public class ContentFeedResponse
        {
            [DataMember(Name = "xmlns")]
            public string Xmlns { get; set; }

            [DataMember(Name = "title")]
            public Title Title { get; set; }

            [DataMember(Name = "subtitle")]
            public Subtitle Subtitle { get; set; }

            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "rights")]
            public Rights Rights { get; set; }

            [DataMember(Name = "updated")]
            private string RawUpdatedData { get; set; }

            [IgnoreDataMember]
            public DateTime Updated
            {
                get { return DateTime.Parse(RawUpdatedData, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind); }
                set { RawUpdatedData = value.ToString("o"); }
            }

            [DataMember(Name = "generator")]
            public string Generator { get; set; }

            [DataMember(Name = "link")]
            public List<Link> Links { get; set; }

            [DataMember(Name = "entry")]
            public List<Entry> Entries { get; set; }
        }

        [DataContract]
        public class Entry
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "title")]
            public Title Title { get; set; }

            [DataMember(Name = "updated")]
            private string RawUpdatedDate { get; set; }

            [IgnoreDataMember]
            public DateTime Updated
            {
                get { return DateTime.Parse(RawUpdatedDate, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind); }
                set { RawUpdatedDate = value.ToString("s"); }
            }

            [DataMember(Name = "author")]
            public Author Author { get; set; }

            [DataMember(Name = "link")]
            public Link Link { get; set; }

            [DataMember(Name = "content")]
            public Content Content { get; set; }

            [DataMember(Name = "datatype")]
            public Datatype Datatype { get; set; }

            [DataMember(Name = "pageShortName")]
            public PageShortName PageShortName { get; set; }

            [DataMember(Name = "jgApiId")]
            public JgApiId JgApiId { get; set; }

            [DataMember(Name = "images")]
            public Images Images { get; set; }
        }

        [DataContract]
        public class Images
        {
            [DataMember(Name = "xmlns")]
            public string Xmlns { get; set; }

            [DataMember(Name = "image")]
            public Image Image { get; set; }
        }

        [DataContract]
        public class Image
        {
            [DataMember(Name = "xmlns")]
            public string Xmlns { get; set; }

            [DataMember(Name = "size")]
            public string Size { get; set; }

            [DataMember(Name = "uri")]
            public string Uri { get; set; }
        }

        [DataContract]
        public class JgApiId : Datatype
        {

        }

        [DataContract]
        public class PageShortName : Datatype
        {

        }

        [DataContract]
        public class Datatype
        {
            [DataMember(Name = "xmlns")]
            public string Xmlns { get; set; }

            [DataMember(Name = "text")]
            public string Text { get; set; }
        }

        [DataContract]
        public class Content : Title
        {

        }

        [DataContract]
        public class Author
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "uri")]
            public string Uri { get; set; }

            [DataMember(Name = "email")]
            public string Email { get; set; }
        }

        [DataContract]
        public class Link
        {
            [DataMember(Name = "rel")]
            public string Rel { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "href")]
            public string Href { get; set; }
        }

        [DataContract]
        public class Rights : Title
        {

        }

        [DataContract]
        public class Subtitle : Title
        {

        }

        [DataContract]
        public class Title
        {
            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "text")]
            public string Text { get; set; }
        }

        [DataContract(Namespace = "", Name = "rateContentRequest")]
        public class RateContentRequest : Rating
        {

        }

        [DataContract(Namespace = "", Name = "contentRatings")]
        public class ContentRatingHistoryResponse
        {
            [DataMember(Name = "ratings")]
            public List<Rating> Ratings { get; set; }

            [DataMember(Name = "pagination")]
            public Pagination Pagination { get; set; }
        }

        [DataContract(Namespace = "", Name = "rating")]
        public class Rating
        {
            [DataMember(Name = "intent")]
            public string Intent { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "contentData")]
            public string ContentData { get; set; }

            [DataMember(Name = "created")]
            public DateTime Created { get; set; }

            [DataMember(Name = "updated")]
            public DateTime Updated { get; set; }
        }

        [DataContract(Namespace = "", Name = "changePassword")]
        public class ChangePasswordRequest
        {
            [DataMember(Name = "emailAddress")]
            public string EmailAddress { get; set; }

            [DataMember(Name = "newPassword")]
            public string NewPassword { get; set; }

            [DataMember(Name = "currentPassword")]
            public string CurrentPassword { get; set; }
        }

        [DataContract(Namespace = "", Name = "changePasswordResponse")]
        public class ChangePasswordResponse
        {
            [DataMember(Name = "success")]
            public bool Success { get; set; }
        }
    }
}