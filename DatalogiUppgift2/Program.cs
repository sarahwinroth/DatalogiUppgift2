using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace DatalogiUppgift2
{
    class Program
    {
        public static TreeLogic tree = new TreeLogic();
        public static List<string> doc1 = new List<string>();
        public static List<string> doc2 = new List<string>();
        public static List<string> doc3 = new List<string>();
        public static List<string> listOfCommands = new List<string>();
        public static List<Result> results;
        public static List<Result> sortedList;

        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            AddTextFromFilesToLists();
            Menu();
            watch.Stop();
        }

        public static void Menu()
        {
            try
            {
                bool run = true;
                while (run)
                {
                    Console.Clear();
                    Console.WriteLine("[MENU]");
                    Console.WriteLine("1. Search for a word in the lists");
                    Console.WriteLine("2. Print all the search results/history (the tree)");
                    Console.WriteLine("3. Print the first number of words from the lists when sorted");
                    Console.WriteLine("4. Print all commandos made by user");
                    Console.Write("> ");
                    int input = Convert.ToInt32(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            listOfCommands.Add("Searched for a word");
                            SearchForAWord();
                            PressEnterToMenu();
                            break;
                        case 2:
                            listOfCommands.Add("Printed the search results");
                            GetTree();
                            PressEnterToMenu();
                            break;
                        case 3:
                            listOfCommands.Add("Printed the first number of words from the lists sorted");
                            PrintTheFirstWordsFromList();
                            PressEnterToMenu();
                            break;
                        case 4:
                            listOfCommands.Add("Printed all commandos made by user");
                            PrintTheFirstWordsFromList();
                            PressEnterToMenu();
                            break;
                    }
                }                
            }
            catch
            {
                Console.WriteLine("Wrong input, please try again!");
            }
        }

        public static void GetTree()
        {
            if (tree.rootNode != null)
            {
                tree.PreorderTraversal();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Unfortunaly there is no search result..");
            }
        }

        public static void PrintTheFirstWordsFromList()
        {
            Console.WriteLine();
            Console.WriteLine("How many words from the lists would you like to print out?");
            Console.Write("> ");
            int input = Convert.ToInt32(Console.ReadLine());

            List<string> newSortedList = doc1.OrderBy(x => x).ToList();
            PrintWordsFromList(1, input, newSortedList);
            newSortedList = doc2.OrderBy(x => x).ToList();
            PrintWordsFromList(2, input, newSortedList);
            newSortedList = doc3.OrderBy(x => x).ToList();
            PrintWordsFromList(3, input, newSortedList);
        }

        public static void PrintWordsFromList(int docNum, int input, List<string> list)
        {
            int i = 0;
            string nextWord = "";

            Console.WriteLine();
            Console.WriteLine("Document nr " + docNum);
            foreach (var word in list)
            {
                if (i == input)
                {
                    break;
                }
                else
                {
                    while (word != nextWord)
                    {
                        Console.WriteLine(word);
                        nextWord = word;
                        i++;
                    }
                }
            }
        }

        public static void SearchForAWord()
        {
            Console.Clear();
            Console.WriteLine("Search for a word..");
            Console.Write(">");
            var wordFromUser = Console.ReadLine();

            //Look if the word already exist in tree if yes print that, else continue and add
            var node = tree.Find(wordFromUser);

            if (node != null)
            {
                PrintResult(node.word, node.results);
            }
            else
            {
                results = new List<Result>();
                sortedList = new List<Result>();

                int amountOfWordsInList = GetAmountFromList(doc1, wordFromUser);
                results.Add(new Result(1, amountOfWordsInList));
                amountOfWordsInList = GetAmountFromList(doc2, wordFromUser);
                results.Add(new Result(2, amountOfWordsInList));
                amountOfWordsInList = GetAmountFromList(doc3, wordFromUser);
                results.Add(new Result(3, amountOfWordsInList));
                sortedList = results.OrderByDescending(o => o.points).ToList();

                PrintResult(wordFromUser, sortedList);

                tree.AddNode(wordFromUser, sortedList);

                results = null;
                sortedList = null;
            }
        }

        public static void PressEnterToMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Press [ENTER] to return to menu..");
            Console.ReadLine();
        }

        public static void PrintResult(string word, List<Result> list)
        {
            int i = 1;

            Console.WriteLine();
            Console.WriteLine("The word: " + word);
            foreach(var obj in list)
            {
                Console.WriteLine(i + ". " + obj.ToString());
                i++;
            }
        }

        public static void AddTextFromFilesToLists()
        {
            var directory = TryGetSolutionDirectoryInfo();
            char[] separators = new char[] { ' ', '.', ',', '"'};

            if (directory != null)
            {
                var pathToDoc1 = directory.FullName + "\\DatalogiUppgift2\\textfiles\\Doc1.txt";
                var pathToDoc2 = directory.FullName + "\\DatalogiUppgift2\\textfiles\\Doc2.txt";
                var pathToDoc3 = directory.FullName + "\\DatalogiUppgift2\\textfiles\\Doc3.txt";

                doc1 = File.ReadAllText(pathToDoc1).Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
                doc2 = File.ReadAllText(pathToDoc2).Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
                doc3 = File.ReadAllText(pathToDoc3).Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }

        public static int GetAmountFromList(List<string> list, string word)
        {
            int totalAmount = 0;

            foreach(var wordInList in list)
            {
                if(word.ToLower() == wordInList.ToLower())
                {
                    totalAmount++;
                }
            }
            return totalAmount;
        }
    }
}
