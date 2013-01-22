using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaikuApplication
{
    class Program
    {
       

        static void Main(string[] args)
        {
            Haiku testHaiku = new Haiku();
            
            testHaiku.populateLists();
            testHaiku.createLine1();
            testHaiku.createLine2();
            testHaiku.createLine1();
            System.Console.ReadLine();
        }

        
    }
}
