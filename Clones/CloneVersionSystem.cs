using System.Collections;
using System.Collections.Generic;

namespace Clones
{
    public class Clone
    {
        public Stack Programs;
        public Stack Rollbacks;

        public Clone()
        {
            Programs = new Stack();
            Rollbacks = new Stack();
        }

        public Clone(Clone mainClone)
        {
            Programs = new Stack(mainClone.Programs);
            Rollbacks = new Stack(mainClone.Rollbacks);
        }
    }

    public class StackItem
    {
        public string Value;
        public StackItem First;

        public StackItem(string value, StackItem first)
        {
            Value = value;
            First = first;
        }
    }

    public class Stack
    {
        public StackItem Last;
        public int Count;

        public Stack(){}
        public Stack(Stack baseStack)
        {
            Last = baseStack.Last;
        }

        public void Push(string item)
        {
            Last = new StackItem(item, Last);
            Count++;
        }

        public string Pop()
        {
            var result = Last.Value;
            Last = Last.First;
            Count--;
            return result;
        }
    }

    public class CloneVersionSystem : ICloneVersionSystem
    {
        private List<Clone> listClone;

        public CloneVersionSystem()
        {
            listClone = new List<Clone>();
            listClone.Add(new Clone());
        }

        public string Execute(string query)
        {
            var str = query.Split(' ');
            var number = int.Parse(str[1]) - 1;
            return Commands(str, number);
        }

        public string Commands(string[] str, int numberClone)
        {
            var clone = listClone[numberClone];
            switch (str[0])
            {
                case "learn":
                    clone.Programs.Push(str[2]);
                    clone.Rollbacks.Last = null; 
                    break;
                case "rollback":
                    if (clone.Programs.Last == null) 
                        break;
                    var program = clone.Programs.Pop();
                    clone.Rollbacks.Push(program); 
                    break;
                case "relearn":
                    if (clone.Rollbacks.Last == null) 
                        break;
                    var rollback = clone.Rollbacks.Pop();
                    clone.Programs.Push(rollback); 
                    break;
                case "clone":
                    listClone.Add(new Clone(listClone[numberClone])); 
                    break;
                case "check":
                    if (clone.Programs.Last == null) return "basic";
                    return clone.Programs.Last.Value;
                default: 
                    return null;
            }
            return null;
        }
    }
}

