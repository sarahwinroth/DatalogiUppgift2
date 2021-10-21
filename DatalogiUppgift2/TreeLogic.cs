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
