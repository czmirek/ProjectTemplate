using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTemplate.Lib
{
    public static class CosmosExtensions
    {
        public static async IAsyncEnumerable<T> CosmosRead<T>(this Container c, Func<IQueryable<T>, IQueryable<T>> filter)
        {
            var queryable = c.GetItemLinqQueryable<T>();
            using FeedIterator<T> setIterator = filter(queryable).ToFeedIterator();

            while (setIterator.HasMoreResults)
            {
                foreach (var item in await setIterator.ReadNextAsync())
                {
                    yield return item;
                }
            }
        }
    }
}
