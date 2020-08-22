namespace Task1Backup.Status
{
    public abstract class Status
    {
        public enum StatusLevel
        {
            Error,
            Warning,
            Message
        }
        internal Status(string message, int level)
        {
            this.message = message;
            this.level = level;
        }
        
        public string message { get; private set; }
        public readonly int level;
    }
}