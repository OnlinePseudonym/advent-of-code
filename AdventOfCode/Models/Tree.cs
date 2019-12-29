using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Models
{
    class Tree
    {
        private ITreeNode root;
        public Tree()
        {
        }

        public Tree(IList<ITreeNode> nodes)
        {
            BuildTree(nodes);
        }

        public ITreeNode Root
        {
            get { return root; }
        }

        public void BuildTree(IList<ITreeNode> nodes)
        {
            root = nodes.FirstOrDefault(x => x.Parent == null);
            AddTreeNode(root, nodes);
        }

        public int GetDepthOfNode(ITreeNode node)
        {
            if (node.Parent == null)
            {
                return 1;
            }

            return GetDepthOfNode(node.Parent) + 1;
        }

        public ITreeNode GetCommonAncestor(ITreeNode firstNode, ITreeNode secondNode)
        {
            var firstPath = GetPathToNode(firstNode, new List<ITreeNode>());
            var secondPath = GetPathToNode(secondNode, new List<ITreeNode>());

            ITreeNode commonAncestor = firstPath.Intersect(secondPath).FirstOrDefault();

            return commonAncestor;
        }

        private IList<ITreeNode> GetPathToNode(ITreeNode node, IList<ITreeNode> path)
        {
            path.Add(node);

            if (node == root)
            {
                return path;
            }

            return GetPathToNode(node.Parent, path);
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
