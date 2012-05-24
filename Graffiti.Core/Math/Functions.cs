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

namespace Graffiti.Math
{
    public static class Functions
    {
        private const int NoiseTableSize = 512; // Only powers of two!
        private static readonly float[] NoiseProfile;

        static Functions()
        {
            var random = new Random(2012);

            NoiseProfile = new float[NoiseTableSize];
            for (int i = 0; i < NoiseTableSize; i += 32)
                NoiseProfile[i] = (float)random.NextDouble();

            for (int i = 0; i < NoiseTableSize; i++)
            {
                if (i % 32 == 0)
                    continue;
                var prev = (i / 32) * 32;
                var next = prev + 32;
                if (next >= NoiseTableSize)
                    next = 0;
                var lambda = (i % 32) / (float)32;
                NoiseProfile[i] = ((1f - lambda) * NoiseProfile[prev]) + (lambda * NoiseProfile[next]);
            }
        }

        private static float Value(float timeMilliSeconds, float phase, float frequency)
        {
            float val = ((phase + timeMilliSeconds) / frequency);
            return val - ((int)val);
        }

        public static float Linear(float time, float speed)
        {
            return time * speed;
        }

        public static float Sine(float timeMilliSeconds, float baseline, float amplitude, float phase, float frequency)
        {
            float value = Value(timeMilliSeconds, phase, frequency);
            value = (float)(2 * System.Math.PI * value);
            return (amplitude * (float)System.Math.Sin(value)) + baseline;
        }

        public static float Sawtooth(float timeMilliSeconds, float baseline, float amplitude, float phase, float frequency)
        {
            return amplitude * Value(timeMilliSeconds, phase, frequency) + baseline;
        }

        public static float Square(float time, float baseline, float amplitude, float phase, float frequency, float dutyCycle)
        {
            var value = Value(time, phase, frequency) < dutyCycle ? 1 : 0;
            return value * amplitude + baseline;
        }

        public static float Noise(float timeMilliSeconds, float baseline, float amplitude, float phase, float frequency)
        {
            float value = Value(timeMilliSeconds, phase, frequency);
            var a = (int)System.Math.Floor(value * NoiseTableSize);
            var b = (int)System.Math.Ceiling(value * NoiseTableSize);
            if (b >= NoiseTableSize)
                b = NoiseTableSize - 1;
            return (amplitude * ((1f - value) * NoiseProfile[a] + value * NoiseProfile[b])) + baseline;
        }
    }
}