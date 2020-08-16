using System.IO;
using Task1Backup.Exception;
using Task1Backup.Status;

namespace Task1Backup.Command
{
    public class DirectoryCopy : ICommand
    {
        private readonly string from;
        private readonly string to;

        public DirectoryCopy(string from, string to)
        {
            this.from = from;
            this.to = to;
        }

        public override Status.Status Execute()
        {
            try
            {
                var directoryFrom = new DirectoryInfo(from);
                var directoryTo = new DirectoryInfo(to);

                if (!Directory.Exists(directoryTo.FullName))
                {
                    Directory.CreateDirectory(directoryTo.FullName);
                }

                foreach (var fileInfo in directoryFrom.GetFiles())
                {
                    CommandInvoker.Add(new FileCopy(fileInfo.ToString(),
                        Path.Combine(directoryTo.ToString(), fileInfo.Name)));
                }

                foreach (var subDir in directoryFrom.GetDirectories())
                {
                    var nextSubDir = directoryTo.CreateSubdirectory(subDir.Name);
                    CommandInvoker.Add(new DirectoryCopy(subDir.ToString(), nextSubDir.ToString()));
                }
            }
            catch (System.Exception e)
            {
                throw new CopyDirectoryException(e.Message);
            }

            return new Ok($"Directory copied {from}");
        }
    }
}