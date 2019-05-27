using NBench;

namespace TaskManagerApi.Nbench.Test
{
    public class NbenchTest
    {
        private readonly string CounterName = "Counter";
        private Counter counter;

        [PerfSetup]
        public void SetUp(BenchmarkContext context)
        {
            counter = context.GetCounter(CounterName);
        }

        [PerfBenchmark(NumberOfIterations = 2, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement("Counter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void BenchmarkMethod(BenchmarkContext context)
        {
            var bytes = new byte[1024];
            counter.Increment();
        }
    }
}
