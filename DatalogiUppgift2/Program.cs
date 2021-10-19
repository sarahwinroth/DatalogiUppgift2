using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DatalogiUppgift2
{
    class Program
    {
        public static List<string> doc1 = new List<string>();
        public static List<string> doc2 = new List<string>();
        public static List<string> doc3 = new List<string>();

        static void Main(string[] args)
        {
            AddTextFromFilesToLists();
            Menu();
        }

        public static void Menu()
        {
            try
            {
                bool run = true;
                while (run)
                {
                    Console.WriteLine();
                    Console.WriteLine("[MENU]");
                    Console.WriteLine("1. Search for a word in the lists");
                    Console.Write(">");
                    int input = Convert.ToInt32(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            SearchForAWord();
                            break;
                    }
                }                
            }
            catch
            {
                Console.WriteLine("Wrong input, please try again!");
            }
        }

        public static void SearchForAWord()
        {
            Console.WriteLine();
            Console.WriteLine("Search for a word..");
            Console.Write(">");
            var wordFromUser = Console.ReadLine();
            int amountOfWordsInList = GetAmountFromList(doc1, wordFromUser);
            PrintResult(1, wordFromUser, amountOfWordsInList);

            amountOfWordsInList = GetAmountFromList(doc2, wordFromUser);
            PrintResult(2, wordFromUser, amountOfWordsInList);

            amountOfWordsInList = GetAmountFromList(doc3, wordFromUser);
            PrintResult(3, wordFromUser, amountOfWordsInList);
        }

        public static void PrintResult(int docNum, string wordFromUser, int amountOfWordsInList)
        {
            if (amountOfWordsInList == 0)
            {
                Console.WriteLine("The word " + wordFromUser + " didn't exist in document nr " + docNum);
            }
            else
            {
                Console.WriteLine("The word " + wordFromUser + " was discovered " + amountOfWordsInList + " times in document nr " + docNum);
            }
        }

        public static void AddTextFromFilesToLists()
        {
            var directory = TryGetSolutionDirectoryInfo();

            if (directory != null)
            {
                var pathToDoc1 = directory.FullName + "\\DatalogiUppgift2\\textfiles\\Doc1.txt";
                var pathToDoc2 = directory.FullName + "\\DatalogiUppgift2\\textfiles\\Doc2.txt";
                var pathToDoc3 = directory.FullName + "\\DatalogiUppgift2\\textfiles\\Doc3.txt";

                doc1 = File.ReadAllText(pathToDoc1).Split(' ').ToList();
                doc2 = File.ReadAllText(pathToDoc2).Split(' ').ToList();
                doc3 = File.ReadAllText(pathToDoc3).Split(' ').ToList();
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
                if(word == wordInList)
                {
                    totalAmount++;
                }
            }
            return totalAmount;
        }
    }
}
