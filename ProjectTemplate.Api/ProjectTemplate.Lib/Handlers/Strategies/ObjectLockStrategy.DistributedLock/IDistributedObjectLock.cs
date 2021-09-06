using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Lib
{
    public interface IDistributedObjectLock
    {
        Task<IDisposable> AcquireAsync(string key);
    }
}