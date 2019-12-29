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

            var tree = createOrbitalMapTree(input);
            var depth = tree.GetTotalOrbits();

            Console.WriteLine(depth);
        }

        private static Tree createOrbitalMapTree(string[] input)
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

            return new Tree(nodes);
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
