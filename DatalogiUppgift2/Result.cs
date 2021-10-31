using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogiUppgift2
{
    /// <summary>
    /// Class responsible for the word list object
    /// </summary>
    public class Result
    {
        public int documentNumber { get; set; }
        public int points { get; set; }

        public Result(int documentNumber, int points)
        {
            this.documentNumber = documentNumber;
            this.points = points;
        }

        public override string ToString()
        {
            return $"Document nr: {documentNumber} \nAmount of times: {points}"; 
        }
    }
}
