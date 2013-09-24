using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_Kth_Smallest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world!\n");

            var root = CreateBST();

            Console.WriteLine(root.value); 

            Console.ReadKey(); 
        }

        /// <summary>
        /// Create a hard-coded BST. 
        /// </summary>
        /// <returns></returns>
        public static Node CreateBST()
        {
            var root = new Node() { value = 8, leftSubtreeCount = 5, rightSubtreeCount = 3};

            return root; 
        }


    }


    public class Node
    {
        public int value;
        public int leftSubtreeCount;
        public int rightSubtreeCount;
        public Node leftChild;
        public Node rightChild; 
    }
}
