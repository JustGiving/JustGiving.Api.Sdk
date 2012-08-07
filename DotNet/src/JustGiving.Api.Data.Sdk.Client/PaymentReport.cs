using System;
using System.Configuration;
using System.IO;
using System.Linq;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Data.Sdk.Configuration;
using JustGiving.Api.Data.Sdk.Model;
using JustGiving.Api.Sdk;
using GiftAidPayment = JustGiving.Api.Data.Sdk.Model.Payment.GiftAid.Payment;
using DonatoionPayment = JustGiving.Api.Data.Sdk.Model.Payment.Donations.Payment;


namespace JustGiving.Api.Data.Sdk.Client
{
    public class PaymentReport
    {
        public static void RetrieveSingleDonationPayment(int paymentId, FileInfo excelFile)
        {
            var client = CreateClient();
            var paymentsClient = new PaymentsApi(client.HttpChannel);

            if (excelFile == null)
            {
                var paymentReport = paymentsClient.RetrieveReport<DonatoionPayment>(paymentId);
                foreach (var item in paymentReport.Donations)
                {
                    Console.WriteLine("£{0} on {1:dd/MM/yyyy} from donor {2} {3} who said: '{4}'", item.Amount, item.Date, item.Donor.FirstName, item.Donor.LastName, item.MessageFromDonor);
                }
            }
            else
            {

                var excelData = paymentsClient.RetrieveReport(paymentId, DataFileFormat.excel);

                using (var fs = new FileStream(excelFile.FullName, FileMode.Create))
                {
                    fs.Write(excelData, 0, excelData.Length);
                    fs.Close();
                }
                Console.WriteLine("Saved!");
            }
        }

        public static void RetrieveSingleGiftAidPayment(int giftAidPaymentId, FileInfo excelFile)
        {
            var client = CreateClient();
            var paymentClient = new PaymentsApi(client.HttpChannel);
            if (excelFile == null)
            {
                var paymentReport = paymentClient.RetrieveReport<GiftAidPayment>(giftAidPaymentId);
                foreach (var item in paymentReport.Donations)
                {
                    Console.WriteLine("£{0} Gift Aid on {1:dd/MM/yyyy} from a donation of {2}", item.NetGiftAidAmount, item.Date, item.Amount);
                }
            }
            else
            {
                var excelData = paymentClient.RetrieveReport(giftAidPaymentId, DataFileFormat.excel);

                using (var fs = new FileStream(excelFile.FullName, FileMode.Create))
                {
                    fs.Write(excelData, 0, excelData.Length);
                    fs.Close();
                }
                Console.WriteLine("Saved!");
            }

        }


        public static void RetrievePaymentList(DateTime startDate, DateTime endDate, FileInfo excelFile)
        {
            var client = CreateClient();
            var paymentclient = new PaymentsApi(client.HttpChannel);
            if (excelFile == null)
            {
                var list = paymentclient.RetrievePaymentsBetween(startDate, endDate);
                if (!list.Any())
                    Console.WriteLine("No payments found for the dates entered");

                foreach (var item in list)
                {
                    Console.WriteLine("#{0} £{1} ({2}) paid to account {3} on {4:dd/MM/yyyy}", item.PaymentRef, item.Net, item.PaymentType, item.Account, item.PaymentDate);
                    Console.WriteLine("Full report: {0}", item.Url);
                    Console.WriteLine();
                }
            }
            else
            {
                var excelData = paymentclient.RetrievePaymentsBetween(startDate, endDate, DataFileFormat.excel);

                using (var fs = new FileStream(excelFile.FullName, FileMode.Create))
                {
                    fs.Write(excelData, 0, excelData.Length);
                    fs.Close();
                }
                Console.WriteLine("Saved!");
            }
        }

        private static JustGivingDataClient CreateClient(FileInfo excelFile = null)
        {
            var sdkConfiguration = ConfigurationManager.GetSection("justGivingDataSdk") as JustGivingDataSdkConfiguration;
            var config = excelFile == null 
                ? new DataClientConfiguration { Username = sdkConfiguration.Username, Password = sdkConfiguration.Password } 
                : new DataClientConfiguration { WireDataFormat = WireDataFormat.Other, Username = sdkConfiguration.Username, Password = sdkConfiguration.Password };

            return new JustGivingDataClient(config);
        }
    }

}