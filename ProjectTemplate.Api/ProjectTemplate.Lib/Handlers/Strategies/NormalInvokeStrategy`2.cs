using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Lib
{
    public class NormalInvokeStrategy<THandler, TRequest> : NormalInvokeStrategy
        where THandler : BaseHandler<TRequest>
    {
        public async Task Invoke(TRequest request)
        {
            using var scope = ServiceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService(typeof(THandler)) as BaseHandler<TRequest>;
            await handler.Handle(request);
        }
    }
}