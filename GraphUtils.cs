using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphWinForms
{
    /// <summary>
    /// Информация о вершине
    /// </summary>
    public class GraphVertexInfo
    {
        /// <summary>
        /// Вершина
        /// </summary>
        public GraphVertex Vertex { get; set; }
        /// <summary>
        /// Не посещенная вершина
        /// </summary>
        public bool IsUnvisited { get; set; }
        /// <summary>
        /// Сумма весов ребер
        /// </summary>
        public int EdgesWeightSum { get; set; }
        /// <summary>
        /// Предыдущая вершина
        /// </summary>
        public GraphVertex PreviousVertex { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertex">Вершина</param>
        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }
    /// <summary>
    /// Класс для хранения результатов работы алгоритма Дейкстры
    /// </summary>
    public class DeikstraResult
    {
        /// <summary>
        /// Общий вес кратчайшего пути
        /// </summary>
        public int ShortestPathWeight { get; private set; }
        /// <summary>
        /// Список имён вершин, составляющих кратчайший путь
        /// </summary>
        public List<string> ShortestPath { get; private set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="shortestPath">Кратчайший путь</param>
        /// <param name="shortestPathWeight">Вес кратчайшего пути</param>
        public DeikstraResult(List<string> shortestPath, int shortestPathWeight)
        {
            ShortestPathWeight = shortestPathWeight;
            ShortestPath = shortestPath;
        }
        public override string ToString()
        {
            var Result = $"Shortest path from '{ShortestPath.First()}' to '{ShortestPath.Last()}':\n\n";
            foreach (var vertexName in ShortestPath)
            {
                Result += $"{vertexName}\n";
            }
            Result += $"\nPath Weight: {ShortestPathWeight}";
            
            return Result;
        }
    }
    /// <summary>
    /// Алгоритм Дейкстры
    /// </summary>
    public class Deikstra
    {
        Graph graph;
        List<GraphVertexInfo> infos;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="graph">Граф</param>
        public Deikstra(Graph graph)
        {
            if (graph == null) throw new NullReferenceException();
            this.graph = graph;
        }
        /// <summary>
        /// Инициализация информации
        /// </summary>
        void InitInfo()
        {
            infos = new List<GraphVertexInfo>();
            foreach (var vertex in graph.Vertices)
            {
                infos.Add(new GraphVertexInfo(vertex));
            }
        }
        /// <summary>
        /// Получение информации о вершине графа
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <returns>Информация о вершине</returns>
        GraphVertexInfo GetVertexInfo(GraphVertex vertex)
        {
            foreach (var item in infos)
            {
                if (item.Vertex.Equals(vertex))
                {
                    return item;
                }
            }

            return null;
        }
        /// <summary>
        /// Поиск непосещенной вершины с минимальным значением суммы
        /// </summary>
        /// <returns>Информация о вершине</returns>
        public GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in infos)
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
        /// Поиск кратчайшего пути по названиям вершин
        /// </summary>
        /// <param name="startName">Название стартовой вершины</param>
        /// <param name="finishName">Название финишной вершины</param>
        /// <returns>Кратчайший путь</returns>
        public DeikstraResult FindShortestPath(string startName, string finishName)
        {
            return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
        }
        /// <summary>
        /// Поиск кратчайшего пути по вершинам
        /// </summary>
        /// <param name="startVertex">Стартовая вершина</param>
        /// <param name="finishVertex">Финишная вершина</param>
        /// <returns>Кратчайший путь</returns>
        public DeikstraResult FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
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

            return new DeikstraResult(GetPath(startVertex, finishVertex), GetVertexInfo(finishVertex).EdgesWeightSum);
        }
        /// <summary>
        /// Вычисление суммы весов ребер для следующей вершины
        /// </summary>
        /// <param name="info">Информация о текущей вершине</param>
        void SetSumToNextVertex(GraphVertexInfo info)
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
        /// Формирование пути
        /// </summary>
        /// <param name="startVertex">Начальная вершина</param>
        /// <param name="endVertex">Конечная вершина</param>
        /// <returns>Путь</returns>
        List<string> GetPath(GraphVertex startVertex, GraphVertex endVertex)
        {
            var Path = new List<string>();
            while (endVertex != null)
            {
                Path.Insert(0, endVertex.ToString());
                endVertex = GetVertexInfo(endVertex).PreviousVertex;
            }

            return Path;
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
    }
    public class Path
    {
        private Graph Graph { get; set; }
        public Path(Graph graph)
        {
            if (graph == null) throw new NullReferenceException();
            Graph = graph;
        }
        public List<GraphPath> FindAllVertexPaths(string vertexName, int pathLength)
        {
            return new List<GraphPath>();
        }
    }
}