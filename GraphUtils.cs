using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphWinForms
{
    /// <summary>
    /// Vertex information
    /// </summary>
    public class GraphVertexInfo
    {
        /// <summary>
        /// Vertex
        /// </summary>
        public GraphVertex Vertex { get; set; }
        /// <summary>
        /// Check if vertex is already visited
        /// </summary>
        public bool IsUnvisited { get; set; }
        /// <summary>
        /// Edge weight sum
        /// </summary>
        public int EdgesWeightSum { get; set; }
        /// <summary>
        /// Previous vertex
        /// </summary>
        public GraphVertex PreviousVertex { get; set; }
        /// <summary>
        /// Vertex info constructor
        /// </summary>
        /// <param name="vertex">Vertex</param>
        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }
    /// <summary>
    /// Deikstra algoritm
    /// </summary>
    public class Deikstra
    {
        /// <summary>
        /// Graph
        /// </summary>
        private Graph Graph { get; set; }
        /// <summary>
        /// Vertices info
        /// </summary>
        private List<GraphVertexInfo> InfoList { get; set; }
        /// <summary>
        /// Deikstra constructor
        /// </summary>
        /// <param name="graph">Graph</param>
        public Deikstra(Graph graph)
        {
            if (graph == null) throw new NullReferenceException();
            this.Graph = graph;
        }
        /// <summary>
        /// Vertices info initialization
        /// </summary>
        private void InitInfo()
        {
            InfoList = new List<GraphVertexInfo>();
            foreach (var vertex in Graph.Vertices)
            {
                InfoList.Add(new GraphVertexInfo(vertex));
            }
        }
        /// <summary>
        /// Get vertex info
        /// </summary>
        /// <param name="vertex">Vertex</param>
        /// <returns>Vertex info</returns>
        private GraphVertexInfo GetVertexInfo(GraphVertex vertex)
        {
            foreach (var item in InfoList)
            {
                if (item.Vertex.Equals(vertex))
                {
                    return item;
                }
            }

            return null;
        }
        /// <summary>
        /// Find unvisited vertex with minimal weight sum
        /// </summary>
        /// <returns>Vertex info</returns>
        public GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in InfoList)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }
        /// <summary>
        /// Search a shortest path between two vertices stated by their names
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <returns>Shortest path</returns>
        public GraphPath FindShortestPath(string firstVertexName, string secondVertexName)
        {
            return FindShortestPath(Graph.FindVertex(firstVertexName), Graph.FindVertex(secondVertexName));
        }
        /// <summary>
        /// Search a shortest path between two vertices
        /// </summary>
        /// <param name="firstVertex">First vertex</param>
        /// <param name="secondVertex">Second vertex</param>
        /// <returns>Кратчайший путь</returns>
        public GraphPath FindShortestPath(GraphVertex firstVertex, GraphVertex secondVertex)
        {
            InitInfo();
            var first = GetVertexInfo(firstVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(secondVertex);
        }
        /// <summary>
        /// Calculate edge weight sum for the next vertex
        /// </summary>
        /// <param name="info">Current vertex info</param>
        private void SetSumToNextVertex(GraphVertexInfo info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetVertexInfo(e.ConnectedVertex);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }
        /// <summary>
        /// Path formation
        /// </summary>
        /// <param name="endVertex">End vertex</param>
        /// <returns>Path</returns>
        private GraphPath GetPath(GraphVertex endVertex)
        {
            var tempEndVertex = endVertex;
            var Path = new List<GraphVertex>();
            while (tempEndVertex != null)
            {
                Path.Insert(0, tempEndVertex);
                tempEndVertex = GetVertexInfo(tempEndVertex).PreviousVertex;
            }

            return new GraphPath(Path, GetVertexInfo(endVertex).EdgesWeightSum);
        }
    }
    /// <summary>
    /// Graph path
    /// </summary>
    public class GraphPath
    {
        /// <summary>
        /// List of path vertices
        /// </summary>
        public List<GraphVertex> Path { get; private set; }
        /// <summary>
        /// Converts vertex path to string path
        /// </summary>
        public List<string> StringPath { get { return Path.Select(vertex => vertex.Name).ToList(); } }
        /// <summary>
        /// Path length
        /// </summary>
        public int Length => Path.Count;
        /// <summary>
        /// Path weight
        /// </summary>
        public int PathWeight { get; private set; }
        /// <summary>
        /// Graph path constructor
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="pathWeight">Path weight</param>
        public GraphPath(List<GraphVertex> path, int pathWeight = 0)
        {
            if (path == null) throw new NullReferenceException();
            Path = path;
            PathWeight = pathWeight;
        }
        /// <summary>
        /// Add vertex to path
        /// </summary>
        /// <param name="vertex">Vertex</param>
        public void AddVertex(GraphVertex vertex)
        {
            if (vertex != null && !Path.Contains(vertex))
            {
                Path.Add(vertex);
            }
        }
        /// <summary>
        /// Set current path weight
        /// </summary>
        /// <param name="pathWeight">Path weight</param>
        public void SetPathWeight(int pathWeight)
        {
            if (pathWeight > 0) PathWeight = pathWeight;
        }
        /// <summary>
        /// Converts path to string
        /// </summary>
        /// <returns>String representation of path</returns>
        public override string ToString() => StringPath.Aggregate((v1, v2) => v1 + " → " + v2) + $" {{ {PathWeight} }}";
    }
    public class Path
    {
        /// <summary>
        /// Graph
        /// </summary>
        private Graph Graph { get; set; }
        /// <summary>
        /// Path constructor
        /// </summary>
        /// <param name="graph">Graph</param>
        public Path(Graph graph)
        {
            if (graph == null) throw new NullReferenceException();
            Graph = graph;
        }
        /// <summary>
        /// Sort path list by path weights
        /// </summary>
        /// <param name="PathList">Path list</param>
        /// <param name="Order">Sort order</param>
        public static void SortPathList(List<GraphPath> PathList, SortType Order = SortType.Ascending)
        {
            switch (Order)
            {
                case SortType.Ascending:
                    {
                        PathList.Sort((x, y) => x.PathWeight - y.PathWeight);
                        break;
                    }
                case SortType.Descending:
                    {
                        PathList.Sort((x, y) => y.PathWeight - x.PathWeight);
                        break;
                    }
            }
        }
        /// <summary>
        /// Find min path with stated length
        /// </summary>
        /// <param name="pathLength">Path length</param>
        /// <returns></returns>
        public List<GraphPath> FindMinPath(int pathLength)
        {
            var MinPathsList = new List<GraphPath>();
            foreach (var vertex in Graph.Vertices)
            {
                var PathList = Graph.FindAllVertexPaths(vertex.Name, pathLength);
                SortPathList(PathList, SortType.Ascending);
                if (PathList.Count > 0)
                {
                    MinPathsList.AddRange(PathList.FindAll(path => path.PathWeight == PathList[0].PathWeight));
                }
            }

            if (MinPathsList.Count > 0)
            {
                SortPathList(MinPathsList, SortType.Ascending);
                MinPathsList.RemoveAll(path => path.PathWeight != MinPathsList[0].PathWeight);
                return MinPathsList;
            }

            return null;
        }
        /// <summary>
        /// Find all possible paths with stated length from stated vertex
        /// </summary>
        /// <param name="firstVertex">Start vertex</param>
        /// <param name="visitedVertices">Just create new empty list</param>
        /// <param name="paths">Create new empty list before calling method</param>
        /// <param name="pathWeight">Path weight</param>
        private void DepthFirstSearch(GraphVertex firstVertex, List<GraphVertex> visitedVertices, List<GraphPath> paths, int pathWeight)
        {
            if (firstVertex == null) return;

            var tempList = visitedVertices.GetRange(0, visitedVertices.Count);
            tempList.Add(firstVertex);

            if (pathWeight == 1)
            {
                tempList.Add(firstVertex);
                paths.Add(new GraphPath(tempList));
                tempList.Remove(firstVertex);
            }

            foreach (var vertex in firstVertex.ConnectedVertices)
            {
                if (!tempList.Contains(vertex))
                {
                    DepthFirstSearch(vertex, tempList, paths, pathWeight - 1);
                }
            }
        }
        /// <summary>
        /// Find all possible paths with stated length from stated vertex
        /// </summary>
        /// <param name="vertexName">Start vertex</param>
        /// <param name="pathLength">Path length</param>
        /// <returns></returns>
        public List<GraphPath> DepthFirstSearch(string vertexName, int pathLength = 0)
        {
            var vertex = Graph.FindVertex(vertexName);
            if (vertex != null && pathLength > 1)
            {
                var PathList = new List<GraphPath>();
                DepthFirstSearch(vertex, new List<GraphVertex>(), PathList, pathLength);

                foreach (var path in PathList)
                {
                    for (var i = 0; i < path.Length - 1; i++)
                    {
                        path.SetPathWeight(path.PathWeight + Graph.GetEdgeWeight(path.Path[i].Name, path.Path[i + 1].Name));
                    }
                }
                
                return PathList;
            }

            return new List<GraphPath>();
        }
    }
}