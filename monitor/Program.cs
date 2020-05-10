using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace monitor
{
    class Program
    {
        private static string[] titles = {
      "Enqueue                       ",
      "TryEnqueue succeeded          ",
      "TryEnqueue failed             ",
      "TryEnqueue(T, wait) succeeded ",
      "TryEnqueue(T, wait) failed    ",
      "Dequeue attempts              ",
      "Dequeue exceptions            ",
      "Remove operations             ",
      "Queue elements removed        "};

        private enum ThreadResultIndex
        {
            EnqueueCt,
            TryEnqueueSucceedCt,
            TryEnqueueFailCt,
            TryEnqueueWaitSucceedCt,
            TryEnqueueWaitFailCt,
            DequeueCt,
            DequeueExCt,
            RemoveCt,
            RemovedCt
        };
        private static SafeQueue<int> intQueue = new SafeQueue<int>();
        private static int threadsRunning = 0;
        private static int[][] results = new int[3][];
        static void Main(string[] args)
        {
            Console.WriteLine("Working...");

            for(int i = 0; i < 3; i++){
                Thread t = new Thread(ThreadProc);
                t.Start(i);
                Interlocked.Increment(ref threadsRunning);
            }
        }

        private static void ThreadProc(object state)
        {
            DateTime finish = DateTime.Now.AddSeconds(10);
            Random rand = new Random();
            int[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int threadNum = (int)state;

            while (DateTime.Now < finish)
            {
                int what = rand.Next(250);
                int how = rand.Next(100);

                if (how < 16)
                {
                    intQueue.Enqueue(what);
                    result[(int)ThreadResultIndex.EnqueueCt] += 1;
                }
                else if (how < 32)
                {
                    if (intQueue.TryEnqueue(what))
                    {
                        result[(int)ThreadResultIndex.TryEnqueueSucceedCt] += 1;
                    }
                    else
                    {
                        result[(int)ThreadResultIndex.TryEnqueueFailCt] += 1;
                    }
                }
                else if (how < 48)
                {
                    if (intQueue.TryEnqueue(what, 10))
                    {
                        result[(int)ThreadResultIndex.TryEnqueueWaitSucceedCt] += 1;
                    }
                    else
                    {
                        result[(int)ThreadResultIndex.TryEnqueueWaitFailCt] += 1;
                    }
                }
                else if (how < 96)
                {
                    result[(int)ThreadResultIndex.DequeueCt] += 1;
                    try
                    {
                        intQueue.Dequeue();
                    }
                    catch
                    {
                        result[(int)ThreadResultIndex.DequeueExCt] += 1;
                    }
                }
                else
                {
                    result[(int)ThreadResultIndex.RemoveCt] += 1;
                    result[(int)ThreadResultIndex.RemoveCt] += intQueue.Remove(what);
                }
            }

            results[threadNum] = result;

            if (0 == Interlocked.Decrement(ref threadsRunning))
            {
                StringBuilder sb = new StringBuilder(
            "                               Thread 1 Thread 2 Thread 3    Total\n");

                for (int row = 0; row < 9; row++)
                {
                    int total = 0;
                    sb.Append(titles[row]);

                    for (int col = 0; col < 3; col++)
                    {
                        sb.Append(string.Format("{0,9}", results[col][row]));
                        total += results[col][row];
                    }

                    sb.AppendLine(string.Format("{0,9}", total));
                }

                System.Console.WriteLine(sb.ToString());
            }
        }
    }

    class SafeQueue<T>
    {
        private Queue<T> m_inputQueue = new Queue<T>();

        public void Enqueue(T qValue)
        {
            Monitor.Enter(m_inputQueue);

            try
            {
                m_inputQueue.Enqueue(qValue);
            }
            finally
            {
                Monitor.Exit(m_inputQueue);
            }
        }

        public bool TryEnqueue(T qValue)
        {
            if (Monitor.TryEnter(m_inputQueue))
            {
                try
                {
                    m_inputQueue.Enqueue(qValue);
                }
                finally
                {
                    Monitor.Exit(m_inputQueue);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryEnqueue(T qValue, int waitTime)
        {
            if (Monitor.TryEnter(m_inputQueue, waitTime))
            {
                try
                {
                    m_inputQueue.Enqueue(qValue);
                }
                finally
                {
                    Monitor.Exit(m_inputQueue);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public T Dequeue()
        {
            T retval;

            Monitor.Enter(m_inputQueue);
            try
            {
                retval = m_inputQueue.Dequeue();
            }
            finally
            {
                Monitor.Exit(m_inputQueue);
            }

            return retval;
        }

        public int Remove(T qValue)
        {
            int removedCt = 0;

            Monitor.Enter(m_inputQueue);

            try
            {
                int counter = m_inputQueue.Count;
                while (counter > 0)
                {
                    T elem = m_inputQueue.Dequeue();
                    if (!elem.Equals(qValue))
                    {
                        m_inputQueue.Enqueue(elem);
                    }
                    else
                    {
                        removedCt += 1;
                    }
                    counter = counter - 1;
                }
            }
            finally
            {
                Monitor.Exit(m_inputQueue);
            }

            return removedCt;
        }

        public string PrintAllElements()
        {
            StringBuilder output = new StringBuilder();

            Monitor.Enter(m_inputQueue);

            try
            {
                foreach (T elem in m_inputQueue)
                {
                    output.AppendLine(elem.ToString());
                }
            }
            finally
            {
                Monitor.Exit(m_inputQueue);
            }

            return output.ToString();
        }
    }
}
