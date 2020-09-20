using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Analogy.LogViewer.RabbitMq;
using Analogy.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Message =  Analogy.LogViewer.RabbitMq.Message;

namespace RabbitMqLoggerUnitTests
{
    [TestClass]
    public class UnitTest
    {
        public string fileName = "testLog.log";

        [TestMethod]
        public void TestDateTimeConvert()
        {
            var msg = new Message();
            var random = new Random();
            DateTime date = DateTime.Now;
            date = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
            var dateStr = date.ToString("d");
            var timeStr = date.TimeOfDay.ToString();
            var DateTimeFromStr = msg.GetDateTimeFromStrings(dateStr, timeStr, CultureInfo.CurrentCulture);

            Assert.IsTrue(date.Equals(DateTimeFromStr));

        }


        [TestMethod]
        public void RabitMqParserTest()
        {
            RabbitMqLogReader reader = new RabbitMqLogReader();
            CancellationTokenSource cts = new CancellationTokenSource();
            MessageHandlerForTesting forTesting = new MessageHandlerForTesting();
            var task = reader.Process(fileName, cts.Token, forTesting);
            var messages = task.Result;
            Assert.IsTrue(messages.Count() == 8);






        }
    }
}
