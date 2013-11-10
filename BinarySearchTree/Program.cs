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
        public int size
        {
            get
            {
                return this.CountNodes(root);
            }
            set
            {
                size = value;
            }
        }

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

        /// <summary>
        /// Calculate the depth of the tree. 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
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
        /// Count the number of nodes in the tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int CountNodes(Node node)
        {
            int sum;

            if (node == null)
                return 0;
            else
                sum = 1; 

            if(node.leftChild != null) 
                sum += CountNodes(node.leftChild);
            if (node.rightChild != null)
                sum += CountNodes(node.rightChild);

            return sum;
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

            CalculateDepth(this.root);
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

        /// <summary>
        /// Perform Breadth First Search on the BST, searching for the provided value.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int? BreadthFirstSearch(Node root, int value)
        {
            // Use the FIFO stack 
            Queue<Node> myQueue = new Queue<Node>();

            if (root == null)
                return null;

            myQueue.Enqueue(root);

            while (myQueue.Count > 0)
            {
                Node current = myQueue.Dequeue();

                if (current == null)
                    continue;
                else if (current.value == value)
                    return value; 
                myQueue.Enqueue(current.leftChild);
                myQueue.Enqueue(current.rightChild); 
            }


            return null;
        }


        /// <summary>
        /// Perfrom Depth First Search on the BST, searching for the provided value. 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int? DepthFirstSearch(Node root, int value)
        {
            // Use the LIFO stack 
            Stack<Node> myStack = new Stack<Node>();

            if (root == null)
                return null;

            myStack.Push(root);

            while (myStack.Count > 0)
            {
                Node current = myStack.Pop();

                if (current == null)
                    continue;
                else if (current.value == value)
                    return value;

                myStack.Push(current.leftChild);
                myStack.Push(current.rightChild);

            }

            return null; 
        }

        public void Balance()
        {
            // Make a sorted list out of the tree
            var list = this.ToList();
            list.Sort((x, y) => x.value.CompareTo(y.value));

            // Empty the tree
            this.Clear();

            // Choose middle element as root
            this.root = list[Convert.ToInt32(Math.Floor((double)list.Count / 2))];

            list.Remove(this.root);

            foreach (var node in list)
            {
                this.InsertNode(node); 
            }
            
            
        }

        /// <summary>
        /// Convert the tree to a list. 
        /// </summary>
        /// <returns></returns>
        public List<Node> ToList()
        {
            Queue<Node> myQueue = new Queue<Node>();
            var list = new List<Node>();

            myQueue.Enqueue(this.root);

            while (myQueue.Count > 0)
            {
                var node = myQueue.Dequeue();

                list.Add(node);

                if(node.leftChild != null)
                    myQueue.Enqueue(node.leftChild);

                if(node.rightChild != null)
                    myQueue.Enqueue(node.rightChild);
            }

            return list;
        }

        /// <summary>
        /// Clear the tree
        /// </summary>
        public void Clear()
        {
            this.root = null;  
        }


        /// <summary>
        /// Validate if the tree is a valid BST, or just a BST
        /// </summary>
        /// <returns>True if the tree is a valid BST, else returns false.</returns>
        public bool IsValid()
        {
            var isValid = Validate(this.root); 
            return isValid; 
        }

        /// <summary>
        /// Recursive function for validating a BST. 
        /// </summary>
        /// <returns></returns>
        public bool Validate(Node node)
        {
            if (node == null)
                return false;

            if (node.leftChild != null)
            {
                if (node.leftChild.value > node.value)
                    return false;

                Validate(node.leftChild);
            }


            if (node.rightChild != null)
            {
                if (node.rightChild.value < node.value)
                                return false;

                Validate(node.rightChild);
            }

            
            return true; 
        }

        /// <summary>
        /// Calculate the path through the tree that will result in the maximum sum. 
        /// </summary>
        /// <param name="node"></param>
        /// <returns>An integer array of all nodes in the path.</returns>
        public int[] MaxSumPath(Node node)
        {
            int[] returnArray = new int[this.depth];     // there will be at most this.depth nodes
            int i = 0;


            if (this.IsValid())
            {
                // Now the path is simple; follow the right child all the way down 
                Node current = this.root;

                while (current != null)
                {
                    returnArray[i] = current.value;
                    i++;

                    current = current.rightChild;
                }
            }
            else
            {
                // Not a valid BST, let's balance it 
                Balance();

                Node current = this.root;

                while (current != null)
                {
                    returnArray[i] = current.value;
                    i++;

                    current = current.rightChild;
                }

            }

            return returnArray; 
        }

        /// <summary>
        /// Given two nodes, return their first common parent. 
        /// That is, the common parent with the lowest depth. 
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public Node FirstCommonParent(int n1, int n2)
        {
            Node parentNode = new Node(0);
            int n1Depth, n2Depth, smallestDepth; 

            n1Depth = DetermineDepth(n1); 
            n2Depth = DetermineDepth(n2);

            if (n1Depth < n2Depth)
                smallestDepth = n1Depth;
            else
                smallestDepth = n2Depth;

           

            return parentNode; 
        }

        /// <summary>
        /// Given a node in the tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int DetermineDepth(int value)
        {
            int depthCounter = 0; 
            Stack<Node> myStack = new Stack<Node>();

            if (root == null)
                return -1;

            myStack.Push(root);

            while (myStack.Count > 0)
            {
                Node current = myStack.Pop();

                if (current == null)
                    continue;
                else if (current.value == value)
                    return depthCounter;

                myStack.Push(current.leftChild);
                myStack.Push(current.rightChild);
                depthCounter++; 

            }

            return -1; 
        }

        /// <summary>
        /// Determine if the parent node has the given ancestor.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="ancestor"></param>
        /// <returns></returns>
        private bool IsAncestorOf(Node parent, Node ancestor)
        {
            // Balance the tree before we do anything 
            this.Balance();

            if (DepthFirstSearch(parent, ancestor.value) != null)
                return true;

            return false; 
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
            ShowMenu(); 

            var line = Console.ReadLine();

            while (true)
            {
                Console.WriteLine(); 

                switch (line)
                {
                    case "1":
                        Console.Write("\nEnter the number you'd like to insert into the tree: ");
                        var str = Console.ReadLine();
                        int newValue = Convert.ToInt32(str);

                        bst.InsertNode(newValue);
                        Console.WriteLine();
                        break; 
                    case "2":
                        PrintPreOrder(bst.root);
                        break; 
                    case "3":
                        var depth = bst.depth;
                        Console.WriteLine("Depth: " + depth);
                        break; 
                    case "4":
                        PrintTreeTopDown(bst);  
                        break; 
                    case "5":
                        Console.Write("\nWhat number are we looking for?: ");
                        var searchStr = Console.ReadLine();
                        int searchValue = Convert.ToInt32(searchStr);
                        int? found = bst.BreadthFirstSearch(bst.root, searchValue);

                        if (found != null)
                            Console.WriteLine("Found {0}!", searchValue);
                        else
                            Console.WriteLine("{0} was not found in the tree.", searchValue); 
                        break; 
                    case "6":
                        bst = new BinarySearchTree();
                        Console.WriteLine("Cleared the tree.");
                        break; 
                    case "7":
                        bst.Balance();
                        break;
                    case "8":
                        if (bst.IsValid())
                            Console.WriteLine("The tree is a valid BST");
                        else
                            Console.WriteLine("The tree is not a valid BST"); 
                        break;
                    case "9":
                        Console.WriteLine("{0} nodes in the tree", bst.size);
                        break; 
                    case "10":
                        Console.Write("\nWhat number are we looking for?: ");
                        searchStr = Console.ReadLine();
                        searchValue = Convert.ToInt32(searchStr);
                        found = bst.DepthFirstSearch(bst.root, searchValue);

                        if (found != null)
                            Console.WriteLine("Found {0}!", searchValue);
                        else
                            Console.WriteLine("{0} was not found in the tree.", searchValue); 
                        break; 
                    case "11":
                        var maxSumPath = bst.MaxSumPath(bst.root);
                        int sum = 0; 
                        foreach (var value in maxSumPath)
                        {
                            Console.Write("{0} ", value);
                            sum += value; 
                        }

                        Console.Write(" = {0}", sum); 
                        break; 
                    case "12":
                        PrintInOrder(bst.root); 
                        break; 
                    case "13":
                        PrintPostOrder(bst.root);
                        break; 
                    case "14":
                        bst.FirstCommonParent(3, 1); 
                        break; 
                    default:
                        break; 
                }

                Console.WriteLine();

                ShowMenu();
                line = Console.ReadLine(); 
            }

            
           
            
        }

        public static void ShowMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("\t1. Insert a new node");
            Console.WriteLine("\t2. Print the tree pre-order");
            Console.WriteLine("\t3. Calculate the depth of the tree.");
            Console.WriteLine("\t4. Print the tree level order");
            Console.WriteLine("\t5. Perform breadth-first search on the tree.");
            Console.WriteLine("\t6. Clear the tree.");
            Console.WriteLine("\t7. Balance the tree.");
            Console.WriteLine("\t8. Validate a BST.");
            Console.WriteLine("\t9. Count nodes in a tree.");
            Console.WriteLine("\t10. Perform depth-first search on the tree.");
            Console.WriteLine("\t11. Find the Maximum Sum Path .");
            Console.WriteLine("\t12. Print the tree in-order");
            Console.WriteLine("\t13. Print the tree in-order");
            Console.WriteLine("\t14. Find first common parent for nodes 3 and 1"); 
            Console.WriteLine("\n\tPress ESC to exit."); 
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

        /// <summary>
        /// Print the tree Post-Order 
        /// </summary>
        /// <param name="root"></param>
        public static void PrintPostOrder(Node root)
        {
            if (root == null)
                return;

            PrintPostOrder(root.leftChild);
            PrintPostOrder(root.rightChild);

            Console.WriteLine(root.value); 
        }

        /// <summary>
        /// Print the tree In Order 
        /// </summary>
        /// <param name="root"></param>
        public static void PrintInOrder(Node root)
        {
            if (root == null)
                return; 

            if (root.leftChild != null)
            {
                PrintInOrder(root.leftChild);
            }

            Console.WriteLine(root.value); 

            if (root.rightChild != null)
            {
                PrintInOrder(root.rightChild); 
            }
        }

        /// <summary>
        /// Print the tree level order starting from the top down
        /// </summary>
        /// <param name="tree"></param>
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

        /// <summary>
        /// Recursive function for printing a tree level order. 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level"></param>
        /// <returns></returns>
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
