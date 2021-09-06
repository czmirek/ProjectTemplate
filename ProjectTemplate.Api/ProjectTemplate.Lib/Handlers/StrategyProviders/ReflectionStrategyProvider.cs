using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTemplate.Lib
{
    public class ReflectionStrategyProvider : IHandlerStrategyProvider
    {
        private readonly Dictionary<string, NormalInvokeStrategy> normalInvoke = new Dictionary<string, NormalInvokeStrategy>();
        private readonly Dictionary<string, ObjectLockStrategy> lockInvoke = new Dictionary<string, ObjectLockStrategy>();

        public ReflectionStrategyProvider(IServiceProvider sp) 
        {
            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                                    .SelectMany(x => x.DefinedTypes)
                                    .Where(type => type.Assembly.GetName().Name.StartsWith("Bytka"))
                                    .Where(type => type.Name.EndsWith("Handler"))
                                    .Where(type => !type.IsAbstract);

            foreach (var handlerType in handlerTypes)
            {
                Type strategyType = null;
                var args = Enumerable.Concat(new Type[] { handlerType }, handlerType.BaseType.GenericTypeArguments).ToArray();
                
                if (typeof(BaseHandler).IsAssignableFrom(handlerType))
                {
                    if (handlerType.BaseType.GenericTypeArguments.Length == 1)
                        strategyType = typeof(NormalInvokeStrategy<,>);
                    else if (handlerType.BaseType.GenericTypeArguments.Length == 2)
                        strategyType = typeof(NormalInvokeStrategy<,,>);

                    Type fullStrategyType = strategyType.MakeGenericType(args);
                    NormalInvokeStrategy strategy = Activator.CreateInstance(fullStrategyType) as NormalInvokeStrategy;
                    strategy.ServiceProvider = sp;
                    normalInvoke.Add(handlerType.Name, strategy);

                }
                else if (typeof(ObjectLockHandler).IsAssignableFrom(handlerType))
                {
                    if (handlerType.BaseType.GenericTypeArguments.Length == 1)
                        strategyType = typeof(ObjectLockStrategy<,>);
                    else if (handlerType.BaseType.GenericTypeArguments.Length == 2)
                        strategyType = typeof(ObjectLockStrategy<,,>);

                    Type fullStrategyType = strategyType.MakeGenericType(args);
                    ObjectLockStrategy strategy = Activator.CreateInstance(fullStrategyType) as ObjectLockStrategy;
                    strategy.ServiceProvider = sp;
                    lockInvoke.Add(handlerType.Name, strategy);
                }
            }
        }

        public NormalInvokeStrategy<THandler, TRequest, TResponse> GetNormalInvokeStrategy<THandler, TRequest, TResponse>() where THandler : BaseHandler<TRequest, TResponse>
        {
            return (NormalInvokeStrategy<THandler, TRequest, TResponse>)normalInvoke[typeof(THandler).Name];
        }

        public NormalInvokeStrategy<THandler, TRequest> GetNormalInvokeStrategy<THandler, TRequest>() where THandler : BaseHandler<TRequest>
        {
            return (NormalInvokeStrategy<THandler, TRequest>)normalInvoke[typeof(THandler).Name];
        }

        public ObjectLockStrategy<THandler, TRequest, TResponse> GetObjectLockStrategy<THandler, TRequest, TResponse>() where THandler : ObjectLockHandler<TRequest, TResponse>
        {
            return (ObjectLockStrategy<THandler, TRequest, TResponse>)lockInvoke[typeof(THandler).Name];
        }

        public ObjectLockStrategy<THandler, TRequest> GetObjectLockStrategy<THandler, TRequest>() where THandler : ObjectLockHandler<TRequest>
        {
            return (ObjectLockStrategy<THandler, TRequest>)lockInvoke[typeof(THandler).Name];
        }
    }
}