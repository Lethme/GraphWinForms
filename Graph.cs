using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    /// <summary>
    /// Ребро графа
    /// </summary>
    public class GraphEdge
    {
        /// <summary>
        /// Связанная вершина
        /// </summary>
        public GraphVertex ConnectedVertex { get; }

        /// <summary>
        /// Вес ребра
        /// </summary>
        public int EdgeWeight { get; }

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
    }
    /// <summary>
    /// Вершина графа
    /// </summary>
    public class GraphVertex
    {
        /// <summary>
        /// Название вершины
        /// </summary>
        public string Name { get; }

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
        /// <param name="firstName">Имя первой вершины</param>
        /// <param name="secondName">Имя второй вершины</param>
        /// <param name="firstEdge">Ребро первой вершины</param>
        /// <param name="secondEdge">Ребро второй вершины</param>
        private void FindEdge(string firstName, string secondName, out GraphEdge firstEdge, out GraphEdge secondEdge)
        {
            foreach (var firstItem in FindVertex(firstName).Edges)
            {
                foreach (var secondItem in FindVertex(secondName).Edges)
                {
                    if (firstItem.ConnectedVertex.Name == secondName && secondItem.ConnectedVertex.Name == firstName)
                    {
                        firstEdge = firstItem;
                        secondEdge = secondItem;
                        return;
                    }
                }
            }

            firstEdge = null;
            secondEdge = null;
        }

        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="firstName">Имя первой вершины</param>
        /// <param name="secondName">Имя второй вершины</param>
        /// <param name="weight">Вес ребра соединяющего вершины</param>
        public void AddEdge(string firstName, string secondName, int weight)
        {
            var FirstVertex = FindVertex(firstName);
            var SecondVertex = FindVertex(secondName);
            if (SecondVertex != null && FirstVertex != null)
            {
                FirstVertex.AddEdge(SecondVertex, weight);
                SecondVertex.AddEdge(FirstVertex, weight);
            }
        }
        /// <summary>
        /// Удаление ребра графа
        /// </summary>
        /// <param name="firstName">Имя первой вершины</param>
        /// <param name="secondName">Имя второй вершины</param>
        public void RemoveEdge(string firstName, string secondName)
        {
            var FirstVertex = FindVertex(firstName);
            var SecondVertex = FindVertex(secondName);
            GraphEdge FirstEdge, SecondEdge;
            FindEdge(firstName, secondName, out FirstEdge, out SecondEdge);
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