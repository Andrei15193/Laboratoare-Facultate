using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace FamilyExpenses.ViewModels
{
	internal class TreeNode
	{
		private sealed class ChildrenCollection
			: ObservableCollection<TreeNode>
		{
			public ChildrenCollection(TreeNode parent)
			{
				if (parent == null)
					throw new ArgumentNullException("parent");
				_parent = parent;
			}

			protected override void InsertItem(int index, TreeNode item)
			{
				if (item == null)
					throw new ArgumentNullException("item");
				if (item.Parent == null)
				{
					TreeNode currentNode = _parent;

					while (currentNode != null && currentNode != item)
						currentNode = currentNode.Parent;

					if (currentNode != null)
						throw new ArgumentException("Cannot make circular reference in tree!", "item");
					item.Parent = _parent;
					base.InsertItem(index, item);
				}
				else
					if (item.Parent != _parent)
						throw new ArgumentException("The node is already a child of a different node!", "item");
			}
			protected override void SetItem(int index, TreeNode item)
			{
				if (item == null)
					throw new ArgumentNullException();
				if (item.Parent == null)
				{
					TreeNode currentNode = _parent;

					while (currentNode != null && currentNode != item)
						currentNode = currentNode.Parent;

					if (currentNode != null)
						throw new ArgumentException("Cannot make circular reference in tree!");
					item.Parent = _parent;
					base.SetItem(index, item);
				}
				else
					if (item.Parent != _parent)
						throw new ArgumentException("The node is already a child of a different node!");
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void ClearItems()
			{
				foreach (TreeNode child in this)
					child.Parent = null;
				base.ClearItems();
			}

			private readonly TreeNode _parent;
		}

		public TreeNode(string header)
		{
			if (header == null)
				throw new ArgumentNullException("header");
			if (string.IsNullOrEmpty(header) || string.IsNullOrWhiteSpace(header))
				throw new ArgumentException("Cannot be empty or whitespace only!", "header");
			_header = header.Trim();
			_children = new ChildrenCollection(this);
			Parent = null;
		}
		public string Header
		{
			get
			{
				return _header;
			}
		}
		public TreeNode Parent
		{
			get;
			private set;
		}
		public IList<TreeNode> Children
		{
			get
			{
				return _children;
			}
		}

		private readonly string _header;
		private readonly ObservableCollection<TreeNode> _children;
	}
}