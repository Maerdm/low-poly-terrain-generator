/*
 * Copyright (c) 2022 Matthias Erdmann & Edgar Scheiermann
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using LowPolyTerrainGenerator.Height.Strategies;

namespace LowPolyTerrainGenerator.Height {
    public class HeightStrategyFactory {

        /// <summary>
        /// Creates a new HeightStrategy according to the terrain options.
        /// </summary>
        /// <param name="options">Options regarding the terrain</param>
        public static HeightStrategy Create(TerrainOptions options) {
            HeightStrategy strategy;
            switch (options.heightSource) {
                case HeightStrategyType.Noise: 
                    strategy = new NoiseHeightStrategy(options.length, options.width, options.maximumHeight, options.scale);
                    break;
                case HeightStrategyType.HeightMap: 
                    strategy = new MapHeightStrategy(options.length, options.width, options.maximumHeight, options.heightMap);
                    break;
                case HeightStrategyType.Random:
                    strategy = new RandomHeightStrategy(options.length, options.width, options.maximumHeight);
                    break;
                case HeightStrategyType.Coordinate:
                default:
                    strategy = new CoordinateHeightStrategy(options.length, options.width, options.maximumHeight, options.scale);
                    break;
            }
            return strategy;
        }
    }
}
