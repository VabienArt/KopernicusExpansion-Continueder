﻿using UnityEngine;

namespace KopernicusExpansion
{
    namespace VertexHeightMap32
    {
        /// <summary>
        /// A heightmap PQSMod that can use the 3 remaining channels of the heightmap
        /// </summary>
        public class PQSMod_VertexHeightMap32 : PQSMod_VertexHeightMap
        {
            /// <summary>
            /// Which channels of the map should be used?
            /// </summary>
            public MapSO.MapDepth depth;

            public override void OnVertexBuildHeight(PQS.VertexBuildData data)
            {
                // Get the Color, not the Float-Value from the Map
                Color32 c = heightMap.GetPixelColor32(data.u, data.v);

                // Build the height from the Color
                double height = 0;
                if (depth == MapSO.MapDepth.Greyscale)
                {
                    height = c.r;
                }
                else if (depth == MapSO.MapDepth.HeightAlpha)
                {
                    height = c.r + c.a * 255d;
                    height *= 1d / 255d;
                }
                else if (depth == MapSO.MapDepth.RGB)
                {
                    height = c.r + c.g * 255d + c.b * (255d * 255d);
                    height *= 1d / (255d * 255d);
                }
                else if (depth == MapSO.MapDepth.RGBA)
                {
                    height = c.r + c.g * 255d + c.b * (255d * 255d) + c.a * (255d * 255d * 255d);
                    height *= 1d / (255d * 255d * 255d);
                }
                else
                {
                    return;
                }

                // Apply it
                data.vertHeight += heightMapOffset + heightMapDeformity * (height / 255d);
            }
        }
    }
}