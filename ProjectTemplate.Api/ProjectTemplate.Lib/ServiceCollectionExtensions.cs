using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ProjectTemplate.Lib
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectTemplateHandlers(this IServiceCollection sc)
        {
            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(x => x.DefinedTypes)
                                        .Where(type => type.Assembly.GetName().Name.StartsWith("Bytka"))
                                        .Where(type => type.Name.EndsWith("Handler"))
                                        .Where(type => !type.IsAbstract);

            foreach (var handlerType in handlerTypes)
            {
                sc.AddScoped(handlerType);
            }
            return sc;
        }
    }
}
