using Analogy.Interfaces;
using Analogy.LogViewer.RabbitMq.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Analogy.LogViewer.RabbitMq
{
    public class RabbitMqFactory : Template.PrimaryFactory
    {
        internal static Guid Id = new Guid("AB966FE4-8D31-4114-9260-74B22A6D61C8");
        internal static string _title = "RabbitMq";


        public override Guid FactoryId { get; set; } = Id;

        public override string Title { get; set; } = "RabbitMq";

        public override IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = RabbitMqChangeLog.GetChangeLog();

        public override Image LargeImage { get; set; } = (Image)Resources.rabbitmq32x32;

        public override Image SmallImage { get; set; } = (Image)Resources.rabbitmq16x16;

        public override IEnumerable<string> Contributors { get; set; } = new List<string>();

        public override string About { get; set; } = string.Empty;
    }
}
