using Analogy.Interfaces;
using System;
using System.Globalization;

namespace Analogy.LogViewer.RabbitMq
{
    public class Message
    {
        public DateTime TimeStamp { get; set; }

        public LevelEnum Level { get; set; }

        public CategoryEnum Category { get; set; }

        public string ConnectionId { get; set; }

        public string Body { get; set; }

        public AnalogyLogMessage ToAnalogyLogMessage()
        {
            AnalogyLogMessage analogyLogMessage = new AnalogyLogMessage();
            analogyLogMessage.Date = TimeStamp;
            analogyLogMessage.Text = Body;
            analogyLogMessage.Category = Category.ToString();
            return analogyLogMessage;
        }

        public DateTime GetDateTimeFromStrings(string date, string time, CultureInfo culture) => DateTime.Parse(date) + TimeSpan.Parse(time);
    }
}
