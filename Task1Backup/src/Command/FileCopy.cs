using System.IO;
using Task1Backup.Exception;
using Task1Backup.Status;

namespace Task1Backup.Command
{
    public class FileCopy : ICommand
    {
        private readonly string from;
        private readonly string to;

        public FileCopy(string from, string to)
        {
            this.from = from;
            this.to = to;
        }

        public override Status.Status Execute()
        {
            Status.Status returnStatus;
            try
            {
                var fileInfo = new FileInfo(from);
                try
                {
                    fileInfo.CopyTo(to, false);
                    returnStatus = new Ok($"File copied successfully {from}");
                }
                catch (IOException e)
                {
                    fileInfo.CopyTo(to, true);
                    returnStatus = new Warning($"File overwritten {@from}");
                }
            }
            catch (System.Exception e)
            {
                throw new CopyFileException(e.Message);
            }

            return returnStatus;
        }
    }
}