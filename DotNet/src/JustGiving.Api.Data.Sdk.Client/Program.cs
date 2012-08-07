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
            var success = false;
            GetDonationPayment(ref success);
            
            if (success)
                return;

            GetGiftAidPayment(ref success);
              if (success)
                return;

            GetPaymentList(ref success);
            if (success)
                return;
        }

        private static void GetPaymentList(ref bool success)
        {
            if (_startDate != null)
            {
                if (_endDate == null)
                {
                    Console.WriteLine("You must also specify an end date.");
                    Console.WriteLine("Try 'jgdata --help' for more information.");
                    return;
                }
            }

            if (_endDate != null)
            {
                if (_startDate == null)
                {
                    Console.WriteLine("You must also specify a start date.");
                    Console.WriteLine("Try 'jgdata --help' for more information.");
                    return;
                }
            }

            if (_startDate == null && _endDate == null)
            {
                Console.WriteLine("No payment id or date range specified.");
                Console.WriteLine("Try 'jgdata --help' for more information.");
                return;
            }

            PaymentReport.RetrievePaymentList(_startDate.Value, _endDate.Value, _excelFile);
            success = true;
        }

        private static void GetGiftAidPayment(ref bool success)
        {
            if (_giftAidPayentId != null)
            {
                if (_startDate != null || _endDate != null)
                {
                    Console.WriteLine("You cannot specify date ranges when retrieving a single payment.");
                    Console.WriteLine("Try 'jgdata --help' for more information.");
                    return;
                }

                try
                {
                    PaymentReport.RetrieveSingleGiftAidPayment(_giftAidPayentId.Value, _excelFile);
                    success = true;
                }
                catch (ResourceNotFoundException)
                {
                    Console.WriteLine("404 Not found");
                }
            }
        }

        private static void GetDonationPayment(ref bool success)
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
                    success = true;
                }
                catch (ResourceNotFoundException)
                {
                    Console.WriteLine("404 Not found");
                }
            }
        }

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
