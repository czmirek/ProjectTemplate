namespace ProjectTemplate.Lib
{
    public interface IHandlerStrategyProvider
    {
        NormalInvokeStrategy<THandler, TRequest, TResponse> GetNormalInvokeStrategy<THandler, TRequest, TResponse>()
            where THandler : BaseHandler<TRequest, TResponse>;

        NormalInvokeStrategy<THandler, TRequest> GetNormalInvokeStrategy<THandler, TRequest>()
            where THandler : BaseHandler<TRequest>;

        ObjectLockStrategy<THandler, TRequest, TResponse> GetObjectLockStrategy<THandler, TRequest, TResponse>()
            where THandler : ObjectLockHandler<TRequest, TResponse>;

        ObjectLockStrategy<THandler, TRequest> GetObjectLockStrategy<THandler, TRequest>()
            where THandler : ObjectLockHandler<TRequest>;
    }
}
