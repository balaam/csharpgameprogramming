using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_9
{
    class Program
    {
        class Item
        {
            public void Destroy()
            {
                Console.WriteLine("Destroyed!");
            }
        }


        static void Main(string[] args)
        {
            List<Item> itemList = new List<Item>();
            itemList.Add(new Item());
            itemList.Add(new Item());


            // Anonymous delegate
            itemList.ForEach(delegate(Item x) { x.Destroy(); });

            // Lambda function
            itemList.ForEach(x => x.Destroy());


        }
    }
}
