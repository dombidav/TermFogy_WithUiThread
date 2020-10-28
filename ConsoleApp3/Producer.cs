using System;
using System.Security.Cryptography;
using System.Threading;

namespace ConsoleApp3
{
    internal class Producer
    {
        public Product Actual { get; private set; } = new Product();

        public bool running = false;

        internal void Start()
        {
            running = true;
            var t = new Thread(StartProduce);
            t.Start();
        }

        private void StartProduce()
        {
            while( running )
            {
                if( Manager.products.Count < Manager.maxCount )
                    Produce();
            }
        }

        private void Produce()
        {
            
            lock( Actual )
            {
                Actual = new Product();
                while( Actual.remaining > 0 )
                {
                    Thread.Sleep(1000);
                    Actual.remaining--;
                    lock( this )
                    {
                        Monitor.PulseAll(this);
                    }
                }
            }
            
            lock( Manager.products )
            {
                while( Manager.products.Count >= Manager.maxCount )
                    Monitor.Wait(Manager.products);
                Manager.products.Enqueue(Actual);
                Monitor.PulseAll(Manager.products);
            }
        }
    }
}