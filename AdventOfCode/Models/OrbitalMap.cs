using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Models
{
    class OrbitalMap : Tree
    {
        public OrbitalMap(IList<ITreeNode> nodes)
        {
            BuildTree(nodes);
        }

        public int GetTotalOrbits()
        {
            if (this.Root == null)
            {
                return 0;
            }

            int totalOrbits = 0;
            int currDepth = 0;

            Queue<ITreeNode> queue = new Queue<ITreeNode>();
            queue.Enqueue(this.Root);

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

        public int GetOrbitsBetweenTwoObjects(ITreeNode startNode, ITreeNode targetNode)
        {
            var commonAncestor = GetCommonAncestor(startNode, targetNode);

            int startDepth = GetDepthOfNode(startNode.Parent);
            int targetDepth = GetDepthOfNode(targetNode.Parent);
            int commonAncestorDepth = GetDepthOfNode(commonAncestor);

            return (startDepth - commonAncestorDepth) + (targetDepth - commonAncestorDepth);
        }
    }
}
