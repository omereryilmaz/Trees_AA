using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesAA
{
    class Program
    {
        static void Main(string[] args)
        {
            AVL tree = new AVL();
            int N = 0;
            
            Console.Write("N degerini giriniz : ");
            N=int.Parse( Console.ReadLine());

            //Ağaca N sayısı kadar random deger ata
            Random r = new Random();
            for (int i = 0; i < N; i++)
            {
                tree.Add(r.Next(0,10000));
            }

            //Olusan agaci goster
            tree.DisplayTree();

            int s;
            Console.Write("Bulunacak deger : ");
            s=int.Parse( Console.ReadLine());
            tree.Find(s);
            Console.ReadKey();

        }
    }

    class AVL
    {
        class Node
        {
            public int data;
            public Node left;
            public Node right;
            public Node(int data)
            {
                this.data = data;
            }
        }
        Node root;

        public AVL()
        {
        }
        public void Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }

        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);//sola git
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);//sağa git
                current = balance_tree(current);
            }
            return current;
        }

        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }

        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int max(int l, int r)//returns maximum of two integers
        {
            return l > r ? l : r;
        }

        int sayici = 0;
       
        public void Find(int key)
        {
            if (Find(key, root).data == key)
            {
                Console.WriteLine("{0} was found!", key);
                Console.WriteLine("Yapilan karsilastirma : "+sayici);
            }
            else
            {
                Console.WriteLine("Nothing found!");
                Console.WriteLine("Yapilan karsilastirma : " + sayici);
            }
        }
        private Node Find(int target, Node current)
        {

            if (target < current.data)
            {
                sayici++;
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                sayici++;
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }

        public void DisplayTree()
        {
            InOrderDisplayTree(root);
            Console.ReadLine();
        }
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplayTree(current.right);
            }
        }
    }
}
