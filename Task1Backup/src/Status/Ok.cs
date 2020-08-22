namespace Task1Backup.Status
{
    public class Ok : Status
    {
        public Ok() : base(string.Empty, (int)StatusLevel.Message) { }

        public Ok(string message) : base(message, (int)StatusLevel.Message) { }
    }
}