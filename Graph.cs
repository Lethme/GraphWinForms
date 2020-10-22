using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Конструктор
        /// </summary>
        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }
        /// <summary>
        /// Добавление вершины
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
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
            }
        }
        /// <summary>
        /// Изменение имени вершины
        /// </summary>
        /// <param name="currentName">Текущее имя вершины</param>
        /// <param name="newName">Новое имя вершины</param>
        public void EditVertex(string currentName, string newName)
        {
            FindVertex(currentName).SetName(newName);
            foreach (var vertex in Vertices)
            {
                foreach (var edge in vertex.Edges)
                {
                    if (edge.ConnectedVertex.Name == currentName) 
                        edge.SetVertex(FindVertex(newName));
                }
            }
        }
        public void EditEdge(string firstVertexName, string secondVertexName, int weight)
        {
            GraphEdge FirstEdge, SecondEdge;
            FindEdge(firstVertexName, secondVertexName, out FirstEdge, out SecondEdge);
            if (FirstEdge != null) FirstEdge.SetWeight(weight);
            if (SecondEdge != null) SecondEdge.SetWeight(weight);
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
            if (SecondVertex != null && FirstVertex != null && FirstEdge != null && SecondEdge != null)
            {
                FirstVertex.RemoveEdge(FirstEdge);
                SecondVertex.RemoveEdge(SecondEdge);
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
        public DeikstraResult FindShortestPath(string firstVertexName, string secondVertexName)
        {
            var deikstra = new Deikstra(this);
            return deikstra.FindShortestPath(firstVertexName, secondVertexName);
        }
    }
}