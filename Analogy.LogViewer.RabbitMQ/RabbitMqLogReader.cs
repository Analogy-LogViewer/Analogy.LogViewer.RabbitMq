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

        public async Task<IEnumerable<IAnalogyLogMessage>> Process(
          string fileName,
          CancellationToken token,
          ILogMessageCreatedHandler messagesHandler)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                AnalogyLogMessage analogyLogMessage = new AnalogyLogMessage("File is null or empty. Aborting.", AnalogyLogLevel.Information, (AnalogyLogClass)0, source, "", "None"); 
                analogyLogMessage.Source = source;
                analogyLogMessage.Module = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                AnalogyLogMessage empty = analogyLogMessage;
                messagesHandler.AppendMessage(empty, GetFileNameAsDataSource(fileName));
                List<AnalogyLogMessage> analogyLogMessageList = new List<AnalogyLogMessage> { empty };
                return analogyLogMessageList;
            }
            Task<IEnumerable<IAnalogyLogMessage>> task = new Task<IEnumerable<IAnalogyLogMessage>>((Func<IEnumerable<IAnalogyLogMessage>>)(() =>
           {
               try
               {
                   List<IAnalogyLogMessage> list = Parser
                       .ParseLog(File.ReadAllLines(fileName), CultureInfo.CurrentCulture).ToList()
                       .Select((Func<Message, IAnalogyLogMessage>)(line => line.ToAnalogyLogMessage())).ToList();
                   messagesHandler.AppendMessages(list, fileName);
                   return (IEnumerable<IAnalogyLogMessage>)list;
               }
               catch (Exception ex)
               {
                   Console.WriteLine((object)ex);
                   throw;
               }
           }));
            task.Start();
            IEnumerable<IAnalogyLogMessage> analogyLogMessages = await task;
            return analogyLogMessages;
        }

        public static string GetFileNameAsDataSource(string fileName)
        {
            string fileName1 = Path.GetFileName(fileName);
            return fileName.Equals(fileName1) ? fileName : fileName1 + " (" + fileName + ")";
        }
    }
}
