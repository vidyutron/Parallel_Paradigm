using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Parallel_Collections
{
    public class Producer_Consumer
    {
        const int _blockingCount = 10;
        //

        BlockingCollection<int> _messages =
            new BlockingCollection<int>(new ConcurrentBag<int>(), _blockingCount);
        Random _random = new Random();
        CancellationTokenSource _cts = new CancellationTokenSource();
        public Producer_Consumer()
        {
            Moderator();
        }

        private void Moderator()
        {
            BlockingCollections();
        }

        private void BlockingCollections()
        {
            Task.Factory.StartNew(ProduceAndConsume, _cts.Token);
            Console.ReadKey();
            _cts.Cancel();
            
        }

        private void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] { producer, consumer }, _cts.Token);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
            catch(OperationCanceledException oce)
            {
               
            }
        }

        private void RunProducer()
        {
            while (true)
            {
                _cts.Token.ThrowIfCancellationRequested();
                int i = _random.Next(100);
                _messages.Add(i);
                Console.WriteLine($" + {i}\t");
                Thread.Sleep(_random.Next(1000));
            }
        }

        private void RunConsumer()
        {
            foreach (var item in _messages.GetConsumingEnumerable())
            {
                _cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($" - {item}\t");
                Thread.Sleep(_random.Next(100));
            }
        }
    }
}
