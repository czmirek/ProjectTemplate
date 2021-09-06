using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Lib
{
    public class ObjectLockStrategy<THandler, TRequest> : ObjectLockStrategy
        where THandler : ObjectLockHandler<TRequest>
    {
        public async Task Invoke(Guid id, TRequest request)
        {
            using var scope = ServiceProvider.CreateScope();

            var lockHandler = (ObjectLockHandler<TRequest>)scope.ServiceProvider.GetService(typeof(THandler));
            var dLock = scope.ServiceProvider.GetRequiredService<IDistributedObjectLock>();
            
            lockHandler.Id = id;
            string lockKey = $"{lockHandler.LockCategory}_{lockHandler.Id}";
            using (dLock.AcquireAsync(lockKey))
            {
                await lockHandler.Handle(request);
            }
        }
    }
}