using System;
using System.IO;
using JustGiving.Api.Data.Sdk.ApiClients;
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

        private static JustGivingDataClient CreateClient(FileInfo excelFile = null)
        {
            var config = excelFile == null ? new DataClientConfiguration() : new DataClientConfiguration(){WireDataFormat=WireDataFormat.Other};
            return new JustGivingDataClient(config);
        }
    }

}