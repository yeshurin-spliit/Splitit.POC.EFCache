namespace EFCache.POC.Configurations
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string ApplicationDbSchema { get; set; }
        public string ApplicationDbConnectionString { get; set; }
        public short? MaxRetryCount { get; set; }
        public int? TimeoutInSeconds { get; set; }
        public int? ConnectionPoolSize { get; set; }
    }
}