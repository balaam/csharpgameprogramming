using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int _mapWidth = 10;
            int _mapHeight = 10;
            int _playerX = 0;
            int _playerY = 0;
            bool _playerIsAlive = true;
            Console.CursorVisible = false; // stop the blinking cursor

            while (_playerIsAlive)
            {
              
                for (int i = 0; i < _mapHeight; i++)
                {
                    for (int j = 0; j < _mapWidth; j++)
                    {
                        if (j == _playerX && i == _playerY)
                        {
                            Console.Write('@');
                        }
                        else
                        {
                            Console.Write('.');
                        }
                    }
                    Console.WriteLine();
                }
                

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    _playerX = Math.Max(0, _playerX - 1);
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    _playerX = Math.Min(_mapWidth, _playerX + 1);
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    _playerY = Math.Max(0, _playerY - 1);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    _playerY = Math.Min(_mapHeight, _playerY + 1);
                }
                Console.SetCursorPosition(0, 0);
                 
            }

        }
    }
}
