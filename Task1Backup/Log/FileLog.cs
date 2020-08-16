using System;
using System.IO;

namespace Task1Backup.Log
{
    public class FileLog : ILog
    {
        private string filePath;
        private int level;
        private StreamWriter streamWriter;
        private string fileName;

        public void Start(string filePath, int level)
        {
            fileName = $"{DateTime.Now.ToShortDateString()}.log";
            this.filePath = filePath;
            this.level = level;
            streamWriter = new StreamWriter(Path.Combine(filePath, fileName));
            streamWriter.WriteLine($"Time: {DateTime.Now}, Log level: {level}");
        }

        public void Stop()
        {
            streamWriter?.Close();
        }

        public void Write(Status.Status status)
        {
            if (level < status.level)
                return;
            Write($"{status.GetType().Name}: {status.message}");
        }

        private void Write(string message)
        {
            streamWriter.WriteLine(message);
        }
    }
}