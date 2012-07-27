using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chapter8_3
{
    /// <summary>
    /// This parser doesn't support kerning! It's also quite brittle and will die on unexpected input.
    /// 
    /// 
    /// Kerning is the minor adjustments made to certain characters when they're next to each other.
    /// Implementing kerning will make your text look a lot better!
    /// 
    /// But it's easy to add - you need to fill in a dictionary that
    /// takes a pair of characters for key and returns an int kerning amount.
    /// such as: Dictionary<Tuple<char, char>, int>> (this definition requires C# 4.0)
    /// </summary>
    public class FontParser
    {
        static int HeaderSize = 4;

        // Gets the value after an equal sign and converts it
        // from a string to an integer
        private static int GetValue(string s)
        {
            string value = s.Substring(s.IndexOf('=') + 1);
            return int.Parse(value);
        }

        public static Dictionary<char, CharacterData> Parse(string filePath)
        {
            Dictionary<char, CharacterData> charDictionary = new Dictionary<char, CharacterData>();

            string[] lines = File.ReadAllLines(filePath);

            for (int i = HeaderSize; i < lines.Length; i += 1)
            {
                string firstLine = lines[i];
                string[] typesAndValues = firstLine.Split(" ".ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);

                // All the data comes in a certain order,
                // used to make the parser shorter
                CharacterData charData = new CharacterData
                {
                    Id = GetValue(typesAndValues[1]),
                    X = GetValue(typesAndValues[2]),
                    Y = GetValue(typesAndValues[3]),
                    Width = GetValue(typesAndValues[4]),
                    Height = GetValue(typesAndValues[5]),
                    XOffset = GetValue(typesAndValues[6]),
                    YOffset = GetValue(typesAndValues[7]),
                    XAdvance = GetValue(typesAndValues[8])
                };
                charDictionary.Add((char)charData.Id, charData);
            }
            return charDictionary;
        }
    }

}
