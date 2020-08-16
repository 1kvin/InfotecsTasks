using Task1Backup.Log;

namespace Task1Backup
{
    class Program
    {
        static void Main(string[] args)
        {
            //var log = new ConsoleLog();
            var log = new FileLog();
            Backup.Start(log);
        }
    }
}
