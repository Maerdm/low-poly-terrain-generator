using UnityEngine;
using LowPolyTerrainGenerator.Height;

namespace LowPolyTerrainGenerator.Objects {
    public class LowPolyTerrain {
        public Vector3[] Vertices { get; private set; }
        public GameObject GameObject { get; private set; }
        private int[] triangles;
        private static int polySize = 10;

        /// <summary>
        /// Method From generates a terrain in low poly style.
        /// </summary>
        /// <param name="options">Options regarding the terrain</param>
        public static LowPolyTerrain From(TerrainOptions options) {
            LowPolyTerrain terrain = new LowPolyTerrain();
            terrain.Vertices = new Vector3[(options.width + 1) * (options.length + 1)];
            terrain.triangles = new int[options.width * options.length * 2 * 3];
            terrain.GenerateVerticesAndTriangles(options);
            terrain.CreateGameObject(options);
            return terrain;
        }

        private void GenerateVerticesAndTriangles(TerrainOptions options) {
            int[,] heights = GenerateHeightMap(options);
            int baseIndex = 0;
            for (int i = 0; i <= options.length; i++) {
                for (int j = 0; j <= options.width; j++) {
                    Vertices[baseIndex] = new Vector3(polySize * j, heights[i, j], polySize * i);
                    baseIndex++;
                }
            }

            baseIndex = 0;
            int verticesIndex = 0;
            for (int i = 0; i < options.length; i++) {
                for (int j = 0; j < options.width; j++) {
                    triangles[baseIndex] = verticesIndex;
                    triangles[baseIndex + 1] = options.width + verticesIndex + 1;
                    triangles[baseIndex + 2] = verticesIndex + 1;

                    triangles[baseIndex + 3] = options.width + verticesIndex + 1;
                    triangles[baseIndex + 4] = options.width + verticesIndex + 2;
                    triangles[baseIndex + 5] = verticesIndex + 1;
                    baseIndex += 6;
                    verticesIndex++;
                }
                verticesIndex++;
            }
        }

        private void CreateGameObject(TerrainOptions options) {
            GameObject = new GameObject("Terrain");
            AddMeshRenderer(options);
            AddMesh();
        }

        private int[,] GenerateHeightMap(TerrainOptions options) {
            HeightStrategy strategy = HeightStrategyFactory.Create(options);
            return strategy.Generate();
        }

        private void AddMesh() {
            Mesh mesh = new Mesh();
            mesh.Clear();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mesh.vertices = Vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            MeshFilter meshFilter = GameObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;
            GameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
        }

        private void AddMeshRenderer(TerrainOptions options) {
            MeshRenderer meshRenderer = GameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = options.material;
        }
    }
}
