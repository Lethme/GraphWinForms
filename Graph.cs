using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GraphWinForms
{
    /// <summary>
    /// Graph edge
    /// </summary>
    public class GraphEdge
    {
        /// <summary>
        /// Vertex which current edge is connected to
        /// </summary>
        public GraphVertex ConnectedVertex { get; private set; }
        /// <summary>
        /// Edge weight
        /// </summary>
        public int EdgeWeight { get; private set; }
        /// <summary>
        /// Edge length
        /// </summary>
        public int EdgeLength { get; private set; }
        /// <summary>
        /// Edge constructor
        /// </summary>
        /// <param name="connectedVertex">Vertex which new edge will be connected to</param>
        /// <param name="weight">Edge weight</param>
        public GraphEdge(GraphVertex connectedVertex, int weight, int length)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
            EdgeLength = length;
        }
        /// <summary>
        /// Change connected vertex
        /// </summary>
        /// <param name="vertex">New vertex</param>
        public void SetVertex(GraphVertex vertex)
        {
            if (vertex != null) ConnectedVertex = vertex;
        }
        /// <summary>
        /// Change edge weight
        /// </summary>
        /// <param name="weight">New edge weight</param>
        public void SetWeight(int weight)
        {
            if (weight > 0) EdgeWeight = weight;
        }
        /// <summary>
        /// Change edge length
        /// </summary>
        /// <param name="length">New edge length</param>
        public void SetLength(int length)
        {
            if (length > 0) EdgeWeight = length;
        }
    }
    /// <summary>
    /// Graph vertex
    /// </summary>
    public class GraphVertex
    {
        /// <summary>
        /// Vertex name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// List of vertex edges
        /// </summary>
        public List<GraphEdge> Edges { get; }
        /// <summary>
        /// List of vertices which are near to current vertex
        /// </summary>
        public List<GraphVertex> ConnectedVertices { get { return Edges.Select(edge => edge.ConnectedVertex).ToList(); } }
        /// <summary>
        /// Vertex constructor
        /// </summary>
        /// <param name="vertexName">Vertex name</param>
        public GraphVertex(string vertexName)
        {
            Name = vertexName;
            Edges = new List<GraphEdge>();
        }
        /// <summary>
        /// Add new edge
        /// </summary>
        /// <param name="newEdge">New edge</param>
        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }
        /// <summary>
        /// Add new edge
        /// </summary>
        /// <param name="vertex">Vertex which new edge will be connected to</param>
        /// <param name="edgeWeight">Edge weight</param>
        /// <param name="edgeLength">Edge length</param>
        public void AddEdge(GraphVertex vertex, int edgeWeight, int edgeLength)
        {
            AddEdge(new GraphEdge(vertex, edgeWeight, edgeLength));
        }
        /// <summary>
        /// Remove edge
        /// </summary>
        /// <param name="edge">Edge</param>
        public void RemoveEdge(GraphEdge edge)
        {
            Edges.Remove(edge);
        }
        /// <summary>
        /// Remove edge
        /// </summary>
        /// <param name="connectedVertexName">Name of vertex which edge to be removed is connected to</param>
        public void RemoveEdge(string connectedVertexName)
        {
            Edges.RemoveAll(edge => edge.ConnectedVertex.Name == connectedVertexName);
        }
        /// <summary>
        /// Change vertex name
        /// </summary>
        /// <param name="name">New vertex name</param>
        public void SetName(string name)
        {
            if (name != null && name != String.Empty) Name = name;
        }
        /// <summary>
        /// Vertex to string converter
        /// </summary>
        /// <returns>Vertex name</returns>
        public override string ToString() => Name;
    }
    /// <summary>
    /// Graph
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// List of graph vertices
        /// </summary>
        public List<GraphVertex> Vertices { get; }
        /// <summary>
        /// Check if graph is empty
        /// </summary>
        public bool IsEmpty => Vertices.Count == 0;
        /// <summary>
        /// Graph constructor
        /// </summary>
        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }
        /// <summary>
        /// Sort graph
        /// </summary>
        /// <param name="sortType">Sort type</param>
        public void Sort(SortType sortType = SortType.Ascending)
        {
            switch (sortType)
            {
                case SortType.Ascending:
                    {
                        Vertices.Sort((x, y) => x.Name.CompareTo(y.Name));
                        foreach (var vertex in Vertices)
                        {
                            vertex.Edges.Sort((x, y) => x.ConnectedVertex.Name.CompareTo(y.ConnectedVertex.Name));
                        }
                        break;
                    }
                case SortType.Descending:
                    {
                        Vertices.Sort((x, y) => y.Name.CompareTo(x.Name));
                        foreach (var vertex in Vertices)
                        {
                            vertex.Edges.Sort((x, y) => y.ConnectedVertex.Name.CompareTo(x.ConnectedVertex.Name));
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// Add new vertex
        /// </summary>
        /// <param name="vertexName">New vertex name</param>
        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
            Sort(SortType.Ascending);
        }
        /// <summary>
        /// Get vertex by its name
        /// </summary>
        /// <param name="vertexName">Vertex name</param>
        /// <returns>Vertex or null reference if there is no vertex with the same name in the graph</returns>
        public GraphVertex FindVertex(string vertexName)
        {
            foreach (var v in Vertices)
            {
                if (v.Name.Equals(vertexName))
                {
                    return v;
                }
            }

            return null;
        }
        /// <summary>
        /// Find edge between two vertices
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <param name="firstEdge">Edge from the first vertex edge list</param>
        /// <param name="secondEdge">Edge from the second vertex edge list</param>
        private void FindEdge(string firstVertexName, string secondVertexName, out GraphEdge firstEdge, out GraphEdge secondEdge)
        {
            firstEdge = null;
            secondEdge = null;

            var firstVertex = FindVertex(firstVertexName);
            var secondVertex = FindVertex(secondVertexName);
            
            if (firstVertex != null && secondVertex != null)
            {
                foreach (var edge in firstVertex.Edges)
                {
                    if (edge.ConnectedVertex.Name == secondVertexName) firstEdge = edge;
                }
                foreach (var edge in secondVertex.Edges)
                {
                    if (edge.ConnectedVertex.Name == firstVertexName) secondEdge = edge;
                }
            }

            if (firstEdge == null && secondEdge != null)
            {
                var tempEdge = firstEdge;
                firstEdge = secondEdge;
                secondEdge = tempEdge;
            }
        }
        /// <summary>
        /// Get edge weight
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <returns>Edge weight</returns>
        public int GetEdgeWeight(string firstVertexName, string secondVertexName)
        {
            GraphEdge FirstEdge, SecondEdge;
            FindEdge(firstVertexName, secondVertexName, out FirstEdge, out SecondEdge);
            if (FirstEdge != null) return FirstEdge.EdgeWeight;
            if (SecondEdge != null) return SecondEdge.EdgeWeight;
            return 0;
        }
        /// <summary>
        /// Add new edge
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <param name="weight">Edge weight</param>
        /// <param name="length">Edge length</param>
        /// <param name="orientation">Edge orientation</param>
        public void AddEdge(string firstVertexName, string secondVertexName, int weight, int length, EdgeOrientation orientation = EdgeOrientation.Binary)
        {
            var FirstVertex = FindVertex(firstVertexName);
            var SecondVertex = FindVertex(secondVertexName);
            if (SecondVertex != null && FirstVertex != null)
            {
                switch (orientation)
                {
                    case EdgeOrientation.Single:
                        {
                            FirstVertex.AddEdge(SecondVertex, weight, length);
                            break;
                        }
                    case EdgeOrientation.Binary:
                        {
                            FirstVertex.AddEdge(SecondVertex, weight, length);
                            SecondVertex.AddEdge(FirstVertex, weight, length);
                            break;
                        }
                }
                Sort(SortType.Ascending);
            }
        }
        /// <summary>
        /// Edit vertex name
        /// </summary>
        /// <param name="currentName">Current vertex name</param>
        /// <param name="newName">New vertex name</param>
        public void EditVertex(string currentName, string newName)
        {
            var vert = FindVertex(currentName);
            foreach (var vertex in Vertices)
            {
                foreach (var edge in vertex.Edges)
                {
                    if (edge.ConnectedVertex.Name == currentName) 
                        edge.SetVertex(FindVertex(newName));
                }
            }
            if (vert != null)
            {
                vert.SetName(newName);
                Sort(SortType.Ascending);
            }
        }
        /// <summary>
        /// Edit edge between two vertices
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <param name="weight">Edge weight</param>
        /// <param name="length">Edge length</param>
        public void EditEdge(string firstVertexName, string secondVertexName, int weight, int length)
        {
            GraphEdge FirstEdge, SecondEdge;
            FindEdge(firstVertexName, secondVertexName, out FirstEdge, out SecondEdge);
            if (FirstEdge != null) { FirstEdge.SetWeight(weight); FirstEdge.SetLength(length); }
            if (SecondEdge != null) { SecondEdge.SetWeight(weight); SecondEdge.SetLength(length); }
            Sort();
        }
        /// <summary>
        /// Remove edge
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        public void RemoveEdge(string firstVertexName, string secondVertexName)
        {
            var FirstVertex = FindVertex(firstVertexName);
            var SecondVertex = FindVertex(secondVertexName);
            GraphEdge FirstEdge, SecondEdge;
            FindEdge(firstVertexName, secondVertexName, out FirstEdge, out SecondEdge);
            
            if (FirstVertex != null && SecondVertex != null)
            {
                if (FirstEdge != null) FirstVertex.RemoveEdge(FirstEdge);
                if (SecondEdge != null) SecondVertex.RemoveEdge(SecondEdge);
                Sort(SortType.Ascending);
            }
        }
        /// <summary>
        /// Remove vertex
        /// </summary>
        /// <param name="vertexName">Vertex name</param>
        public void RemoveVertex(string vertexName)
        {
            var vertex = FindVertex(vertexName);
            while (vertex.Edges.Count != 0)
            {
                RemoveEdge(vertex.Name, vertex.Edges[0].ConnectedVertex.Name);
            }
            Vertices.Remove(vertex);
            Sort(SortType.Ascending);
        }
        /// <summary>
        /// Remove graph
        /// </summary>
        public void Clear()
        {
            while (Vertices.Count != 0)
            {
                RemoveVertex(Vertices[0].Name);
            }
        }
        /// <summary>
        /// Deikstra algorithm's shortest path
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <returns>Shortest path</returns>
        public GraphPath FindShortestPath(string firstVertexName, string secondVertexName)
        {
            var deikstra = new Deikstra(this);
            return deikstra.FindShortestPath(firstVertexName, secondVertexName);
        }
        /// <summary>
        /// Find all the shortest paths from stated vertex to every other one
        /// </summary>
        /// <param name="vertexName">Vertex name</param>
        /// <returns>List of shortest paths</returns>
        public List<GraphPath> FindAllShortestPaths(string vertexName)
        {
            var vertex = FindVertex(vertexName);
            var shortestPaths = new List<GraphPath>();
            if (vertex != null)
            {
                var deikstra = new Deikstra(this);
                foreach (var v in Vertices)
                {
                    if (v != vertex) shortestPaths.Add(deikstra.FindShortestPath(vertex, v));
                }
            }
            return shortestPaths;
        }
        /// <summary>
        /// Allows to get list of vertices connected to stated vertex
        /// </summary>
        /// <param name="vertexName">Vertex name</param>
        /// <returns>List of connected vertices</returns>
        public List<GraphVertex> GetConnectedVertices(string vertexName)
        {
            var vertex = FindVertex(vertexName);
            if (vertex != null)
            {
                return vertex.ConnectedVertices;
            }
            return null;
        }
        /// <summary>
        /// Find all possible paths with stated length from stated vertex
        /// </summary>
        /// <param name="vertexName">Vertex name</param>
        /// <param name="pathLength">Path length</param>
        /// <returns>List of paths</returns>
        public List<GraphPath> FindAllVertexPaths(string vertexName, int pathLength = 0)
        {
            var Path = new Path(this);
            var PathList = Path.DepthFirstSearch(vertexName, pathLength);
            PathList.RemoveAll(path => path.Length != pathLength);

            return PathList;
        }
        /// <summary>
        /// Finds list of paths with minimal weight and stated length
        /// </summary>
        /// <param name="pathLength">Path length</param>
        /// <returns>list of paths with minimal weight and stated length</returns>
        public List<GraphPath> FindMinPath(int pathLength)
        {
            var Path = new Path(this);
            var PathList = Path.FindMinPath(pathLength);
            if (PathList != null && PathList.Count > 1) PathList.Sort((x, y) => x.Path[0].Name.CompareTo(y.Path[0].Name));
            return PathList;
        }
    }
}