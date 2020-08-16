namespace Task1Backup.Log
{
    public interface ILog
    {
        public void Start(string filePath, int level);
        public void Stop();
        public void Write(Status.Status status);
    }
}