using System;
using System.Threading;

namespace ConsoleApp3
{
    internal class Consumer
    {
        internal void StartConsume()
        {
            var t = new Thread(Consume);
            t.Start();
        }

        private void Consume()
        {
            Product result;
            while( !Manager.products.TryDequeue(out result) )
                lock( Manager.products )
                {
                    Monitor.Wait(Manager.products);
                }
            lock( Manager.consumed )
            {
                Manager.consumed.Enqueue(result);
                Monitor.PulseAll(Manager.consumed);
            }
            lock( Manager.consumers )
            {
                Manager.consumers.Remove(this);
                Monitor.PulseAll(Manager.consumers);
            }
        }
    }
}