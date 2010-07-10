using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    /// <summary>
    /// Used to as key to the kerning amount for two characters.
    /// </summary>
    internal struct KernKey
    {
        public int FirstCharacter { get; set; }
        public int SecondCharacter { get; set; }

        public KernKey(int firstCharacter, int secondCharacter) : this()
        {
            FirstCharacter = firstCharacter;
            SecondCharacter = secondCharacter;
        }
    }
}
