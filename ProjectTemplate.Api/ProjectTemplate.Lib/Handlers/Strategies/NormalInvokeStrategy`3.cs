using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Lib
{
    public class NormalInvokeStrategy<THandler, TRequest, TResponse> : NormalInvokeStrategy
        where THandler : BaseHandler<TRequest, TResponse>
    {
        public async Task<TResponse> Invoke(TRequest request)
        {
            using var scope = ServiceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService(typeof(THandler)) as BaseHandler<TRequest, TResponse>;
            return await handler.Handle(request);
        }
    }
}