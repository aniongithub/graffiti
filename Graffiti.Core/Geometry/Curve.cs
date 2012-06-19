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
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Graffiti.Core.Geometry
{
    internal class Curve: ICurve
    {
        private readonly IList<Vector3> _points = new List<Vector3>();


        
        internal void Add(Vector3 point)
        {
            _points.Add(point);
        }

        #region ICurve Members

        public int Count
        {
            get { return _points.Count; }
        }

        public CurveType CurveType { get; internal set; }

        #endregion

        #region IEnumerable<Vector3> Members

        public IEnumerator<Vector3> GetEnumerator()
        {
            return _points.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}