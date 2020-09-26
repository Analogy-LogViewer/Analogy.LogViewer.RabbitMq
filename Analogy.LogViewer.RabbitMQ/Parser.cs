using System;
using System.Collections.Generic;
using System.Globalization;

namespace Analogy.LogViewer.RabbitMq
{
  public static class Parser
  {
    public static Message ParseLine(string logLine, CultureInfo culture)
    {
      Message message = new Message();
      message.Category = CategoryEnum.Default;
      string[] strArray = logLine.Split(new char[1]{ ' ' }, 5);
      LevelEnum category;
      if (GetLevel(strArray[2], out category))
      {
        try
        {
          message.TimeStamp = GetDateTimeFromStrings(strArray[0], strArray[1], CultureInfo.InvariantCulture);
          message.Level = category;
          message.ConnectionId = strArray[3];
          message.Body = strArray[4].TrimStart('\r', '\n', ' ');
        }
        catch (Exception ex)
        {
          Logger.LogError("Error parsing message", ex);
          return new Message() { Body = string.Empty };
        }
      }
      return message;
    }

    public static IEnumerable<Message> ParseLog(
      string[] logArray,
      CultureInfo culture)
    {
      try
      {
        List<Message> messageList = new List<Message>();
        for (int index = 0; index < logArray.Length; ++index)
        {
          string logLine = logArray[index];
          if (index + 1 < logArray.Length)
          {
            for (string log = logArray[index + 1]; !IsNewLogLine(log); log = logArray[++index + 1])
            {
              logLine = logLine + Environment.NewLine + log;
              if (index >= logArray.Length)
                break;
            }
          }
          Message line = ParseLine(logLine, CultureInfo.InvariantCulture);
          messageList.Add(line);
        }
        return (IEnumerable<Message>) messageList;
      }
      catch (Exception ex)
      {
        Logger.LogError("Error parsing log", ex);
        return (IEnumerable<Message>) null;
      }
    }

    public static bool IsNewLogLine(string logLine)
    {
      try
      {
        return GetLevel(logLine.Split(new char[1]
        {
          ' '
        }, 4)[2], out LevelEnum _);
      }
      catch
      {
        return false;
      }
    }

    public static bool GetLevel(string input, out LevelEnum category)
    {
      category = LevelEnum.None;
      return Enum.TryParse<LevelEnum>(input.Substring(1, input.Length - 2), true, out category);
    }

    public static DateTime GetDateTimeFromStrings(
      string date,
      string time,
      CultureInfo culture)
    {
      return DateTime.Parse(date) + TimeSpan.Parse(time);
    }
  }
}
