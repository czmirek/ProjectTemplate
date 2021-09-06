using Microsoft.Azure.Cosmos;

namespace ProjectTemplate.Lib
{
    public interface ICosmosClientProvider
    {
        Container GetContainer(string containerId);
        CosmosClient GetClient();
    }
}