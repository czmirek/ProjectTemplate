using System.Threading.Tasks;

namespace ProjectTemplate.Lib
{
    public abstract class BaseHandler<TRequest, TResponse> : BaseHandler
    {
        public abstract Task<TResponse> Handle(TRequest request);
    }

    public abstract class BaseHandler<TRequest> : BaseHandler
    {
        
        public abstract Task Handle(TRequest request);
    }

    public abstract class BaseHandler
    {
        
    }
}
