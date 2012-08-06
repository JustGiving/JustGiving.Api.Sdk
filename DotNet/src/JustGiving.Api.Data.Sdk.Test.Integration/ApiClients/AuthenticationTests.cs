using System.Net;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Data.Sdk.Model.Payment.Donations;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class AuthenticationTests : ApiTestFixture
    {
        private int _paymentId = 1062979;

        [Test]
        public void AuthenticationSuccess_DoesNotReturnHttp401Unauthorised()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.IsZipSupportedByClient = true);
                
            var client = new JustGivingDataClient(clientConfiguration);
            var paymentClient = new PaymentsApi(client.HttpChannel);

            var payment = paymentClient.RetrieveReport<Payment>(_paymentId);

            Assert.That(payment.HttpStatusCode, Is.Not.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public void AuthenticationFailure_ReturnsHttp401Unauthorised()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.Username = "")
                .With((clientConfig) => clientConfig.Password = "");
            
            var client = new JustGivingDataClient(clientConfiguration);
            var paymentClient = new PaymentsApi(client.HttpChannel);

            var exception = Assert.Throws<ErrorResponseException>(() => paymentClient.RetrieveReport<Payment>(_paymentId));
            Assert.That(exception.Message.Contains("401"));
          
        }
    }
}