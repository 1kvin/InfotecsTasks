namespace Task1Backup.Command.ConfigReader
{
    public abstract class IConfigReader : ICommand
    {
        public abstract Config GetResult();
    }
}