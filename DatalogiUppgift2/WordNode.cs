using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogiUppgift2
{
    public class WordNode
    {
        public string word { get; set; }
        public List<Result> results { get; set; }
        public WordNode rightNode { get; set; }
        public WordNode leftNode { get; set; }

        public WordNode(string word, List<Result> results)
        {
            this.word = word;
            this.results = results;
        }

        /// <summary>
        /// Add node to current node
        /// </summary>
        /// <param name="word"></param>
        /// <param name="listOfResults"></param>
        public void AddNode(string word, List<Result> listOfResults)
        {
            if (word.Length >= this.word.Length)
            {   
                if (rightNode == null)
                {
                    rightNode = new WordNode(word, listOfResults);
                }
                else
                {
                    rightNode.AddNode(word, listOfResults);
                }
            }
            else
            {
                if (leftNode == null)
                {
                    leftNode = new WordNode(word, listOfResults);
                }
                else
                {
                    leftNode.AddNode(word, listOfResults);
                }
            }
        }

        /// <summary>
        /// Find word node
        /// </summary>
        /// <param name="word"></param>
        /// <returns>WordNode</returns>
        public WordNode Find(string word)
        {        
            WordNode currentNode = this;

            while (currentNode != null)
            {
                if (word == currentNode.word)
                {
                    return currentNode;
                }
                else if (word.Length >= currentNode.word.Length)
                {
                    currentNode = currentNode.rightNode;
                }
                else
                {
                    currentNode = currentNode.leftNode;
                }
            }
            return null;
        }

        /// <summary>
        /// Displays WordNode
        /// </summary>
        public void PrintNode()
        {
            Console.WriteLine();
            Console.WriteLine(word + " ");

            foreach (var result in results)
            {
                Console.WriteLine(result.ToString());
            }

            if (leftNode != null)
                leftNode.PrintNode();

            if (rightNode != null)
                rightNode.PrintNode();
        }
    }
}
