namespace Task1Backup.Status
{
    public class Ok : Status
    {
        public Ok() : base(string.Empty, 2) { }

        public Ok(string message) : base(message, 2) { }
    }
}