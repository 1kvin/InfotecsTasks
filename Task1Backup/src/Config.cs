using System.Collections.Generic;

namespace Task1Backup
{
    public class Config
    {
        public List<string> HomeDirectory { get; set; }
        public string BackupDirectory { get; set; }
        public string LogFileDirectory { get; set; }
        public int LogLevel { get; set; }
    }
}