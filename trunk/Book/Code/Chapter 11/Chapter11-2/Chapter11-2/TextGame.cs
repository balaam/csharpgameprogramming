using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11_2
{
    class TextGame
    {
        // These could be put in a structure or class.
        const string Look = "look";
        const string Search = "search";
        const string Use = "use";
        const string On = "on";
        const string DidntUnderstand = "I'm sorry. I don't understand this command.";

        bool _exitGame = false;
        string _output = "";
        Dictionary<string, Room> _rooms = new Dictionary<string, Room>();
        Room _currentRoom;

        internal void Play()
        {
            System.Diagnostics.Debug.Assert(_currentRoom != null, "A current room must be set before playing the game.");
            while (_exitGame == false)
            {
                Console.WriteLine(_output);
                Console.Write(">");
                string input = Console.ReadLine();
                _output = HandleInput(input.ToLower());
            }
        }

        private string HandleInput(string input)
        {
            if (input == Look)
            {
                // The look function writes the description to the console.
                return _currentRoom.Look();
            }
            else if (input == Search)
            {
                // Search the room!
                return "write some code to handle this!";
            }
            else if (input.StartsWith(Use))
            {
                string strippedUseStatement = input.Remove(input.IndexOf(Use), Use.Length );
                strippedUseStatement = strippedUseStatement.Trim();

                if (string.IsNullOrWhiteSpace(strippedUseStatement))
                {
                    return DidntUnderstand;
                }

                if (strippedUseStatement.Contains(On))
                {
                    int onIndex = strippedUseStatement.IndexOf(On);
                    string parameter1 = strippedUseStatement.Substring(0, onIndex);
                    string parameter2 = strippedUseStatement.Substring(onIndex + On.Length);
                    return DoUseAction(parameter1.Trim(), parameter2.Trim());
                }

                // We might also want to support commands like "use computer" here
            }

            return DidntUnderstand;
        }

        private string DoUseAction(string parameter1, string parameter2)
        {
            return "You can't use " + parameter1 + " on " + parameter2;
        }

        internal void AddRoom(Room startingRoom)
        {
            _rooms.Add(startingRoom.Id, startingRoom);
        }

        internal void SetStartRoom(string roomId)
        {
            _currentRoom = _rooms[roomId];
            _output = _currentRoom.Description;
        }
    }
}
