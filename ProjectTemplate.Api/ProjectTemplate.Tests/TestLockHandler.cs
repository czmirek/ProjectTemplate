using ProjectTemplate.Lib;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Tests
{
    public class TestLockHandler : ObjectLockHandler<TestModel>
    {
        public override string LockCategory => "test";

        public override async Task Handle(TestModel request)
        {
            await Task.Delay(TimeSpan.FromSeconds(10000));
        }
    }
}
