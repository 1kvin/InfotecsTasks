using System;

namespace Task1Backup.Log
{
    public class ConsoleLog : ILog
    {
        private int level;

        public void Start(string filePath, int level)
        {
            this.level = level;
            Write($"Time: {DateTime.Now}, Log level: {level}");
        }

        public void Stop() { }
        
        public void Write(Status.Status status)
        {
            if (level < status.level)
                return;
            Write($"{status.GetType().Name}: {status.message}");
        }

        private void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}