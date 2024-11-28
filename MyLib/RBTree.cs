using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class RBTree<Tkey, Tval> where Tkey : IComparable
    {
        internal class TreeElement
        {
            public TreeElement? left;
            public TreeElement? right;
            public TreeElement? parent;
            public bool isRed;
            public Tkey key;
            public Tval value;
            public bool isNill;

            public TreeElement(Tkey key, Tval value)
            {
                this.value = value;
                this.key = key;
                this.right = null;
                this.left = null;
                isRed = true;
                isNill = false;
                parent = null;
            }

            internal TreeElement Left() => left;
            internal TreeElement Right() => right;
        }
        
        TreeElement? _root;
        TreeElement? _nill;
        Func<Tkey?, Tkey?, int>? comparator;
        int size;

        internal TreeElement? Root() => _root;
        internal TreeElement? Nill() => _nill;

        private int Compare(Tkey? first, Tkey? second)
        {
            if (first == null) throw new ArgumentNullException("first element is null");
            if (second == null) throw new ArgumentNullException("second element is null");
            return first.CompareTo(second);
        }

        public RBTree()
        {
            _nill = new TreeElement(default(Tkey), default(Tval));
            _nill.isRed = false;
            _nill.left = _nill;
            _nill.right = _nill;
            _nill.isNill = true;
            _root = _nill;
            comparator = Compare;
            size= 0;
        }

        private void RightRotate(TreeElement node)
        {
            TreeElement left = node.left;
            node.left = left.right;
            if (left.right != _nill)
            {
                left.right.parent = node;
            }
            left.parent = node.parent;
            if (node.parent == null) _root = left;
            else if (node == node.parent.right) node.parent.right = left;
            else node.parent.left = left;
            left.right = node;
            node.parent = left;
        }
        private void LeftRotate(TreeElement node)
        {
            TreeElement right = node.right;
            node.right = right.left;
            if (right.left != _nill)
            {
                right.left.parent = node;
            }
            right.parent = node.parent;
            if (node.parent == null) _root = right;
            else if (node == node.parent.left) node.parent.left = right;
            else node.parent.right = right;
            right.left = node;
            node.parent = right;
        }
        private void FixInsert(TreeElement node)
        {
            while (node != _root && node.parent.isRed)
            {
                if (node.parent == node.parent.parent.left)
                {
                    TreeElement uncle = node.parent.parent.right;
                    if (uncle.isRed)
                    {
                        node.parent.isRed = false;
                        uncle.isRed = false;
                        node.parent.parent.isRed = true;
                        node = node.parent.parent;
                    }
                    else
                    {
                        if (node == node.parent.right)
                        {
                            node = node.parent;
                            LeftRotate(node);
                        }
                        node.parent.isRed = false;
                        node.parent.parent.isRed = true;
                        RightRotate(node.parent.parent);
                    }
                }
                else
                {
                    TreeElement uncle = node.parent.parent.left;
                    if (uncle.isRed)
                    {
                        node.parent.isRed = false;
                        uncle.isRed = false;
                        node.parent.parent.isRed= true;
                        node = node.parent.parent;
                    }
                    else
                    {
                        if (node == node.parent.left)
                        {
                            node = node.parent;
                            RightRotate(node);
                        }
                        node.parent.isRed = false;
                        node.parent.parent.isRed = true;
                        LeftRotate(node.parent.parent);
                    }
                }
            }
            _root.isRed = false;
        }
        private TreeElement? Search(Tkey key)
        {
            TreeElement node = _root;
            while (node != _nill)
            {
                if (key.Equals(node.key)) return node;
                if (comparator(key,node.key) > 0) node = node.right;
                else node = node.left;
            }
            return null;
        }
        private TreeElement GetMin(TreeElement node)
        {
            while (node.left != _nill) {
                node = node.left;
            }
            return node;
        }
        private void FixAfterDel(TreeElement node)
        {
            while (node != _root && !node.isRed)
            {
                TreeElement brother;
                if (node.parent.left == node)
                {
                    brother = node.parent.right;
                    if (brother.isRed)
                    {
                        brother.isRed = false;
                        node.parent.isRed = true;
                        LeftRotate(node.parent);
                        brother = node.parent.right;
                    }
                    if (!brother.left.isRed && !brother.right.isRed)
                    {
                        brother.isRed = true;
                        node = node.parent;
                    }
                    else
                    {
                        if (!brother.right.isRed)
                        {
                            brother.left.isRed = false;
                            brother.isRed = true;
                            RightRotate(brother);
                            brother = node.parent.right;
                        }
                        brother.isRed = node.parent.isRed;
                        node.parent.isRed = false;
                        brother.right.isRed= false;
                        LeftRotate(node.parent);
                        node = _root;
                    }
                }
                else
                {
                    brother = node.parent.left;
                    if (brother.isRed)
                    {
                        brother.isRed = false;
                        node.parent.isRed = true;
                        RightRotate(node.parent);
                        brother = node.parent.left;
                    }
                    if (!brother.left.isRed && !brother.right.isRed)
                    {
                        RightRotate(node.parent);
                        brother = node.parent.left;
                    }
                    else
                    {
                        if (!brother.left.isRed)
                        {
                            brother.right.isRed = false;
                            brother.isRed = true;
                            LeftRotate(brother);
                            brother = node.parent.left;
                        }
                        brother.isRed = node.parent.isRed;
                        node.parent.isRed = false;
                        brother.left.isRed = false;
                        RightRotate(node.parent);
                        node = _root;
                    }
                }
            }
            node.isRed = false;
        }

        public void Insert(Tkey key, Tval value)
        {
            TreeElement node = new TreeElement(key, value);
            node.left = _nill;
            node.right = _nill;

            TreeElement parent = null;
            TreeElement current = _root;

            while (current != _nill) { 
                parent = current;
                if (comparator(key, current.key) == 0)
                {
                    current.value = value;
                    return;
                }
                if (comparator(key, current.key) < 0) current = current.left;
                else current = current.right;
            }

            node.parent = parent;

            if (parent == null)
            {
                node.isRed = false;
                _root = node;
                size++;
                return;
            }

            if (comparator(parent.key, key) > 0) parent.left = node;
            else parent.right = node;

            size++;
            if (parent.parent == null) return;
            FixInsert(node);
        }
        public bool IsContain(Tkey key)
        {
            TreeElement node = _root;
            while (node != _nill) { 
                if (key.Equals(node.key)) return true;
                if (comparator(key, node.key) > 0) node = node.right;
                else node = node.left;
            }
            return false;
        }
        public void Remove(Tkey key)
        {
            TreeElement nodeToDel = Search(key);
            bool colorOfDel = nodeToDel.isRed;
            if (nodeToDel == null) return;

            // Если лист
            if (nodeToDel.left == _nill && nodeToDel.right == _nill)
            {
                if (nodeToDel == _root) nodeToDel = _nill;
                else if (nodeToDel.parent.left == nodeToDel) nodeToDel.parent.left = _nill;
                else nodeToDel.parent.right = _nill;
                return;
            }

            TreeElement child = _nill;
            // Если 1 потомок
            if (nodeToDel.left != _nill && nodeToDel.right == _nill)
            {
                if (nodeToDel == _root) _root = _root.left;
                else if (nodeToDel.parent.left == nodeToDel) nodeToDel.parent.left = nodeToDel.left;
                else nodeToDel.parent.right = nodeToDel.left;
            }
            else if (nodeToDel.left == _nill && nodeToDel.right != _nill)
            {
                if (nodeToDel == _root) _root = _root.right;
                else if (nodeToDel.parent.left == nodeToDel) nodeToDel.parent.left = nodeToDel.right;
                else nodeToDel.parent.right = nodeToDel.right;
            }

            // Если 2
            else
            {
                TreeElement min = GetMin(nodeToDel.right);
                nodeToDel.key = min.key;
                nodeToDel.value = min.value;
                colorOfDel = min.isRed;
                if (min == _root) _root = min.right;
                else if (min.parent.right == min) min.parent.right = min.right;
                else min.parent.left = min.right;
                child = min.right;
            }
            size--;
            if (!colorOfDel) FixAfterDel(child);
        }
        public int Size() => size;
    }
}
