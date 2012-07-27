using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Chapter11_2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TextGame textGame = new TextGame();
            InitGame(textGame);
            textGame.Play();
        }

        private static void InitGame(TextGame textGame)
        {
            Room startingRoom = new Room("start", "You are in a dark room, you can see nothing.");
            textGame.AddRoom(startingRoom);
            textGame.SetStartRoom("start");
        }
    }
}
