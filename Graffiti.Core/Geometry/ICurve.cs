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
using Graffiti.Core.Math;
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Geometry
{
    public enum CurveType
    { 
        Polyline,
        CubicBezier
    }
    
    public interface ICurve: IEnumerable<Vector3>
    {
        int Count { get; }
        CurveType CurveType { get; }
    }

    public static class CurveExtensions
    {
        private static readonly Vector3Interpolator _interpolator = new Vector3Interpolator();
        
        public static IIndexedMesh CreateStrokeMesh(this ICurve curve, float strokeWidth, TexcoordGenerationMode uMappingMode = TexcoordGenerationMode.Unwrapped_Linear, TexcoordGenerationMode vMappingMode = TexcoordGenerationMode.Unwrapped_Linear)
        {
            // TODO: Continue here after Vector3Interpolator.CubicBezier is complete.
            throw new NotImplementedException();
        }
    }
}