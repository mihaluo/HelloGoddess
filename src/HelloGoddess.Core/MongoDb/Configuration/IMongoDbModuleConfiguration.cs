namespace HelloGoddess.Core.MongoDb.Configuration
{
    public interface IMongoDbModuleConfiguration
    {
        string ConnectionString { get; set; }

        string DatatabaseName { get; set; }
    }
}
