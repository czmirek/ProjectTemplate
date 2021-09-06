using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Timers;

namespace ProjectTemplate.Lib
{
    public class InMemoryDistributedObjectLock : IDistributedObjectLock
    {
        private static readonly HashSet<string> locks = new();
        private Timer timeoutTimer;
        private string key;

        public Task<IDisposable> AcquireAsync(string key)
        {
            if (locks.Contains(key))
                throw new DistributedObjecktLockAcquiredException();

            locks.Add(key);
            timeoutTimer = new Timer(5000);
            timeoutTimer.Elapsed += LockTimeoutElapsed;
            timeoutTimer.Start();

            return Task.FromResult<IDisposable>(new InMemoryDistributedObjectLockAcquisition(this, key));
        }

        private void LockTimeoutElapsed(object sender, ElapsedEventArgs e)
        {
            timeoutTimer.Stop();
            Unset();
        }

        private void Unset()
        {
            locks.Remove(key);
        }

        public class InMemoryDistributedObjectLockAcquisition : IDisposable
        {
            private readonly InMemoryDistributedObjectLock dl;
            private readonly string key;

            public InMemoryDistributedObjectLockAcquisition(InMemoryDistributedObjectLock dl, string key)
            {
                this.dl = dl;
                this.key = key;
            }

            public void Dispose() => dl.Unset(key);
        }
    }
}
