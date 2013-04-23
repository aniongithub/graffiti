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
using System.Collections;

#if WINDOWS_PHONE_7
using Graffiti.Core.Collections;
#endif

using System.Collections.Generic;

namespace Graffiti.Core.Animation
{
    public sealed class Keyframes<T> : IKeyframes<T>
    {
        private float _min;
        private float _max;

        private readonly SortedDictionary<float, T> _keyframes = new SortedDictionary<float, T>();

        public float Min
        {
            get { return _min; }
        }
        public float Max
        {
            get { return _max; }
        }
        public int Count
        {
            get { return _keyframes.Count; }
        }
        public void Add(float time, T value)
        {
            _min = System.Math.Min(_min, time);
            _max = System.Math.Max(_max, time);

            _keyframes.Add(time, value);
        }

        public IEnumerator<KeyValuePair<float, T>> GetEnumerator()
        {
            return _keyframes.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}