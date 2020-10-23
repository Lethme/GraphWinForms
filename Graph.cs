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
    /// Ориентация ребра графа
    /// </summary>
    public enum EdgeOrientation
    {
        Single,
        Binary
    }
    /// <summary>
    /// Ребро графа
    /// </summary>
    public class GraphEdge
    {
        /// <summary>
        /// Связанная вершина
        /// </summary>
        public GraphVertex ConnectedVertex { get; private set; }
        /// <summary>
        /// Вес ребра
        /// </summary>
        public int EdgeWeight { get; private set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="connectedVertex">Связанная вершина</param>
        /// <param name="weight">Вес ребра</param>
        public GraphEdge(GraphVertex connectedVertex, int weight)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }
        /// <summary>
        /// Изменить смежную вершину
        /// </summary>
        /// <param name="vertex">Смежная вершина</param>
        public void SetVertex(GraphVertex vertex)
        {
            if (vertex != null) ConnectedVertex = vertex;
        }
        /// <summary>
        /// Изменить вес ребра
        /// </summary>
        /// <param name="weight">Вес ребра</param>
        public void SetWeight(int weight)
        {
            if (weight > 0) EdgeWeight = weight;
        }
    }
    /// <summary>
    /// Вершина графа
    /// </summary>
    public class GraphVertex
    {
        /// <summary>
        /// Название вершины
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Список ребер
        /// </summary>
        public List<GraphEdge> Edges { get; }
        /// <summary>
        /// List of vertices connected to current vertex
        /// </summary>
        public List<GraphVertex> ConnectedVertices 
        { 
            get
            {
                var List = new List<GraphVertex>();
                foreach (var edge in Edges)
                {
                    List.Add(edge.ConnectedVertex);
                }
                return List;
            }
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertexName">Название вершины</param>
        public GraphVertex(string vertexName)
        {
            Name = vertexName;
            Edges = new List<GraphEdge>();
        }
        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="newEdge">Ребро</param>
        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }
        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <param name="edgeWeight">Вес</param>
        public void AddEdge(GraphVertex vertex, int edgeWeight)
        {
            AddEdge(new GraphEdge(vertex, edgeWeight));
        }
        /// <summary>
        /// Удалить ребро
        /// </summary>
        /// <param name="edge">Ребро графа</param>
        public void RemoveEdge(GraphEdge edge)
        {
            Edges.Remove(edge);
        }
        /// <summary>
        /// Изменение имени вершины
        /// </summary>
        /// <param name="name">Имя вершины</param>
        public void SetName(string name)
        {
            if (name != null && name != string.Empty) Name = name;
        }
        /// <summary>
        /// Преобразование в строку
        /// </summary>
        /// <returns>Имя вершины</returns>
        public override string ToString() => Name;
    }
    /// <summary>
    /// Граф
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Список вершин графа
        /// </summary>
        public List<GraphVertex> Vertices { get; }
        /// <summary>
        /// Check if graph is empty
        /// </summary>
        public bool IsEmpty => Vertices.Count == 0;
        /// <summary>
        /// Конструктор
        /// </summary>
        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }
        /// <summary>
        /// Sort graph
        /// </summary>
        public void Sort()
        {
            Vertices.Sort((x, y) => x.Name.CompareTo(y.Name));
            foreach (var vertex in Vertices)
            {
                vertex.Edges.Sort((x, y) => x.ConnectedVertex.Name.CompareTo(y.ConnectedVertex.Name));
            }
        }
        /// <summary>
        /// Добавление вершины
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
            Sort();
        }
        /// <summary>
        /// Поиск вершины
        /// </summary>
        /// <param name="vertexName">Название вершины</param>
        /// <returns>Найденная вершина</returns>
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
        /// Поиск рёбер между двумя вершинами
        /// </summary>
        /// <param name="firstVertexName">Имя первой вершины</param>
        /// <param name="secondVertexName">Имя второй вершины</param>
        /// <param name="firstEdge">Ребро первой вершины</param>
        /// <param name="secondEdge">Ребро второй вершины</param>
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
        /// Добавление ребра
        /// </summary>
        /// <param name="firstVertexName">Имя первой вершины</param>
        /// <param name="secondVertexName">Имя второй вершины</param>
        /// <param name="weight">Вес ребра соединяющего вершины</param>
        public void AddEdge(string firstVertexName, string secondVertexName, int weight, EdgeOrientation orientation = EdgeOrientation.Binary)
        {
            var FirstVertex = FindVertex(firstVertexName);
            var SecondVertex = FindVertex(secondVertexName);
            if (SecondVertex != null && FirstVertex != null)
            {
                switch (orientation)
                {
                    case EdgeOrientation.Single:
                        {
                            FirstVertex.AddEdge(SecondVertex, weight);
                            break;
                        }
                    case EdgeOrientation.Binary:
                        {
                            FirstVertex.AddEdge(SecondVertex, weight);
                            SecondVertex.AddEdge(FirstVertex, weight);
                            break;
                        }
                }
                Sort();
            }
        }
        /// <summary>
        /// Изменение имени вершины
        /// </summary>
        /// <param name="currentName">Текущее имя вершины</param>
        /// <param name="newName">Новое имя вершины</param>
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
                Sort();
            }
        }
        /// <summary>
        /// Edit edge between two vertices
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <param name="weight">Edge weight</param>
        public void EditEdge(string firstVertexName, string secondVertexName, int weight)
        {
            GraphEdge FirstEdge, SecondEdge;
            FindEdge(firstVertexName, secondVertexName, out FirstEdge, out SecondEdge);
            if (FirstEdge != null) FirstEdge.SetWeight(weight);
            if (SecondEdge != null) SecondEdge.SetWeight(weight);
            Sort();
        }
        /// <summary>
        /// Удаление ребра графа
        /// </summary>
        /// <param name="firstVertexName">Имя первой вершины</param>
        /// <param name="secondVertexName">Имя второй вершины</param>
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
                Sort();
            }
        }
        /// <summary>
        /// Удаление вершины графа
        /// </summary>
        /// <param name="vertexName">Имя удаляемой вершины</param>
        public void RemoveVertex(string vertexName)
        {
            var vertex = FindVertex(vertexName);
            while (vertex.Edges.Count != 0)
            {
                RemoveEdge(vertex.Name, vertex.Edges[0].ConnectedVertex.Name);
            }
            Vertices.Remove(vertex);
            Sort();
        }
        /// <summary>
        /// Удаление всего графа
        /// </summary>
        public void Clear()
        {
            while (Vertices.Count != 0)
            {
                RemoveVertex(Vertices[0].Name);
            }
        }
        /// <summary>
        /// Кратчайший путь по алгоритму Дейкстры
        /// </summary>
        /// <param name="firstVertexName">Имя начальной вершины</param>
        /// <param name="secondVertexName">Имя конечной вершины</param>
        /// <returns>Объект, хранящий кратчайший путь и его вес</returns>
        public GraphPath FindShortestPath(string firstVertexName, string secondVertexName)
        {
            var deikstra = new Deikstra(this);
            return deikstra.FindShortestPath(firstVertexName, secondVertexName);
        }
        /// <summary>
        /// Allows to get list of vertices connected to stated vertex
        /// </summary>
        /// <param name="vertexName">Vertex name</param>
        /// <returns>List of connected vertices</returns>
        public List<GraphVertex> GetConnectedVertices(string vertexName)
        {
            var List = new List<GraphVertex>();
            var vertex = FindVertex(vertexName);
            if (vertex != null)
            {
                foreach (var edge in vertex.Edges)
                {
                    List.Add(edge.ConnectedVertex);
                }
            }
            return List;
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