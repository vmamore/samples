using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace semaphoresample
{
    class Program
    {
        const int BUFFER_MAX = 2;
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(BUFFER_MAX);

        static Queue<string> producer = new Queue<string>();

        static void Main(string[] args)
        {
            while (true)
            {
                var t1 = new Thread(Add);
                t1.Start();
                var t2 = new Thread(Read);
                t2.Start();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Add()
        {
            int value = 0;
            while (true)
            {
                if (producer.Count == BUFFER_MAX)
                    Monitor.Wait(producer);

                System.Console.WriteLine($"Produced produced: {value}");

                producer.Enqueue($"item - {value++}");
                Monitor.PulseAll(producer);

                Thread.Sleep(1000);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Read()
        {
            while (true)
            {
                while (producer.Count == 0)
                    Monitor.Wait(producer);

                var item = producer.Dequeue();
                System.Console.WriteLine($"Consumer consumed: {item}");
                Monitor.PulseAll(producer);

                Thread.Sleep(1000);
            }
        }
    }
}
