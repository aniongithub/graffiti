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

using System.Collections.Generic;
using Graffiti.Core.Brushes;

namespace Graffiti.Core.Text
{
    internal sealed class TextBrush : Brush, ITextBrush
    {
        private readonly IDualLayer _dualLayer = new DualLayer();
        protected override IEnumerable<ILayer> GetLayerEnumerable()
        {
            foreach (var layer in SubBrush)
            {
                layer.TexCoordChannel = 0;
                TextLayer.TexCoordChannel = 1;

                _dualLayer.Layer1 = layer;
                _dualLayer.Layer2 = TextLayer;
                
                yield return _dualLayer;
            }
        }

        internal ILayer TextLayer { get; set; }

        public IBrush SubBrush { get; set; }
    }
}