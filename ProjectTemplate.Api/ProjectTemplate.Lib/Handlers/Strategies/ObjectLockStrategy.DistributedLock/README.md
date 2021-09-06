# Distributed object lock

Distributed object lock is related to the ObjectLockStrategy and it shouldn't be used elsewhere.

The ObjectLockStrategy uses this lock to enforce pessimistic concurrency in handlers implementing `ObjectLockHandler`.
These handlers will throw exception on a pessimistic lock hit.