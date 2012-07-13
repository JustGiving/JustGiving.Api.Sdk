using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using JustGiving.Api.Sdk.Model.Charity;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Unit
{
    [TestFixture]
    [Category("Fast")]
    public class ModelTests
    {
        [TestCase("SampleXml\\CharityResponse.xml")]
        [TestCase("SampleXml\\CharityResponse2.xml")]
        public void CanDeserialiseMobileAppeals(string xmlFile)
        {
            var xml = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, xmlFile));
            var reader = new DataContractSerializer(typeof(Charity));
            var byteArray = Encoding.UTF8.GetBytes(xml);
            var stream = new MemoryStream(byteArray);
            var charity = (Charity)reader.ReadObject(stream);

            Assert.That(charity.MobileAppeals, Is.Not.Null);


        }

        [Test]
        public void SerialiseTest()
        {
            Charity c = new Charity();
            c.Name = "sample";
            c.MobileAppeals = new List<MobileAppeal>();
            c.MobileAppeals.Add(new MobileAppeal(){Name="test1", SmsCode = "12345"});
            c.MobileAppeals.Add(new MobileAppeal() { Name = "test2", SmsCode = "12345" });

            var writer = new DataContractSerializer(typeof(Charity));
            var stream = new MemoryStream();
            writer.WriteObject(stream, c);
            stream.Seek(0, SeekOrigin.Begin);
            var streamreader = new StreamReader(stream);
            Console.WriteLine(streamreader.ReadToEnd());
        }
    }
}
