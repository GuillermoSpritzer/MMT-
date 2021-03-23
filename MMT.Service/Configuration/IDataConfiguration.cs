namespace MMT.Service.Configuration
{
    public interface IDataConfiguration
    {
        public string connectionString { get; }
        public string apiKey { get; }

    }
}