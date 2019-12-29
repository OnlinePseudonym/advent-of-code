using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Models
{
    class Tree
    {
        private ITreeNode root;
        public Tree(IList<ITreeNode> nodes)
        {
            root = nodes.FirstOrDefault(x => x.Parent == null);
            BuildTree(nodes);
        }

        public ITreeNode Root
        {
            get { return root; }
        }

        public void BuildTree(IList<ITreeNode> nodes)
        {
            AddTreeNode(root, nodes);
        }

        public int GetTotalOrbits()
        {
            if (root == null)
            {
                return 0;
            }

            int totalOrbits = 0;
            int currDepth = 0;

            Queue<ITreeNode> queue = new Queue<ITreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                currDepth++;

                var queueSize = queue.Count;

                for (var i = 0; i < queueSize; i++)
                {
                    var current = queue.Dequeue();
                    if (current.Children.Any())
                    {
                        foreach (var child in current.Children)
                        {
                            totalOrbits += currDepth;
                            queue.Enqueue(child);
                        }
                    }
                }
            }

            return totalOrbits;
        }

        private static void AddTreeNode(ITreeNode node, IList<ITreeNode> nodes)
        {
            var children = nodes.Where(x => x.Parent == node).ToList();
            node.AddChildren(children);
            RemoveAssignedChildren(node, nodes);

            for (var i = 0; i < children.Count; i++)
            {
                AddTreeNode(children[i], nodes);
                if (nodes.Count == 0) break;
            }
        }

        private static void RemoveAssignedChildren(ITreeNode root, IList<ITreeNode> nodes)
        {
            foreach (var node in root.Children)
            {
                nodes.Remove(node);
            }
        }
    }
}
