using System;
using System.IO;
using JustGiving.Api.Sdk.Http;
using Mono.Options;

namespace JustGiving.Api.Data.Sdk.Client
{
    class Program
    {
        private static bool _showHelp;
        private static int? _paymentId;
        private static int? _giftAidPayentId;
        private static DateTime? _startDate;
        private static DateTime? _endDate;
        private static FileInfo _excelFile;

        static void Main(string[] args)
        {

            var options = new OptionSet
                              {
                                  {"p|payment=", "a payment id", (int v) => _paymentId = v},
                                  {"g|giftaid=", "a Gift Aid payment id", (int v) => _giftAidPayentId = v},
                                  {"s|startdate=", "the date to view payments from", (DateTime v) => _startDate = v},
                                  {"e|enddate=", "the date to view payments until", (DateTime v) => _endDate = v},
                                  {"x|excel=", "save the data as an excel file to the specified location",  v => _excelFile = new FileInfo(v)},
                                  {"h|help", "show this message and exit", v => _showHelp = v != null}

                            };

            try
            {
                options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try 'jgdata --help' for more information.");

                return;
            }

            if (_showHelp)
            {
                ShowHelp(options);
                return;
            }

            GetDonationPayment();


            //if (_giftAidPayentId != null)
            //{
            //    if (_startDate != null || _endDate != null)
            //    {
            //        Console.WriteLine("You cannot specify date ranges when retrieving a single payment.");
            //        Console.WriteLine("Try 'jgdata --help' for more information.");
            //        return;
            //    }

            //    try
            //    {
            //        RetrieveSingleGiftAidPayment(_giftAidPayentId.Value);
            //    }
            //    catch (ResourceNotFoundException)
            //    {
            //        Console.WriteLine("404 Not found");
            //    }

            //    return;
            //}


        }

        private static void GetDonationPayment()
        {
            if (_paymentId != null)
            {
                if (_startDate != null || _endDate != null)
                {
                    Console.WriteLine("You cannot specify date ranges when retrieving a single payment.");
                    Console.WriteLine("Try 'jgdata --help' for more information.");
                    return;
                }

                try
                {
                    PaymentReport.RetrieveSingleDonationPayment(_paymentId.Value, _excelFile);
                }
                catch (ResourceNotFoundException)
                {
                    Console.WriteLine("404 Not found");
                }

                return;
            }
        }

        //private static void RetrieveSingleDonationPayment(int paymentId)
        //{
        //    var client = CreateClient();
        //    var paymentsClient = new PaymentsApi(client.HttpChannel);

        //    if (_excelFile == null)
        //    {
        //        var paymentReport = paymentsClient.RetrieveReport<DonatoionPayment>(paymentId);
        //        foreach (var item in paymentReport.Donations)
        //        {
        //            Console.WriteLine("£{0} on {1:dd/MM/yyyy} from donor {2} {3} who said: '{4}'", item.Amount, item.Date, item.Donor.FirstName, item.Donor.LastName, item.MessageFromDonor);
        //        }
        //    }
        //    else
        //    {

        //        var excelData = paymentsClient.RetrieveReport(paymentId, DataFileFormat.excel);

        //        using (var fs = new FileStream(_excelFile.FullName, FileMode.Create))
        //        {
        //            fs.Write(excelData, 0, excelData.Length);
        //            fs.Close();
        //        }
        //        Console.WriteLine("Saved!");
        //    }
        //}

        //private static void RetrieveSingleGiftAidPayment(int giftAidPaymentId)
        //{
        //    var client = CreateClient();
        //    var paymentsClient = new PaymentsApi(client.HttpChannel);
            
        //    if (_excelFile == null)
        //    {
        //        var paymentReport = paymentsClient.RetrieveReport<GiftAidPayment>(giftAidPaymentId);
        //        foreach (var item in paymentReport.Donations)
        //        {
        //            Console.WriteLine("£{0} Gift Aid on {1:dd/MM/yyyy} from a donation of {2}", item.NetGiftAidAmount, item.Date, item.Amount);
        //        }
        //    }
        //    else
        //    {
        //        var excelData = paymentsClient.RetrieveReport(giftAidPaymentId, DataFileFormat.excel);

        //        using (var fs = new FileStream(_excelFile.FullName, FileMode.Create))
        //        {
        //            fs.Write(excelData, 0, excelData.Length);
        //            fs.Close();
        //        }
        //        Console.WriteLine("Saved!");
        //    }

        //}

        

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: jgdata [OPTIONS]");
            Console.WriteLine("Query the JustGiving Data API for lists of payments within a given date range,");
            Console.WriteLine("and retrieve detailed donation data for individual payments.");
            Console.WriteLine();

            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine();
            Console.WriteLine("jgdata -i 1062979 -x C:\\report.xls");
            Console.WriteLine();
            Console.WriteLine("jgdata -s 01/01/2010 -e 31/12/2010 ");
        }
    }
}
