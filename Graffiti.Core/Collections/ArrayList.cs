#region License and Copyright Notice
// Copyright (c) 2010 Ananth Balasubramaniam
// All rights reserved.
// 
// The contents of this file are made available under the terms of the
// Eclipse Public License v1.0 (the "License") which accompanies this
// distribution, and is available at the following URL:
// http://www.opensource.org/licenses/eclipse-1.0.php
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either expressed or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// By using this software in any fashion, you are agreeing to be bound by the
// terms of the License.
#endregion

using System;
using System.Collections.Generic;

namespace Graffiti.Core.Collections
{
    internal sealed class ArrayList<T> : IList<T>
    {
        private T[] _items;
        private int _count;

        private void EnsureCapacity(int newCapacity)
        {
            if (newCapacity < _items.Length)
                return;

            Array.Resize(ref _items, newCapacity);
        }

        public ArrayList()
            : this(4)
        {
        }

        public ArrayList(int capacity)
        {
            _items = new T[capacity];
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _count; i++)
                if (_items[i].Equals(item))
                    return i;

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index > _count)
                throw new InvalidOperationException("Cannot insert past end of the ArrayList");

            if (index == Count)
            {
                Add(item);
                return;
            }

            EnsureCapacity(_count + 1);
            Array.Copy(_items, index, _items, index + 1, _count - index);

            _items[index] = item;
            _count++;
        }

        public void RemoveAt(int index)
        {
            if (_count == 0)
                throw new InvalidOperationException("Cannot delete item from empty list");

            if (index == Count)
            {
                _count--;
                return;
            }

            Array.Copy(_items, index + 1, _items, index, _count - index - 1);
            _count--;
        }

        public T this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public void Add(T item)
        {
            EnsureCapacity(_count + 1);
            _count++;
            _items[_count - 1] = item;
        }

        public void Clear()
        {
            _count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (_items as IEnumerable<T>).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public T[] GetArray()
        {
            return _items;
        }
    }
}