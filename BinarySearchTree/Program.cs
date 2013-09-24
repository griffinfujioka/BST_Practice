using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    /// <summary>
    /// Node object. 
    /// </summary>
    class Node
    {
        public int value;
        public Node leftChild, rightChild;

        public Node(int value)
        {
            this.value = value;
            leftChild = null;
            rightChild = null; 
        }
    }

    /// <summary>
    /// Binary tree object.
    /// </summary>
    class BinarySearchTree
    {
        public Node root;
        public int size;

        public int depth
        {
            get
            {
                return this.CalculateDepth(root);
            }
            set
            {
                depth = value; 
            }
        }

        public BinarySearchTree()
        {
            root = null; 
        }

        public BinarySearchTree(Node node)
        {
            root = node; 
        }


        int CalculateDepth(Node node)
        {
            if (node == null)
                return 0;

            int leftDepth = CalculateDepth(node.leftChild);
            int rightDepth = CalculateDepth(node.rightChild);

            if (leftDepth > rightDepth)
                return (leftDepth + 1);
            else
                return (rightDepth + 1);  
        }
        

        /// <summary>
        /// Insert a node into the tree. 
        /// </summary>
        /// <param name="node"></param>
        public void InsertNode(Node node)
        {
            if (root == null)
                root = node;
            else
            {
                if (root.value == node.value)
                    return;
                else if (node.value < root.value)         // Insert into left subtree 
                {
                    if (root.leftChild == null)
                        root.leftChild = node;
                    else
                        InsertNode(root.leftChild, node);
                }
                else if (node.value > root.value)         // Insert into the right subtree
                {
                    if (root.rightChild == null)
                        root.rightChild = node;
                    else
                        InsertNode(root.rightChild, node);
                }
            }
        }

        /// <summary>
        /// Insert a new node into the tree using the provided value. 
        /// </summary>
        /// <param name="node"></param>
        public void InsertNode(int value)
        {
            var node = new Node(value); 

            if (root == null)
                root = node;
            else
            {
                if (root.value == node.value)
                    return;
                else if (node.value < root.value)         // Insert into left subtree 
                {
                    if (root.leftChild == null)
                        root.leftChild = node;
                    else
                        InsertNode(root.leftChild, node);
                }
                else if (node.value > root.value)         // Insert into the right subtree
                {
                    if (root.rightChild == null)
                        root.rightChild = node;
                    else
                        InsertNode(root.rightChild, node);
                }
            }
        }

        /// <summary>
        /// Insert a node into the tree with the parent being either a direct parent or an ancestor. 
        /// </summary>
        /// <param name="parent">Parent of the new node </param>
        /// <param name="node">Node to be inserted </param>
        public void InsertNode(Node parent, Node node)
        {
            if (parent.value == node.value)             // Don't insert duplicates 
                return; 
            else if (node.value < parent.value)         // Insert into left subtree 
            {
                if (parent.leftChild == null)
                    parent.leftChild = node;
                else
                    InsertNode(parent.leftChild, node);
            }
            else if(node.value > parent.value)         // Insert into the right subtree
            {
                if (parent.rightChild == null)
                    parent.rightChild = node;
                else
                    InsertNode(parent.rightChild, node); 
            }
        }

        
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree bst = new BinarySearchTree();

            bst.InsertNode(5);
            bst.InsertNode(7);
            bst.InsertNode(3);
            bst.InsertNode(9);
            bst.InsertNode(1); 

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("\t1. Insert a new node");
            Console.WriteLine("\t2. Print the tree pre-order");
            Console.WriteLine("\t3. Calculate the depth of the tree.");
            Console.WriteLine("\t4. Print the tree level order");

            Console.WriteLine("\n\tPress ESC to exit."); 

            var key = Console.ReadKey();

            while (key.Key != ConsoleKey.Escape)
            {
                Console.WriteLine(); 

                switch (key.KeyChar)
                {
                    case '1':
                        Console.Write("\nEnter the number you'd like to insert into the tree: ");
                        var str = Console.ReadLine();
                        int newValue = Convert.ToInt32(str);

                        bst.InsertNode(newValue);
                        Console.WriteLine();
                        break; 
                    case '2':
                        PrintPreOrder(bst.root);
                        break; 
                    case '3':
                        var depth = bst.depth;
                        Console.WriteLine("Depth: " + depth);
                        break; 
                    case '4':
                        PrintTreeTopDown(bst);  
                        break; 
                    default:
                        break; 
                }

                Console.WriteLine();

                ShowMenu();
                key = Console.ReadKey(); 
            }

            
           
            
        }

        public static void ShowMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("\t1. Insert a new node");
            Console.WriteLine("\t2. Print the tree pre-order");
            Console.WriteLine("Press ESC to exit."); 
        }

        /// <summary>
        /// Print the tree in pre-order. 
        /// </summary>
        /// <param name="root"></param>
        public static void PrintPreOrder(Node root)
        {
            if (root == null)
            {
                return;
            }

            if (root.leftChild == null && root.rightChild == null)
                Console.WriteLine("Value: " + root.value);
            else if (root.leftChild != null && root.rightChild == null)
                Console.WriteLine("Value: " + root.value + "\tLeft child: " + root.leftChild.value); 
            else if(root.leftChild == null && root.rightChild != null)
                Console.WriteLine("Value: " + root.value + "\tRight child: " + root.rightChild.value); 
            else
                Console.WriteLine("Value: " + root.value + "\tLeft child: " + root.leftChild.value + "\tRight child: " + root.rightChild.value);

            if (root.leftChild != null)
            {
                PrintPreOrder(root.leftChild);
                
            }

            if (root.rightChild != null)
            {
                PrintPreOrder(root.rightChild);
            }
        }

        public static void PrintTreeTopDown(BinarySearchTree tree)
        {
            int i;

            for (i = 0; i < tree.depth; i++)
            {
                Console.Write("Level " + i + ": "); 
                var str = PrintLevel(tree.root, i);
                Console.WriteLine(str); 
            }
        }

        public static string PrintLevel(Node node, int level)
        {
            if (node == null)
                return "";

            if (level == 0)
                return node.value.ToString() + " "; 

            var leftString = PrintLevel(node.leftChild, level - 1);
            var rightString = PrintLevel(node.rightChild, level - 1);

            return leftString + rightString; 
        }

 
    }
}
