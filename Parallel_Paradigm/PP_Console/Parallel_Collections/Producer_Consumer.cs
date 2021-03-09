using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Parallel_Collections
{
    /// <summary>
    /// 
    /// </summary>
    public class Producer_Consumer
    {
        private const int _blockingCount            = 10;
        /// <summary>
        /// Blocking Collection 
        /// </summary>
        public BlockingCollection<int> _messages    = new BlockingCollection<int>(new ConcurrentBag<int>(), _blockingCount);
        private Random _random                      = new Random();
        private CancellationTokenSource _cts        = new CancellationTokenSource();

        /// <summary>
        /// Constructor
        /// </summary>
        public Producer_Consumer()
        {
            Moderator();
        }

        /// <summary>
        /// Producer Consumer Controller Method
        /// </summary>
        public void Moderator()
        {
            BlockingCollections();
        }

        /// <summary>
        /// 
        /// </summary>
        public void BlockingCollections()
        {
            Task.Factory.StartNew(ProduceAndConsume, _cts.Token);
            Console.ReadKey();
            _cts.Cancel();
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void ProduceAndConsume()
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

        /// <summary>
        /// Demonstrate Producer of theaded messages
        /// </summary>
        public void RunProducer()
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

        /// <summary>
        /// Demonstrate Consumer of theaded messages
        /// </summary>
        public void RunConsumer()
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
