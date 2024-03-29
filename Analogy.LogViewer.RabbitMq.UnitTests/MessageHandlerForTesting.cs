﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;

namespace Analogy.UnitTests
{
    class MessageHandlerForTesting:ILogMessageCreatedHandler
    {
        private List<IAnalogyLogMessage> messages;
        public MessageHandlerForTesting()
        {
            messages=new List<IAnalogyLogMessage>();
        }
        public void AppendMessage(IAnalogyLogMessage message, string dataSource)
        {
            messages.Add(message);
        }

        public void AppendMessages(List<IAnalogyLogMessage> messages, string dataSource)
        {
            this.messages.AddRange(messages);
        }

        public void ReportFileReadProgress(AnalogyFileReadProgress progress)
        {
            
        }

        public bool ForceNoFileCaching { get; set; }
        public bool DoNotAddToRecentHistory { get; set; }
    }
}
