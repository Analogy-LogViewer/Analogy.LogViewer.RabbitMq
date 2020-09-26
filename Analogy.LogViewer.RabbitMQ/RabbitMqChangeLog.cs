using Analogy.Interfaces;
using System.Collections.Generic;

namespace Analogy.LogViewer.RabbitMq
{
    public static class RabbitMqChangeLog
    {
        public static IEnumerable<IAnalogyChangeLog> GetChangeLog() => new List<IAnalogyChangeLog>(0);
    }
}
