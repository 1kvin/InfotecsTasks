using System.Collections.Generic;
using Task1Backup.Command;
using Task1Backup.Log;
using Task1Backup.Status;

namespace Task1Backup
{
    public static class CommandInvoker
    {
        private static readonly Queue<ICommand> commands = new Queue<ICommand>();
        private static ILog logger;
        public static void Init(ILog logger = null)
        {
            CommandInvoker.logger = logger;
            commands.Clear();
        }
        
        public static void Add(ICommand command)
        {
            commands.Enqueue(command);
            //TODO Add sequencer
            Invoke();
        }

        private static void Invoke()
        {
            while (commands.Count != 0)
            {
               ExecuteCommand(commands.Dequeue());
            }
        }

        private static void ExecuteCommand(ICommand command)
        {
            Status.Status status = null;
            try
            {
                status = command.Execute();
            }
            catch (System.Exception e)
            { 
                status = new Error(e.Message);
                Backup.Stop();
            }
            finally
            {
                command.isDone = true;
                logger?.Write(status);
            }
        }
    }
}