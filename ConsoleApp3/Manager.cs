using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    internal class Manager
    {
        internal static Queue<Product> products = new Queue<Product>();
        internal static Queue<Product> consumed = new Queue<Product>();
        internal static int maxCount = 5;
        public static List<Producer> producers = new List<Producer>();
        public static List<Consumer> consumers = new List<Consumer>();

        internal static void StartProducers( int count = 3 )
        {
            for( int i = 0; i < count; i++ )
            {
                var temp = new Producer();
                producers.Add(temp);
                temp.Start();
            }
        }

        internal static void AddConsumer()
        {
            var temp = new Consumer();
            consumers.Add(temp);
            temp.StartConsume();
        }
    }
}