namespace Task1Backup.Command
{
    public abstract class ICommand
    {
        public abstract Status.Status Execute();
        public bool isDone = false;
    }
}