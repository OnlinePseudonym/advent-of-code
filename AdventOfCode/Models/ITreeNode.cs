using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventOfCode.Models
{
    interface ITreeNode
    {
        string Name { get; set; }
        ITreeNode Parent { get; }
        ReadOnlyCollection<ITreeNode> Children { get; }
        void SetParent(ITreeNode node);
        void AddChild(ITreeNode node);
        void AddChildren(IList<ITreeNode> nodes);
    }
}
