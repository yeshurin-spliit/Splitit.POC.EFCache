namespace EFCache.POC.Configurations
{
    public interface IDatabaseConfiguration
    {
        string ApplicationDbSchema { get; set; }
        string ApplicationDbConnectionString { get; set; }
        short? MaxRetryCount { get; set; }
        int? TimeoutInSeconds { get; set; }
        int? ConnectionPoolSize { get; set; }
    }
}
