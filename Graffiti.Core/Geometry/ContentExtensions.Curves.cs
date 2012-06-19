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

using System.Text.RegularExpressions;
using Graffiti.Core.Geometry;
using Microsoft.Xna.Framework.Content;

using GraffitiCurve = Graffiti.Core.Geometry.Curve;

namespace Microsoft.Xna.Framework
{
    public static partial class ContentExtensions
    {
        private static readonly Regex _absoluteCubicBezierParser = new Regex(@"(M\s*(?<MoveAbsX>-?\d+\.?\d*)\s*,(?<MoveAbsY>-?\d+\.?\d*)){1}(C(\s*(?<CubicControl1AbsX>-?\d+\.?\d*)\s*,(?<CubicControl1AbsY>-?\d+\.?\d*))(\s*(?<CubicControl2AbsX>-?\d+\.?\d*)\s*,(?<CubicControl2AbsY>-?\d+\.?\d*))(\s*(?<CubicPt2AbsX>-?\d+\.?\d*)\s*,(?<CubicPt2AbsY>-?\d+\.?\d*)))*");
        
        public static ICurve ParsePathGeometry(this ContentManager content, string pathGeometry)
        {
            var match = _absoluteCubicBezierParser.Match(pathGeometry);
            if (!match.Success)
                throw new ContentLoadException(string.Format("Could not parse path geometry, unknown geometry/format\n{0}", pathGeometry));

            var result = new GraffitiCurve();
            result.Add(new Vector3(float.Parse(match.Groups["MoveAbsX"].Value), float.Parse(match.Groups["MoveAbsY"].Value), 0f));
            for (int i = 0; i < match.Groups["CubicPt2AbsX"].Captures.Count; i++)
            {
                result.Add(new Vector3(float.Parse(match.Groups["CubicControl1AbsX"].Captures[i].Value), float.Parse(match.Groups["CubicControl1AbsY"].Captures[i].Value), 0f));
                result.Add(new Vector3(float.Parse(match.Groups["CubicControl2AbsX"].Captures[i].Value), float.Parse(match.Groups["CubicControl2AbsY"].Captures[i].Value), 0f));
                result.Add(new Vector3(float.Parse(match.Groups["CubicPt2AbsX"].Captures[i].Value), float.Parse(match.Groups["CubicPt2AbsY"].Captures[i].Value), 0f));
            }
            result.CurveType = CurveType.CubicBezier;

            return result;
        }
    }
}