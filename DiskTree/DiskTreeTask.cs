using System.Collections.Generic;
using System;
using System.Linq;

namespace DiskTree
{
    public class DiskTreeTask
    {
        public class Root
        {
            public Dictionary<string, Root> NodeDictionary = new Dictionary<string, Root>();
            public string RootName;

            public Root(string name)
            {
                RootName = name;
            }

            public List<string> MakeConclusion(int step, List<string> list)
            {
                if (step >= 0) list.Add(new string(' ', step) + RootName);
                step++;
                foreach (var item in NodeDictionary
                                    .Values
                                    .OrderBy(root => root.RootName, StringComparer.Ordinal))
                    list = item.MakeConclusion(step, list);
                return list;
            }

            public Root GetFileDirection(string subRoot)
            {   
                if (!NodeDictionary.TryGetValue(subRoot, out Root node))
                    return NodeDictionary[subRoot] = new Root(subRoot);
                return node; 
            }
        }

        public static List<string> Solve(List<string> input)
        {
            var root = new Root("");
            foreach (var name in input)
            {
                string[] pathArray = name.Split('\\');
                Root node = root;
                foreach (var str in pathArray)
                    node = node.GetFileDirection(str);
            }

            return root.MakeConclusion(-1, new List<string>());
        }
    }
}

