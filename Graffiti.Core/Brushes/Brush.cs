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

using System.Collections;
using System.Collections.Generic;

namespace Graffiti.Core.Brushes
{
    public class Brush : IBrush
    {
        protected readonly List<ILayer> _layers = new List<ILayer>();

        protected virtual IEnumerable<ILayer> GetLayerEnumerable()
        {
            return _layers;
        }

        public void Update(float elapsedMilliseconds)
        {
            foreach (var layer in this)
                layer.Update(elapsedMilliseconds);
        }

        public int IndexOf(ILayer item)
        {
            return _layers.IndexOf(item);
        }
        public void Insert(int index, ILayer item)
        {
            _layers.Insert(index, item);
        }
        public void RemoveAt(int index)
        {
            _layers.RemoveAt(index);
        }
        public ILayer this[int index]
        {
            get { return _layers[index]; }
            set { _layers[index] = value; }
        }
        public void Add(ILayer item)
        {
            _layers.Add(item);
        }
        public void Clear()
        {
            _layers.Clear();
        }
        public bool Contains(ILayer item)
        {
            return _layers.Contains(item);
        }
        public void CopyTo(ILayer[] array, int arrayIndex)
        {
            _layers.CopyTo(array, arrayIndex);
        }
        public int Count
        {
            get { return _layers.Count; }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public bool Remove(ILayer item)
        {
            return _layers.Remove(item);
        }
        public IEnumerator<ILayer> GetEnumerator()
        {
            return GetLayerEnumerable().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}