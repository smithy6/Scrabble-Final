using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Scrabble_Remix
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Program scrabbleGame = new Program();

            while (scrabbleGame.loopGame)
            {
                scrabbleGame.Run();
            }
        }
    }
}

