using Singleton.ThreadSafeUsingDoubkeCheckedLock;
using System;
using System.Collections.Generic;

namespace Singleton.ThreadSafeUsingLock
{
    public class MemoryLogger
    {
        private static MemoryLogger _instance = null; 
        private static readonly object _lock = new object();

        private int _InfoCount;
        private int _WarningCount;
        private int _ErrorCount;

        private MemoryLogger() { }


        // T1, T2
        public static MemoryLogger GetLogger
        {  
            get
            { 
                if (_instance == null)
                {
                    // T1, T2
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MemoryLogger();
                        }
                    }
                }
                return _instance;   
            }
        }

        private List<LogMessage> _logs = new List<LogMessage>();
        public IReadOnlyCollection<LogMessage> Logs => _logs;


        private void Log(string message, LogType logType)
        {

            _logs.Add(new LogMessage
            {
                Message = message,
                LogType = logType,
                CreatedAt = DateTime.Now
            });
        }

        public void LogInfo(string message)
        {
            ++_InfoCount;
            Log(message, LogType.INFO);
        }
        public void LogError(string message)
        {
            ++_ErrorCount;
            Log(message, LogType.ERROR);
        }
        public void LogWarning(string message)
        {
            ++_WarningCount;
            Log(message, LogType.WARNING);
        }

        public void ShowLog()
        {

            _logs.ForEach(x => Console.WriteLine(x));
            Console.WriteLine($"-------------------------------");

            Console.WriteLine($"Info ({_InfoCount}), Warning ({_WarningCount}), Error ({_ErrorCount})");
        }
    }
}
