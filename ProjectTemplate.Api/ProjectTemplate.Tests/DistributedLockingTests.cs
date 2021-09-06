using ProjectTemplate.Lib;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectTemplate.Tests
{
    public class DistributedLockingTests
    {
        private readonly IHandlerStrategyProvider hsp;

        public DistributedLockingTests(IHandlerStrategyProvider hsp)
        {
            this.hsp = hsp ?? throw new ArgumentNullException(nameof(hsp));
        }

        /*
                Co je třeba otestovat:

                - konflikt 2 requestů nad stejným handlerem
                - konflikt 2 requestů nad 2 handlery se stejným lockem
                - sériově zavolané requesty nad stejným handlerem nevyvolají konflikt
                - sériově zavolané requesty nad 2 handlery s různým lockem nevyvolají konflikt
                - paralelně zavolané requesty nad 2 handlery s různým lockem nevyvolají konflikt
                
         */

        [Fact]
        public void LockThrowsException()
        {
            Guid id = Guid.NewGuid();
            var t1 = Task.Run(async () =>
            {
                var test = hsp.GetObjectLockStrategy<TestLockHandler, TestModel>();
                await test.Invoke(id, new TestModel());
            });

            var delay = Task.Delay(TimeSpan.FromSeconds(1));
            delay.Wait();

            Assert.ThrowsAsync<DistributedObjecktLockAcquiredException>(async () =>
            {
                var test = hsp.GetObjectLockStrategy<TestLockHandler, TestModel>();
                await test.Invoke(id, new TestModel());
            });
        }
    }
}
