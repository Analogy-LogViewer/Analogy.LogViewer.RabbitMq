using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Drawing;
using Analogy.LogViewer.RabbitMq.Properties;

namespace Analogy.LogViewer.RabbitMq
{
  public class RabbitMqFactory : IAnalogyFactory
  {
    internal static Guid Id = new Guid("AB966FE4-8D31-4114-9260-74B22A6D61C8");
    internal static string _title = "RabbitMq";

    public void RegisterNotificationCallback(INotificationReporter notificationReporter)
    {
        
    }

    public Guid FactoryId { get; set; } = Id;

    public string Title { get; set; } = "RabbitMq";

    public IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = RabbitMqChangeLog.GetChangeLog();

    public Image LargeImage { get; set; } = (Image) Resources.rabbitmq32x32;

    public Image SmallImage { get; set; } = (Image) Resources.rabbitmq16x16;

    public IEnumerable<string> Contributors { get; set; }

    public string About { get; set; }
  }
}
