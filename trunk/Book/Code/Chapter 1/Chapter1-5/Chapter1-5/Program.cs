using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_5
{
    class Program
    {
        class Item
        {
        }

        delegate void SomeDelegate(Item item);

        /// <summary>
        /// Takes it a list of items and then performs an action to each item in the list.
        /// </summary>
        /// <param name="list">The list of items</param>
        /// <param name="action">The action to perform on each item</param>
        void DoToList(List<Item> list, SomeDelegate action)
        {
            foreach (Item item in list)
            {
                action(item);
            }
        }

        void PrintAction(Item item)
        {
            System.Console.WriteLine("Performing action on " + item.ToString());
        }

        static void Main(string[] args)
        {
            List<Item> list = new List<Item>();
            list.Add(new Item());
            list.Add(new Item());

            Program program = new Program();

            // Use the PrintAction method as a delegate
            program.DoToList(list, program.PrintAction);

            // This is an annoymous delegate
            program.DoToList(list, delegate(Item item)
            {
                System.Console.WriteLine("Performing action on " + item.ToString());
            });

        }
    }
}
