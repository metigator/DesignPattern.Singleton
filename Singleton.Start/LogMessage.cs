using System;

namespace Singleton.Start
{ 
    public class LogMessage
    {
        public string Message { get; set; }
        public LogType LogType { get; set; } 
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            var timestamp = CreatedAt.ToString("yyyy-MM-dd hh:mm");
            
            return $"{LogType.ToString().PadLeft(7, ' ')}  [{timestamp}] {Message}";
        }
    }
}
