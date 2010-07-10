using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Engine
{
    public class FontParser
    {
        static int HeaderSize = 2;

        // Gets the value after an equal sign and converts it
        // from a string to an integer
        private static int GetValue(string s)
        {
            string value = s.Substring(s.IndexOf('=') + 1);
            return int.Parse(value);
        }


        private static string GetTextValue(string s)
        {
            string value = s.Substring(s.IndexOf('=') + 1);
            return value;
        }

        /// <summary>
        /// Create a font object from a .fnt file (and associated textures)
        /// </summary>
        /// <param name="path">Path to the .fnt file.</param>
        /// <param name="textureManager">Texture manager to load font textures</param>
        /// <returns>Font as described by the .fnt file.</returns>
        public static Font CreateFont(string path, TextureManager textureManager)
        {
            List<Texture> _texturePages = new List<Texture>();
            Dictionary<KernKey, int> kernDictionary = new Dictionary<KernKey, int>();
            Dictionary<char, CharacterData> charDictionary = new Dictionary<char, CharacterData>();

            string[] lines = File.ReadAllLines(path);

            int texturePageInfo = HeaderSize;
            while (lines[texturePageInfo].StartsWith("page"))
            {
                string line = lines[texturePageInfo];
                string[] typesAndValues = line.Split(" ".ToCharArray(),
                  StringSplitOptions.RemoveEmptyEntries);
                string textureString = GetTextValue(typesAndValues[2]).Trim('"');
                string textureId = Path.GetFileNameWithoutExtension(textureString);

                if (textureManager.Exists(textureId))
                {
                    // Really textures should never be loaded twice so it's worth warning the user
                    Console.Error.WriteLine("WARNING: Tried to load a texture that had been already been loaded. "
                        + "[" + textureString + "] when loading font [" + path + "]");
                }
                else
                {
                    // Assume texture files are in the same path as the .fnt file.
                    string directory = Path.GetDirectoryName(path);
                    if (string.IsNullOrEmpty(directory) == false)
                    {
                        directory += "\\";
                    }
                    textureManager.LoadTexture(textureId, directory  + textureString);   
                }

                _texturePages.Add(textureManager.Get(textureId));

                texturePageInfo++;
            }

            texturePageInfo++; // jump over number of characters data.

            for (int i = texturePageInfo; i < lines.Length; i += 1)
            {
                string line = lines[i];
                string[] typesAndValues = line.Split(" ".ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);

                // Some fonts have kerning data at the end
                if (line.StartsWith("kernings"))
                {
                    ParseKernData(i + 1, lines, kernDictionary);
                    break;
                }

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

            return new Font(_texturePages.FirstOrDefault(), charDictionary, kernDictionary);
        }

        private static void ParseKernData(int start, string[] lines, Dictionary<KernKey, int> kernDictionary)
        {
            for (int i = start; i < lines.Length; i += 1)
            {
                string line = lines[i];
                string[] typesAndValues = line.Split(" ".ToCharArray(), 
                    StringSplitOptions.RemoveEmptyEntries);
                // As before the order of the enteries is used to make the parsing simpler.
                KernKey key = new KernKey(GetValue(typesAndValues[1]), GetValue(typesAndValues[2]));
                kernDictionary.Add(key, GetValue(typesAndValues[3]));
            }
        }
    }

}
