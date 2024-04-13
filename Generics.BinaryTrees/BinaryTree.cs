using System.Collections;

namespace Generics.BinaryTrees;

public class BinaryTree<T>
    where T : IComparable
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node()
        {
            Value = default(T);
        }
        public Node(T value)
        {
            Value = value;
        }
    }

    
    public Node<T> Value { get; set; }
    public Node<T> Left { get; set; }
    public Node<T> Right { get; set; }


    public BinaryTree()
    {
        Value = new Node<T>();
    }

    public void Add(T node)
    {
        if (Value == null)
        {
            Value = new Node<T>();
            Value.Value = node;
        }
    }
    private void Add(Node<T> node, T value)
    {
        if (node.Value > value)
    }
}