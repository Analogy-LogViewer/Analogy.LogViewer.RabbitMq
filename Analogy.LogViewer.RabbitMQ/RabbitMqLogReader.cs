using Analogy.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.RabbitMq
{
    public class RabbitMqLogReader
    {
        private string source = "";

        public async Task<IEnumerable<AnalogyLogMessage>> Process(
          string fileName,
          CancellationToken token,
          ILogMessageCreatedHandler messagesHandler)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                AnalogyLogMessage analogyLogMessage = new AnalogyLogMessage("File is null or empty. Aborting.", (AnalogyLogLevel)7, (AnalogyLogClass)0, source, "None", (string)null, (string)null, 0, 0, (Dictionary<string, string>)null, (string)null, nameof(Process), "E:\\workspaces\\danw25\\rabbit\\Analogy.LogViewer.RabbitMq\\RabbitMqLogReader.cs", 22);
                analogyLogMessage.Source = source;
                analogyLogMessage.Module = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                AnalogyLogMessage empty = analogyLogMessage;
                messagesHandler.AppendMessage(empty, GetFileNameAsDataSource(fileName));
                List<AnalogyLogMessage> analogyLogMessageList = new List<AnalogyLogMessage>();
                analogyLogMessageList.Add(empty);
                return (IEnumerable<AnalogyLogMessage>)analogyLogMessageList;
            }
            Task<IEnumerable<AnalogyLogMessage>> task = new Task<IEnumerable<AnalogyLogMessage>>((Func<IEnumerable<AnalogyLogMessage>>)(() =>
           {
               try
               {
                   List<AnalogyLogMessage> list = Parser.ParseLog(File.ReadAllLines(fileName), CultureInfo.CurrentCulture).ToList<Message>().Select<Message, AnalogyLogMessage>((Func<Message, AnalogyLogMessage>)(line => line.ToAnalogyLogMessage())).ToList<AnalogyLogMessage>();
                   messagesHandler.AppendMessages(list, fileName);
                   return (IEnumerable<AnalogyLogMessage>)list;
               }
               catch (Exception ex)
               {
                   Console.WriteLine((object)ex);
                   throw;
               }
           }));
            task.Start();
            IEnumerable<AnalogyLogMessage> analogyLogMessages = await task;
            return analogyLogMessages;
        }

        public static string GetFileNameAsDataSource(string fileName)
        {
            string fileName1 = Path.GetFileName(fileName);
            return fileName.Equals(fileName1) ? fileName : fileName1 + " (" + fileName + ")";
        }
    }
}
