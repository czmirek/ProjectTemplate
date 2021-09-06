using System;

namespace ProjectTemplate.Api
{
    public static class Env
    {
        public static string COSMOSDB_ENDPOINT { get; } = Environment.GetEnvironmentVariable(nameof(COSMOSDB_ENDPOINT));
        public static string COSMOSDB_AUTHENTICATION_KEY { get; } = Environment.GetEnvironmentVariable(nameof(COSMOSDB_AUTHENTICATION_KEY));
        public static string COSMOSDB_DATABASE { get; } = Environment.GetEnvironmentVariable(nameof(COSMOSDB_DATABASE));
    }
}
