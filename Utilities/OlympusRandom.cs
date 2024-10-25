using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace OlympusMod.Utilities
{
    public static class OlympusRandom
    {
        public static Color RandomColor()
        {
            int randR = Main.rand.Next(256);
            int randG = Main.rand.Next(256);
            int randB = Main.rand.Next(256);
            return new Color(randR, randG, randB);
        }
    }
}
