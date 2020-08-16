namespace Task1Backup.Status
{
    public abstract class Status
    {
        internal Status(string message, int level)
        {
            this.message = message;
            this.level = level;
        }

        public string GetMessage()
        {
            return message;
        }

        public readonly string message;
        public readonly int level;
    }
}