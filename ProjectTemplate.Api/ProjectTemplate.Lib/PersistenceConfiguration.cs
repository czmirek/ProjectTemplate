namespace ProjectTemplate.Lib
{
    public class PersistenceConfiguration
    {
        public string CosmosDbConnectionString => $"AccountEndpoint={CosmosDbEndpoint};AccountKey={CosmosDbKey}";
        public PersistenceType PersistenceType { get; init; }
        public string CosmosDbEndpoint { get; init; }
        public string CosmosDbKey { get; init; }
        public string DatabaseName { get; init; }
    }
}
