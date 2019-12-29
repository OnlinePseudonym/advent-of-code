using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Models;

namespace AdventOfCode.Days
{
    static class Day6
    {
        public static void LogAnswerToConsole()
        {
            var input = Utilities.getInputStrings("day6.txt");
            var nodes = BuildTreeNodes(input);

            var startNode = nodes.Where(x => x.Name == "YOU").FirstOrDefault();
            var targetNode = nodes.Where(x => x.Name == "SAN").FirstOrDefault();

            var map = new OrbitalMap(nodes);

            int distance = map.GetOrbitsBetweenTwoObjects(startNode, targetNode); 

            int depth = map.GetTotalOrbits();
            Console.WriteLine(distance);
        }
        
        private static IList<ITreeNode> BuildTreeNodes(string[] input)
        {
            var nodes = new List<ITreeNode>();

            foreach (var orbitalRelationship in input)
            {
                var parentName = orbitalRelationship.Split(')')[0];
                var childName = orbitalRelationship.Split(')')[1];

                var parent = getOrCreateNode(parentName, nodes);
                var child = getOrCreateNode(childName, nodes);

                child.SetParent(parent);
            }

            return nodes;
        }

        private static ITreeNode getOrCreateNode(string name, IList<ITreeNode> nodes)
        {
            var node = nodes.FirstOrDefault(x => x.Name == name);

            if (node == null)
            {
                node = new TreeNode(name);
                nodes.Add(node);
            }

            return node;
        }
    }
}
