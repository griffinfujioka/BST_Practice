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

        public BinarySearchTree()
        {
            root = null; 
        }

        public BinarySearchTree(Node node)
        {
            root = node; 
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

            PrintPreOrder(bst.root);

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine(); 
            
           
            
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
            Console.Write("\t");

            Console.Write(root.value);

            Console.Write("\t");

            Console.Write("\n"); 
      
            PrintPreOrder(root.leftChild);

            Console.Write("\t");

            PrintPreOrder(root.rightChild);
        }

        
    }
}
