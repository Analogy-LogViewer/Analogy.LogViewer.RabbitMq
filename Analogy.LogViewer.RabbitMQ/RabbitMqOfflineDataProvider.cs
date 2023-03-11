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
    public class RabbitMqOfflineDataProvider : Template.OfflineDataProvider
    {
        private static string _title = "RabbitMQ";

        public override Task InitializeDataProvider(IAnalogyLogger logger)
        {
            LogReader = new RabbitMqLogReader();
            return base.InitializeDataProvider(logger);
        }
        public override Image SmallImage { get; set; }

        public override bool DisableFilePoolingOption { get; set; } = false;

        public override bool CanSaveToLogFile { get; set; } = false;

        public override string FileOpenDialogFilters { get; set; } = "RabbitMQ Log file |*.log";

        public override string FileSaveDialogFilters { get; set; } = string.Empty;

        public override IEnumerable<string> SupportFormats { get; set; } = (IEnumerable<string>)new string[1]
        {
            "*.log"
        };

        public override string InitialFolderFullPath => Environment.CurrentDirectory;

        public override (Color backgroundColor, Color foregroundColor) GetColorForMessage(
          IAnalogyLogMessage logMessage)
        {
            return (Color.Empty, Color.Empty);
        }

        public override IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders() => (IEnumerable<(string, string)>)Array.Empty<(string, string)>();

        public override Guid Id { get; set; } = RabbitMqFactory.Id;

        public override string OptionalTitle
        {
            get => _title;
            set => _title = value;
        }

        public override bool UseCustomColors { get; set; } = false;

        public RabbitMqLogReader LogReader { get; set; }

        public override async Task<IEnumerable<IAnalogyLogMessage>> Process(
          string fileName,
          CancellationToken token,
          ILogMessageCreatedHandler messagesHandler)
        {
            if (!CanOpenFile(fileName))
            {
                return new List<AnalogyLogMessage>(0);
            }

            IEnumerable<IAnalogyLogMessage> analogyLogMessages = await LogReader.Process(fileName, token, messagesHandler);
            return analogyLogMessages;
        }

        protected override List<FileInfo> GetSupportedFilesInternal(DirectoryInfo dirInfo, bool recursive)
        {

            List<FileInfo> list = ((IEnumerable<FileInfo>)dirInfo.GetFiles("*.log")).ToList<FileInfo>();
            if (!recursive)
            {
                return list;
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
                return list;
            }
            return list;
        }


        public override bool CanOpenFile(string fileName) => fileName.EndsWith("log");

        public override bool CanOpenAllFiles(IEnumerable<string> fileNames) => fileNames.All<string>(new Func<string, bool>(CanOpenFile));

        public override Image LargeImage { get; set; }
    }
}
