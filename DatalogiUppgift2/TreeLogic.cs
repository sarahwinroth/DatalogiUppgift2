using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogiUppgift2
{
    public class TreeLogic
    {
        public WordNode rootNode;

        /// <summary>
        /// Add node to tree
        /// </summary>
        /// <param name="word"></param>
        /// <param name="listOfResult"></param>
        public void AddNode(string word, List<Result> listOfResult)
        {
            if(rootNode != null)
            {
                rootNode.AddNode(word, listOfResult);
            }
            else
            {
                rootNode = new WordNode(word, listOfResult);
            }
        }

        /// <summary>
        /// Check if root node is empty before search
        /// </summary>
        /// <param name="word"></param>
        /// <returns>WordNode</returns>
        /// Time complexity O(3)
        public WordNode Find(string word)
        {
            if (rootNode != null)
            {
                return rootNode.Find(word);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check if root node is empty before printing
        /// </summary>
        /// <returns></returns>
        public bool PrintTree()
        {
            if (rootNode != null)
            {
                rootNode.PrintNode();
                return true;
            }
            else
            { return false;  }
        }
    }
}
