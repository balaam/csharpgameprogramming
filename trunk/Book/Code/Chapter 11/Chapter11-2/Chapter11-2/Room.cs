using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11_2
{
    public class Room
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public List<Room> Exits { get; set; }
        public Room(string description, List<Room> exits)
        {
            Exits = exits;
            Description = description;
        }

        public Room(string id, string description)
        {
            Id = id;
            Description = description;
        }

        internal string Look()
        {
            return Description;
        }
    }
}
