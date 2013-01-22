using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaikuApplication
{
    class Word
    {
        string word;
        int plural; //0=s, 1=p
        int numsyl;
        string type;

        public Word(string word, int plural, int numsyl, string type)
        {
            this.word = word;
            this.plural = plural;
            this.numsyl = numsyl;
            this.type = type;
        }

        public string getWord()
        {
            return this.word;
        }

        public int getSyl()
        {
            return this.numsyl;
        }

        public bool isPlural()
        {
            return (this.plural == 1);
        }

        public string getType()
        {
            return (this.type);
        }
    }
}
