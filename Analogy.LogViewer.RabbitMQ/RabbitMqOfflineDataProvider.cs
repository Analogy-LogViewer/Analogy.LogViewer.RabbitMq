using Analogy.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.RabbitMq
{
  public class RabbitMqOfflineDataProvider : IAnalogyOfflineDataProvider, IAnalogyDataProvider
  {
    private static string _title = "RabbitMQ";

    public Task InitializeDataProviderAsync(IAnalogyLogger logger)
    {
      LogReader = new RabbitMqLogReader();
      return Task.CompletedTask;
    }

    public void MessageOpened(AnalogyLogMessage message)
    {
    }

    public Image SmallImage { get; set; }

    public bool DisableFilePoolingOption { get; } = false;

    public bool CanSaveToLogFile { get; } = false;

    public string FileOpenDialogFilters { get; } = "RabbitMQ Log file |*.log";

    public string FileSaveDialogFilters { get; } = string.Empty;

    public IEnumerable<string> SupportFormats { get; } = (IEnumerable<string>) new string[1]
    {
      "*.log"
    };

    public string InitialFolderFullPath => Environment.CurrentDirectory;

    public (Color backgroundColor, Color foregroundColor) GetColorForMessage(
      IAnalogyLogMessage logMessage)
    {
      return (Color.Empty, Color.Empty);
    }

    public IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders() => (IEnumerable<(string, string)>) Array.Empty<(string, string)>();

    public Guid Id { get; set; } = RabbitMqFactory.Id;

    public string OptionalTitle
    {
      get => _title;
      set => _title = value;
    }

    public bool UseCustomColors { get; set; } = false;

    public RabbitMqLogReader LogReader { get; set; }

    public async Task<IEnumerable<AnalogyLogMessage>> Process(
      string fileName,
      CancellationToken token,
      ILogMessageCreatedHandler messagesHandler)
    {
      if (!CanOpenFile(fileName))
      {
          return (IEnumerable<AnalogyLogMessage>) new List<AnalogyLogMessage>(0);
      }

      IEnumerable<AnalogyLogMessage> analogyLogMessages = await LogReader.Process(fileName, token, messagesHandler);
      return analogyLogMessages;
    }

    public IEnumerable<FileInfo> GetSupportedFiles(
      DirectoryInfo dirInfo,
      bool recursiveLoad)
    {
      List<FileInfo> list = ((IEnumerable<FileInfo>) dirInfo.GetFiles("*.txt")).ToList<FileInfo>();
      if (!recursiveLoad)
      {
          return (IEnumerable<FileInfo>) list;
      }

      try
      {
        foreach (DirectoryInfo directory in dirInfo.GetDirectories())
        {
            list.AddRange(GetSupportedFiles(directory, true));
        }
      }
      catch (Exception ex)
      {
        return (IEnumerable<FileInfo>) list;
      }
      return (IEnumerable<FileInfo>) list;
    }

    public Task SaveAsync(List<AnalogyLogMessage> messages, string fileName) => throw new NotImplementedException();

    public bool CanOpenFile(string fileName) => fileName.EndsWith("log");

    public bool CanOpenAllFiles(IEnumerable<string> fileNames) => fileNames.All<string>(new Func<string, bool>(CanOpenFile));

    public Image LargeImage { get; set; }
  }
}
