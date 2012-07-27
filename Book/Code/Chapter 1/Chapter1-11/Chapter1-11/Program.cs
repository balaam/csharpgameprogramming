using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1_11
{
    class Program
    {
        class Item
        {
            string _name;
            public Item(string name)
            {
                _name = name;
            }
        }

        class Orc
        {
           List<Item> _items = new List<Item>();
   
           public Orc()
           {
   	            _items.Add(new Item("axe"));
   	            _items.Add(new Item("pet_hamster"));
   	            _items.Add(new Item("skull_helm"));
           }
        }

        class Orc_UsingCollectionInitializers
        {
           List<Item> _items = new List<Item>  
           {
    	        new Item("axe"), 
    	        new Item("pet_hamster"),
    	        new Item("skull_helm")  
           };
        }


        static void Main(string[] args)
        {
            Orc Orc1 = new Orc();
            Orc_UsingCollectionInitializers Orc2 = new Orc_UsingCollectionInitializers();

            
            List<List<Orc>> OrcSquads = new List<List<Orc>>();
            var OrcSquads_UsingVar = new List<List<Orc>>();

            var fullname = new { firstName = "John", lastName = "Doe" };
        }
    }
}
