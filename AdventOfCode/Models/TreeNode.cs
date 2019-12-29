using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AdventOfCode.Models
{
    class TreeNode : ITreeNode
    {
        private string _name;
        private ITreeNode _parent;
        private IList<ITreeNode> _children;

        public TreeNode(string name)
        {
            Name = name;
            _children = new List<ITreeNode>();
        }
        public TreeNode(string name, ITreeNode parent)
            : this(name)
        {
            _parent = parent;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public ITreeNode Parent
        {
            get
            {
                return _parent;
            }
        }

        public ReadOnlyCollection<ITreeNode> Children
        {
            get
            {
                return new ReadOnlyCollection<ITreeNode>(_children.OrderBy(x => x.Name).ToList());
            }
        }

        public void AddChild(ITreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("Cannot insert null value.");
            }

            _children.Add(node);
            node.SetParent(this);
        }

        public void AddChildren(IList<ITreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                AddChild(node);
            }
        }

        public virtual void SetParent(ITreeNode node)
        {
            _parent = node;
        }
    }
}
