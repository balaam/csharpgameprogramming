using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_6
{
    class Program
    {
        class Item
        {
            string _name = "Unnamed item";
            bool _dead = false;


            public void Destroy()
            {
                // Add a break point here to check it's being called.
                _name = "";
                _dead = true;
            }
        }

        delegate void SomeDelegate(Item item);

        void DoToList(List<Item> list, SomeDelegate action)
        {
            foreach (Item item in list)
            {
                action(item);
            }
        }

        static void Main(string[] args)
        {
            List<Item> _itemList = new List<Item>();
            _itemList.Add(new Item());
            _itemList.Add(new Item());

            Program program = new Program();

            program.DoToList(_itemList, delegate(Item item) { item.Destroy(); });
        }
    }
}
