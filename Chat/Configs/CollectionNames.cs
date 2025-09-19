namespace Chat.Configs
{
    public class MongoDbSettings
    {
        public string DatabaseName { get; set; }
        public Dictionary<string, string> CollectionNames { get; set; } = new();
    }
}
