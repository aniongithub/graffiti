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
using System.Linq;
using Graffiti.Math;

namespace Graffiti.Core.Animation
{
    public interface IKeyframes<T>: IEnumerable<KeyValuePair<float, T>>
    {
        float Min { get; }
        float Max { get; }
        int Count { get; }

        void Add(float time, T value);
    }

    internal static class KeyframesExtensions
    {
        private static float GetKeyframeTime<T>(IKeyframes<T> keyframes, float elapsedMilliseconds, Mode mode)
            where T: IComparable<T>
        {
            switch (mode)
            {
                case Mode.StopAtEnd:
                    return elapsedMilliseconds;

                case Mode.Loop:
                    return elapsedMilliseconds % (keyframes.Max - keyframes.Min);

                case Mode.PingPong:
                    var interval = keyframes.Max - keyframes.Min;
                    var loopTime = elapsedMilliseconds % interval;
                    return (((int) loopTime % 2) == 0) ? loopTime : interval - loopTime;

                default:
                    throw new NotSupportedException();
            }
        }

        private static void GetKeyframesAt<T>(this IKeyframes<T> keyframes, float keyframeTime, out float lowerTime, out T lowerBound, out float upperTime, out T upperBound)
            where T: IComparable<T>
        {
            int keyframeCount = keyframes.Count;

            if (keyframeCount < 1)
                throw new ArgumentOutOfRangeException();

            var first = keyframes.First();

            if (first.Key > keyframeTime)
            {
                lowerTime = upperTime = first.Key;
                lowerBound = upperBound = first.Value;

                return;
            }

            var prev = first;
            foreach (var kvp in keyframes)
            {
                if (kvp.Key > keyframeTime)
                {
                    lowerTime = prev.Key;
                    lowerBound = prev.Value;

                    upperTime = kvp.Key;
                    upperBound = kvp.Value;

                    return;
                }

                prev = kvp;
            }

            var last = keyframes.Last();
            lowerTime = upperTime = last.Key;
            lowerBound = upperBound = last.Value;
        }

        public static T GetValueAt<T, TInterpolator>(this IKeyframes<T> keyframes, TInterpolator interpolator, float elapsedMilliseconds, Mode mode)
            where T: struct, IComparable<T>
            where TInterpolator: IInterpolator<T>, new()
        {
            float lowerTime;
            float upperTime;
            T lowerBound;
            T upperBound;
            var time = GetKeyframeTime(keyframes, elapsedMilliseconds, mode);
            keyframes.GetKeyframesAt(elapsedMilliseconds, out lowerTime, out lowerBound, out upperTime, out upperBound);

            float lambda = 0.0f;
            {
                float diffTime = upperTime - lowerTime;
                if (diffTime > 0.0f)
                    lambda = (time - lowerTime) / diffTime;
            }

            return interpolator.Lerp(lowerBound, upperBound, lambda);
        }
    }
}