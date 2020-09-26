namespace Analogy.LogViewer.RabbitMq
{
  public enum LevelEnum
  {
    None = 0,
    Critical = 4,
    Error = 8,
    Warning = 16, // 0x00000010
    Info = 64, // 0x00000040
    Debug = 128, // 0x00000080
  }
}
