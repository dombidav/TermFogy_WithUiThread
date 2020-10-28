using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace ConsoleApp3
{
    internal class Program
    {
        private static bool running = true;

        private static void Main()
        {
            Manager.StartProducers();
            Thread t = StartUIThread();
            ConsoleKeyInfo v = new ConsoleKeyInfo();
            while( v.Key != ConsoleKey.Escape )
            {
                v = Console.ReadKey();
                switch( v.Key )
                {
                    case ConsoleKey.Escape:
                        running = false;
                        break;

                    case ConsoleKey.F:
                        Manager.AddConsumer();
                        break;

                    default:
                        break;
                }
            }
            t.Join();
            Console.WriteLine("DONE");
        }

        private static Thread StartUIThread()
        {
            Thread t = new Thread(UIRefresh);
            t.Start();
            return t;
        }

        private static void UIRefresh()
        {
            while( running )
            {
                Console.Clear();
                foreach( var item in Manager.producers )
                {
                    Console.WriteLine($"{item.Actual}{(item.Actual.IsCompleted ? " - Waiting for consumers..." : "")}");
                }
                Console.WriteLine("--------");
                Console.WriteLine("Completed: ");
                lock( Manager.products )
                {
                    Console.WriteLine(string.Join(", ", Manager.products)); 
                }
                Console.WriteLine("--------");
                Console.WriteLine($"Consumers waiting: {Manager.consumers.Count}");
                Console.WriteLine("--------");
                Console.WriteLine("Consumed: ");
                Console.WriteLine(string.Join(", ", Manager.consumed));

                Thread.Sleep(1000);
            }
        }
    }
}