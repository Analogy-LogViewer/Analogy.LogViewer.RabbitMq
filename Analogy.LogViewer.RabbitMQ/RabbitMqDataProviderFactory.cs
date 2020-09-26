using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using System;
using System.Collections.Generic;

namespace Analogy.LogViewer.RabbitMq
{
  public class RabbitMqDataProviderFactory : IAnalogyDataProvidersFactory
  {
    public Guid FactoryId { get; set; } = RabbitMqFactory.Id;

    public string Title { get; set; } = RabbitMqFactory._title;

    public IEnumerable<IAnalogyDataProvider> DataProviders { get; }

    public RabbitMqDataProviderFactory()
    {
      List<IAnalogyDataProvider> ianalogyDataProviderList = new List<IAnalogyDataProvider>();
      ianalogyDataProviderList.Add((IAnalogyDataProvider) new RabbitMqOfflineDataProvider());
      DataProviders = (IEnumerable<IAnalogyDataProvider>) ianalogyDataProviderList;
    }
  }
}
