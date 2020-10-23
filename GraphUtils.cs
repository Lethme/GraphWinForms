using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphWinForms
{
    /// <summary>
    /// Sorting type
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// Sorting by values ascending
        /// </summary>
        Ascending,
        /// <summary>
        /// Sorting by values descending
        /// </summary>
        Descending
    }
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
    /// Алгоритм Дейкстры
    /// </summary>
    public class Deikstra
    {
        /// <summary>
        /// Graph
        /// </summary>
        Graph graph;
        /// <summary>
        /// Vertices info
        /// </summary>
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
        public GraphPath FindShortestPath(string startName, string finishName)
        {
            return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
        }
        /// <summary>
        /// Поиск кратчайшего пути по вершинам
        /// </summary>
        /// <param name="startVertex">Стартовая вершина</param>
        /// <param name="finishVertex">Финишная вершина</param>
        /// <returns>Кратчайший путь</returns>
        public GraphPath FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
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

            return new GraphPath(GetPath(startVertex, finishVertex), GetVertexInfo(finishVertex).EdgesWeightSum);
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
        List<GraphVertex> GetPath(GraphVertex startVertex, GraphVertex endVertex)
        {
            var Path = new List<GraphVertex>();
            while (endVertex != null)
            {
                Path.Insert(0, endVertex);
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
        /// Converts vertex path to string path
        /// </summary>
        public List<string> StringPath
        { 
            get
            {
                return Path.Select(vertex => vertex.Name).ToList();
            }
        }
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
        public override string ToString()
        {
            var tempStr = String.Empty;
            foreach (var vertex in Path)
            {
                if (vertex == Path.Last())
                    tempStr += $"{vertex.Name}";
                else
                    tempStr += $"{vertex.Name} → ";
            }
            tempStr += $" {{ {PathWeight} }}";

            return tempStr;
        }
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
        public static void SortPathList(List<GraphPath> PathList, SortOrder Order = SortOrder.Ascending)
        {
            switch (Order)
            {
                case SortOrder.Ascending:
                    {
                        PathList.Sort((x, y) => x.PathWeight - y.PathWeight);
                        break;
                    }
                case SortOrder.Descending:
                    {
                        PathList.Sort((x, y) => y.PathWeight - x.PathWeight);
                        break;
                    }
            }
        }
        /// <summary>
        /// Find min 
        /// </summary>
        /// <param name="vertexName"></param>
        /// <param name="pathLength"></param>
        /// <returns></returns>
        public List<GraphPath> FindMinPath(int pathLength)
        {
            var MinPathsList = new List<GraphPath>();
            foreach (var vertex in Graph.Vertices)
            {
                var PathList = Graph.FindAllVertexPaths(vertex.Name, pathLength);
                SortPathList(PathList, SortOrder.Ascending);
                if (PathList.Count > 0)
                {
                    foreach (var minVertexPath in PathList.FindAll(path => path.PathWeight == PathList[0].PathWeight))
                    {
                        MinPathsList.Add(minVertexPath);
                    }
                }
            }

            if (MinPathsList.Count > 0)
            {
                SortPathList(MinPathsList, SortOrder.Ascending);
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