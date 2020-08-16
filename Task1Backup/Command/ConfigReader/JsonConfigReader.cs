using System.IO;
using System.Text.Json;
using Task1Backup.Exception;
using Task1Backup.Status;

namespace Task1Backup.Command.ConfigReader
{
    public class JsonConfigReader : IConfigReader
    {
        private readonly string fileName;
        private Config config;

        public JsonConfigReader(string fileName)
        {
            this.fileName = fileName;
        }

        public override Status.Status Execute()
        {
            try
            {
                var jsonString = File.ReadAllText(fileName);
                config = JsonSerializer.Deserialize<Config>(jsonString);
            }
            catch (System.Exception e)
            {
                throw new ReadConfigException(e.Message);
            }

            return new Ok();
        }

        public override Config GetResult()
        {
            return config;
        }
    }
}