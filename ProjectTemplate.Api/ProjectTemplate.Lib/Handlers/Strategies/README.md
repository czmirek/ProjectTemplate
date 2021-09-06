# Handler strategies

All domain actions (create user, update user, create order, etc.) are constructed inside **handlers**.

Each handler implements a specific handler type which in turn decides an **invocation strategy** 
which is basically a strategy that decides how that handler is going to be run.

## List of handlers and their strategies


### `BaseHandler` and `NormalInvokeStrategy`
Handler implementing `BaseHandler` does not do anything special. The handler's method is invoked 
in the same thread. The NormalInvokeStrategy just forwards the call to the handler without doing
anything else.

### `ObjectLockHandler` and `ObjectLockStrategy`
Handler implementing `ObjectLockHandler` is a pessimistic concurrency implementation 
locking on a whole object, mimicing an actor.

On lock conflict, exception is thrown.