using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Task1Backup.Command;
using Task1Backup.Command.ConfigReader;
using Task1Backup.Log;

namespace Task1Backup
{
    public static class Backup
    {
        private static ILog logger = null;

        public static void Start(ILog logger = null)
        {
            Backup.logger = logger;
            CommandInvoker.SetLogger(logger);
            var configReader = new JsonConfigReader("Config.json");

            CommandInvoker.Add(configReader);
            var config = configReader.GetResult();
            logger?.Start(config.LogFileDirectory, config.LogLevel);

            foreach (var directoryCopy in config.HomeDirectory.Select(homeDirectory =>
                new DirectoryCopy(homeDirectory,
                    Path.Combine(config.BackupDirectory, new DirectoryInfo(homeDirectory).Name))))
            {
                CommandInvoker.Add(directoryCopy);
            }

            logger?.Stop();
        }

        public static void Stop()
        {
            logger?.Stop();
            Environment.Exit(1);
        }
    }
}