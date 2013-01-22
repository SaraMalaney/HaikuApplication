using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaikuApplication
{
    class Haiku
    {
        List<Word> nounList;
        List<Word> transverbList;
        List<Word> intransverbList;
        List<Word> tobeverbList;
        List<Word> adjList;
        List<Word> articleList;
        List<Word> pronounList;
        List<Word> adverbList;
        List<List<Word>> masterList;
        Random randomSource;

        public Haiku()
        {
            nounList = new List<Word>();
            transverbList = new List<Word>();
            intransverbList = new List<Word>();
            tobeverbList = new List<Word>();
            adjList = new List<Word>();
            articleList = new List<Word>();
            pronounList = new List<Word>();
            adverbList = new List<Word>();
            masterList = new List<List<Word>>();
            randomSource = new Random();
         }

        public void populateLists()
        {
            const string directoryPath = @"C:\Users\ThinkPad\Documents\Visual Studio 2010\Projects\HaikuApplication\";
            populateList(directoryPath + "nounlist.csv", nounList);
            populateList(directoryPath + "transverblist.csv", transverbList);
            populateList(directoryPath + "intransverblist.csv", intransverbList);
            populateList(directoryPath + "tobeverblist.csv", tobeverbList);
            populateList(directoryPath + "adjlist.csv", adjList);
            populateList(directoryPath + "articlelist.csv", articleList);
            populateList(directoryPath + "pronounlist.csv", pronounList);
            populateList(directoryPath + "adverblist.csv", adverbList);

            masterList.Add(nounList);
            masterList.Add(transverbList); // added trans, intrans, tobe verb lists to masterlist - Laurence
            masterList.Add(intransverbList);
            masterList.Add(tobeverbList);
            masterList.Add(adjList);
            masterList.Add(articleList);
            masterList.Add(pronounList);
            masterList.Add(adverbList);

        }

        void populateList(string filelocation, List<Word> list)
        {
            string ReadAll = System.IO.File.ReadAllText(filelocation);
            string[] lineSplit = ReadAll.Split('\n');

            foreach(string s in lineSplit)
            {
                string[] thing = s.Split(',');
                if (thing.Length == 4)
                {
                    Word temp = new Word(thing[0], thing[1].Contains("s") ? 0 : 1, Int32.Parse(thing[2]), thing[3]);
                    list.Add(temp);
                }
  
            }

        }

        //5 syl line generator, for line 1 and 3
        public string createLine1()
        {
            int sylcount = 0;
            bool isplural = false;
            string type = "none";
            String firstline = null;

            Word firstword = ChooseFirstWord();
            firstline = firstword.getWord();
            sylcount = sylcount + firstword.getSyl();
            type = firstword.getType();
            isplural = firstword.isPlural();

            while (sylcount < 5)
            {
                Word nextword = ChooseWord(isplural, type);

                while ((sylcount + nextword.getSyl()) > 5)
                {
                    nextword = ChooseWord(isplural, type);
                }
                firstline = firstline + " " + nextword.getWord();
                
                if (nextword.getType() != "adj\r")
                {
                    isplural = nextword.isPlural();
                }

                type = nextword.getType();
                
                sylcount = sylcount + nextword.getSyl();     
            }
            System.Console.Out.WriteLine(firstline);
            return firstline;
        }

        //generate 7 syl line, for line 2 of haiku
        public string createLine2()
        {
            int sylcount = 0;
            bool isplural = false;
            string type = "none";
            string secondline = null;

            Word firstword = ChooseFirstWord();
            secondline = firstword.getWord();
            sylcount = sylcount + firstword.getSyl();
            type = firstword.getType();
            isplural = firstword.isPlural();

            while (sylcount < 7)
            {
                Word nextword = ChooseWord(isplural, type);
                while ((sylcount + nextword.getSyl()) > 7)
                {
                    nextword = ChooseWord(isplural, type);
                }
                secondline = secondline + " " + nextword.getWord();
                type = nextword.getType();
                isplural = nextword.isPlural();
                sylcount = sylcount + nextword.getSyl();

            }

            System.Console.Out.WriteLine(secondline);
            return secondline;
        }

        public Word ChooseFirstWord()
        {   
            List<Word> temp = masterList[this.randomSource.Next(4)];
            Word word = temp[this.randomSource.Next(temp.Count)];   
            return(word);
        }

        public Word ChooseWord(bool isplural, string wordtype)
        {
            List<Word> chooseList = null;
            List<Word> templist = null;

            //System.Console.Out.WriteLine("debug: choosing word, type = " + wordtype);
            if (wordtype == "noun" | wordtype == "noun\r")
            {
                chooseList = new List<Word>(intransverbList);
                chooseList.AddRange(transverbList);
                chooseList.AddRange(tobeverbList);
            }
            else if (wordtype == "adj" | wordtype == "adj\r")
            {
                chooseList = new List<Word>(nounList);
            }
            else if (wordtype == "article" | wordtype == "article\r")
            {
                chooseList = new List<Word>(adjList);
                chooseList.AddRange(nounList);
               chooseList.AddRange(adverbList);
            }
            else if (wordtype == "transverb" | wordtype == "transverb\r")
            {
                chooseList = new List<Word>(nounList);
            }
            else if (wordtype == "intransverb" | wordtype == "intransverb\r")
            {
                chooseList = new List<Word>(adverbList);
            }
            else if (wordtype == "tobeverb" | wordtype == "tobeverb\r")
            {
                chooseList = new List<Word>(adjList);
            }
            else if (wordtype == "pronoun" | wordtype == "pronoun\r")
            {
                chooseList = new List<Word>(transverbList);
                chooseList.AddRange(intransverbList);
                chooseList.AddRange(tobeverbList);
            }
            else if (wordtype == "adverb" | wordtype == "adverb\r")
            {
                chooseList = new List<Word>(intransverbList);
                chooseList.AddRange(pronounList);
            }
                            
            Word temp = chooseList[this.randomSource.Next(chooseList.Count)];
            bool plurality = temp.isPlural();

            //System.Console.Out.WriteLine("debug: finding plurality");
            if (wordtype != "adj\r" & wordtype != "adverb\r")
            {
                //System.Console.Out.WriteLine("debug: plurality check plural: " + (plurality != isplural).ToString());
                while (plurality != isplural)
                {
                    temp = chooseList[this.randomSource.Next(chooseList.Count)];
                    plurality = temp.isPlural();
                }
            }        
            return(temp);
        }

    }
}
