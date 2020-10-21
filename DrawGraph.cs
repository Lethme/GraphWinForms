using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Extension;

namespace GraphWinForms
{
    /// <summary>
    /// Represents drawing tools constants
    /// </summary>
    public enum DrawTools
    {
        Cursor,
        Vertex,
        Edge,
        Edit,
        Delete,
        Deikstra
    }
    /// <summary>
    /// Represents tool selector class
    /// </summary>
    public static class DrawTool
    {
        /// <summary>
        /// Current drawing tool
        /// </summary>
        public static DrawTools CurrentTool { get; private set; } = DrawTools.Cursor;
        /// <summary>
        /// Base application form handler
        /// </summary>
        public static Form1 FormHandler { get; private set; }
        /// <summary>
        /// Base application form handler setter method
        /// </summary>
        /// <param name="formHandler">Base application form handler</param>
        public static void SetFormHandler(Form1 formHandler)
        { 
            FormHandler = formHandler;
            SelectTool();
        }
        /// <summary>
        /// Sets drawing tool from drawing tools constants
        /// </summary>
        /// <param name="Tool">Drawing tool</param>
        public static void SetTool(DrawTools Tool)
        {
            if (!Enum.IsDefined(typeof(DrawTools), (int)Tool))
                CurrentTool = DrawTools.Cursor;
            CurrentTool = Tool;
            SelectTool();
        }
        /// <summary>
        /// Removes controls focus
        /// </summary>
        public static void LoseFocus()
        {
            FormHandler.ActiveControl = null;
        }
        /// <summary>
        /// Enables or disables tools controls in application form
        /// </summary>
        private static void SelectTool()
        {
            switch (CurrentTool)
            {
                case DrawTools.Cursor:
                    {
                        FormHandler.cursorTool.Enabled = false;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        FormHandler.deikstraTool.Enabled = true;
                        break;
                    }
                case DrawTools.Vertex:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = false;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        FormHandler.deikstraTool.Enabled = true;
                        break;
                    }
                case DrawTools.Edge:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = false;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        FormHandler.deikstraTool.Enabled = true;
                        break;
                    }
                case DrawTools.Edit:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = false;
                        FormHandler.deleteTool.Enabled = true;
                        FormHandler.deikstraTool.Enabled = true;
                        break;
                    }
                case DrawTools.Delete:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = false;
                        FormHandler.deikstraTool.Enabled = true;
                        break;
                    }
                case DrawTools.Deikstra:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        FormHandler.deikstraTool.Enabled = false;
                        break;
                    }
            }
            LoseFocus();
        }
        /// <summary>
        /// Basic mouse click handler for PictureBox control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BasicDrawHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (DrawTool.CurrentTool)
                {
                    case DrawTools.Cursor:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            MessageBox.Show(vertex == null ? "No vertex clicked" : vertex.Name);
                            break;
                        }
                    case DrawTools.Vertex:
                        {
                            if (!DrawGraph.IsVertexExist(e.X, e.Y, DefaultSettings.VertexRadiusExpansion))
                            {
                                using (var inputDialog = new InputDialog("Input vertex name", "Vertex name", InputDialog.DialogType.Text))
                                {
                                    inputDialog.ShowDialog();
                                    if (inputDialog.Line != String.Empty)
                                    {
                                        DrawGraph.AddVertex(e.X, e.Y, inputDialog.Line);
                                        DrawGraph.RedrawSheet();
                                    }
                                }
                            }
                            break;
                        }
                    case DrawTools.Edge:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            if (vertex != null) vertex.Select();
                            else if (DrawGraph.Graph.SelectedVertices.Count > 0) DrawGraph.RemoveSelection();
                            DrawGraph.RedrawSheet();

                            if (DrawGraph.Graph.SelectedVertices.Count == 2)
                            {
                                if (!DrawGraph.IsEdgeExist(DrawGraph.Graph.SelectedVertices[0].Name, DrawGraph.Graph.SelectedVertices[1].Name))
                                {
                                    using (var inputDialog = new InputDialog("Input edge weight", "Edge weight", InputDialog.DialogType.IntNumber))
                                    {
                                        inputDialog.ShowDialog();
                                        if (inputDialog.Line != String.Empty)
                                        {
                                            DrawGraph.AddEdge(DrawGraph.Graph.SelectedVertices[0].Name, DrawGraph.Graph.SelectedVertices[1].Name, Int32.Parse(inputDialog.Line));
                                        }
                                    }
                                }
                                DrawGraph.RemoveSelection();
                                DrawGraph.RedrawSheet();
                            }
                            break;
                        }
                    case DrawTools.Edit:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            if (vertex != null)
                            {
                                using (var inputDialog = new InputDialog("Change vertex name", "Vertex name", InputDialog.DialogType.Text))
                                {
                                    inputDialog.ShowDialog();
                                    if (inputDialog.Line != String.Empty)
                                    {
                                        DrawGraph.EditVertex(vertex, inputDialog.Line);
                                        DrawGraph.RedrawSheet();
                                    }
                                }
                            }
                            break;
                        }
                    case DrawTools.Delete:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            if (vertex != null)
                            {
                                if (Utils.Confirmation($"Are you really want to delete '{vertex.Name}' vertex?", "Delete Vertex"))
                                {
                                    DrawGraph.RemoveVertex(vertex);
                                    DrawGraph.RedrawSheet();
                                }
                            }
                            break;
                        }
                    case DrawTools.Deikstra:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            if (vertex != null) vertex.Select();
                            else if (DrawGraph.Graph.SelectedVertices.Count > 0) DrawGraph.RemoveSelection();
                            DrawGraph.RedrawSheet();

                            if (DrawGraph.Graph.SelectedVertices.Count == 2)
                            {
                                var firstVertexName = DrawGraph.Graph.SelectedVertices[0].Name;
                                var secondVertexName = DrawGraph.Graph.SelectedVertices[1].Name;
                                var result = DrawGraph.LocalGraph.FindShortestPath(firstVertexName, secondVertexName);

                                DrawGraph.HighlightPath(result.ShortestPath, Color.Tomato);
                                MessageBox.Show
                                (
                                    result.ToString(),
                                    "Shortest path with Deikstra Algorithm"
                                );
                                DrawGraph.UnHighlightPath();
                                DrawGraph.RemoveSelection();
                            }
                            break;
                        }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                switch (DrawTool.CurrentTool)
                {
                    case DrawTools.Cursor:
                        {
                            break;
                        }
                    case DrawTools.Vertex:
                        {
                            break;
                        }
                    case DrawTools.Edge:
                        {
                            break;
                        }
                    case DrawTools.Edit:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            if (vertex != null) vertex.Select();
                            else if (DrawGraph.Graph.SelectedVertices.Count > 0) DrawGraph.RemoveSelection();
                            DrawGraph.RedrawSheet();

                            if (DrawGraph.Graph.SelectedVertices.Count == 2)
                            {
                                if (DrawGraph.IsEdgeExist(DrawGraph.Graph.SelectedVertices[0].Name, DrawGraph.Graph.SelectedVertices[1].Name))
                                {
                                    using (var inputDialog = new InputDialog("Change edge weight", "Edge weight", InputDialog.DialogType.IntNumber))
                                    {
                                        inputDialog.ShowDialog();
                                        if (inputDialog.Line != String.Empty)
                                        {
                                            var firstVertex = DrawGraph.Graph.SelectedVertices[0];
                                            var secondVertex = DrawGraph.Graph.SelectedVertices[1];
                                            DrawGraph.EditEdge(firstVertex, secondVertex, Int32.Parse(inputDialog.Line));
                                        }
                                    }
                                }
                                DrawGraph.RemoveSelection();
                                DrawGraph.RedrawSheet();
                            }
                            break;
                        }
                    case DrawTools.Delete:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            if (vertex != null) vertex.Select();
                            else if (DrawGraph.Graph.SelectedVertices.Count > 0) DrawGraph.RemoveSelection();
                            DrawGraph.RedrawSheet();

                            if (DrawGraph.Graph.SelectedVertices.Count == 2)
                            {
                                if (DrawGraph.IsEdgeExist(DrawGraph.Graph.SelectedVertices[0].Name, DrawGraph.Graph.SelectedVertices[1].Name))
                                {
                                    if (Utils.Confirmation($"Are you really want to delete edge between " +
                                        $"'{DrawGraph.Graph.SelectedVertices[0].Name}' and '{DrawGraph.Graph.SelectedVertices[1].Name}' vertices?",
                                        "Delete Vertex"))
                                    {
                                        DrawGraph.RemoveEdge(DrawGraph.Graph.SelectedVertices[0], DrawGraph.Graph.SelectedVertices[1]);
                                    }
                                }
                                DrawGraph.RemoveSelection();
                                DrawGraph.RedrawSheet();
                            }
                            break;
                        }
                    case DrawTools.Deikstra:
                        {
                            break;
                        }
                }
            }
        }
    }
    /// <summary>
    /// Default variables class
    /// </summary>
    static public class DefaultSettings
    {
        /// <summary>
        /// Default vertex radius
        /// </summary>
        public const int VertexRadius = 20;
        /// <summary>
        /// Default vertex radius expansion
        /// </summary>
        public const int VertexRadiusExpansion = 100;
        /// <summary>
        /// Default label width modifier
        /// </summary>
        public const int LabelWidthModifier = 5;
        /// <summary>
        /// Default label height modifier
        /// </summary>
        public const int LabelHeightModifier = 2;
        /// <summary>
        /// Default color
        /// </summary>
        public static Color Color { get; private set; } = Color.Black;
        /// <summary>
        /// Default selection color
        /// </summary>
        public static Color SelectionColor { get; private set; } = Color.Red;
        /// <summary>
        /// Default font for vertex labels
        /// </summary>
        public static Font VertexFont { get; private set; } = new Font("Consolas", 9);
        /// <summary>
        /// Default font for edge labels
        /// </summary>
        public static Font EdgeFont { get; private set; } = new Font("Consolas", 12);
        /// <summary>
        /// Default string format for labels
        /// </summary>
        public static StringFormat StringFormat { get; private set; } = new StringFormat();
        /// <summary>
        /// Default variables initializator
        /// </summary>
        /// <remarks>
        /// You can use it for initializing data need to be stated after compiling process
        /// </remarks>
        public static void Initialize()
        {
            StringFormat.Alignment = StringAlignment.Center;
            StringFormat.LineAlignment = StringAlignment.Center;
        }
    }
    /// <summary>
    /// Graphic vertex class
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Y coordinate
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// Vertex name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Vertex radius
        /// </summary>
        public int Radius { get; private set; }
        /// <summary>
        /// Vertex border color
        /// </summary>
        public Color BorderColor { get; private set; } = DefaultSettings.Color;
        /// <summary>
        /// Vertex constructor
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="name">Vertex name</param>
        /// <param name="radius">Vertex radius</param>
        public Vertex(int x, int y, string name, int radius = DefaultSettings.VertexRadius)
        {
            this.X = x;
            this.Y = y;
            this.Name = name;
            this.Radius = radius;
        }
        /// <summary>
        /// Allows to change vertex border color
        /// </summary>
        /// <param name="borderColor">Border color</param>
        public void SetBorderColor(Color borderColor)
        {
            this.BorderColor = borderColor;
        }
        /// <summary>
        /// Allows to change vertex coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void SetCoordinates(int x, int y)
        {
            if (x >= 0) this.X = x;
            if (y >= 0) this.Y = y;
        }
        /// <summary>
        /// Allows to change vertex radius
        /// </summary>
        /// <param name="radius">Vertex radius</param>
        public void SetRadius(int radius)
        {
            if (radius >= 15 && radius <= 30) this.Radius = radius;
        }
        /// <summary>
        /// Allows to change vertex name
        /// </summary>
        /// <param name="name">Vertex name</param>
        public void SetName(string name)
        {
            if (name != String.Empty) this.Name = name;
        }
        /// <summary>
        /// Selects vertex
        /// </summary>
        public void Select()
        {
            SetBorderColor(DefaultSettings.SelectionColor);
            if (DrawGraph.Graph.SelectedVertices.Contains(this)) Unselect();
            else
            {
                DrawGraph.Graph.SelectedVertices.Add(this);
                DrawGraph.RedrawSheet();
            }
        }
        /// <summary>
        /// Unselects vertex
        /// </summary>
        public void Unselect()
        {
            SetBorderColor(DefaultSettings.Color);
            DrawGraph.Graph.SelectedVertices.Remove(this);
            DrawGraph.RedrawSheet();
        }
        /// <summary>
        /// Basic vertex comparison method
        /// </summary>
        /// <param name="vertex">Vertex to compare with</param>
        /// <returns></returns>
        public bool Equals(Vertex vertex)
        {
            if 
            (
                this.Name == vertex.Name &&
                this.Radius == vertex.Radius &&
                this.X == vertex.X &&
                this.Y == vertex.Y
            )
            {
                return true;
            }
            return false;
        }
        public override string ToString() => Name;
    }
    /// <summary>
    /// Graphic edge class
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// First vertex that current edge basing on
        /// </summary>
        public Vertex FirstVertex { get; private set; }
        /// <summary>
        /// Second vertex that current edge basing on
        /// </summary>
        public Vertex SecondVertex { get; private set; }
        /// <summary>
        /// Edge weight
        /// </summary>
        public int Weight { get; private set; }
        /// <summary>
        /// Edge color
        /// </summary>
        public Color Color { get; private set; } = DefaultSettings.Color;
        /// <summary>
        /// Edge constructor
        /// </summary>
        /// <param name="firstVertex">First vertex that current edge basing on</param>
        /// <param name="secondVertex">Second vertex that current edge basing on</param>
        /// <param name="weight">Edge weight</param>
        public Edge (Vertex firstVertex, Vertex secondVertex, int weight)
        {
            this.FirstVertex = firstVertex;
            this.SecondVertex = secondVertex;
            this.Weight = weight;
        }
        /// <summary>
        /// Allows to change edge weight
        /// </summary>
        /// <param name="weight">Edge weight</param>
        public void SetWeight(int weight)
        {
            if (weight >= 0) this.Weight = weight;
        }
        /// <summary>
        /// Allows to change edge vertices
        /// </summary>
        /// <param name="firstVertex">First vertex that current edge basing on</param>
        /// <param name="secondVertex">Second vertex that current edge basing onparam>
        public void SetVertices(Vertex firstVertex, Vertex secondVertex)
        {
            if (!Utils.Equals(firstVertex, secondVertex))
            {
                FirstVertex = firstVertex;
                SecondVertex = secondVertex;
            }
        }
        /// <summary>
        /// Allows to change edge color
        /// </summary>
        /// <param name="color">Color</param>
        public void SetColor(Color color)
        {
            Color = color;
        }
        /// <summary>
        /// Checking if current edge contains stated vertex
        /// </summary>
        /// <param name="vertex">Vertex to check</param>
        /// <returns>Returns <c>true</c> if current edge contains stated vertex and <c>false</c> otherwise</returns>
        public bool ContainsVertex(Vertex vertex)
        {
            return JsonConvert.SerializeObject(FirstVertex) == JsonConvert.SerializeObject(vertex) ||
                   JsonConvert.SerializeObject(SecondVertex) == JsonConvert.SerializeObject(vertex);
        }
        /// <summary>
        /// Updates vertex information in current edge
        /// </summary>
        /// <param name="vertex">Vertex to update</param>
        /// <param name="vertexName">Vertex name</param>
        public void UpdateVertex(Vertex vertex, string vertexName)
        {
            if (Utils.Equals(FirstVertex, vertex)) FirstVertex.SetName(vertexName);
            if (Utils.Equals(SecondVertex, vertex)) SecondVertex.SetName(vertexName);
        }
    }
    /// <summary>
    /// Graphic data class
    /// </summary>
    public class GraphData
    {
        /// <summary>
        /// Used if vertex name is not stated on creating vertex (it's not being used currently)
        /// </summary>
        public int CurrentNumber { get; set; } = 1;
        /// <summary>
        /// Vertices list
        /// </summary>
        public List<Vertex> Vertices { get; private set; } = new List<Vertex>();
        /// <summary>
        /// Selected vertices list
        /// </summary>
        public List<Vertex> SelectedVertices { get; private set; } = new List<Vertex>();
        /// <summary>
        /// Edges list
        /// </summary>
        public List<Edge> Edges { get; private set; } = new List<Edge>();
    }
    /// <summary>
    /// Graph drawing class
    /// </summary>
    public static class DrawGraph
    {
        /// <summary>
        /// Graphics unit handler
        /// </summary>
        private static Graphics Graphics { get; set; }
        /// <summary>
        /// PictureBox hangler
        /// </summary>
        public static PictureBox PictureBox { get; private set; }
        /// <summary>
        /// Bitmap for drawing
        /// </summary>
        public static Bitmap GraphBitmap { get; private set; }
        /// <summary>
        /// Graphic graph data object
        /// </summary>
        public static GraphData Graph { get; private set; } = new GraphData();
        /// <summary>
        /// Local graph data object
        /// </summary>
        public static Graph LocalGraph { get; private set; } = new Graph();
        /// <summary>
        /// Basic initialization method
        /// </summary>
        /// <param name="pictureBox">PictureBox handler</param>
        public static void CreateGraphics(PictureBox pictureBox)
        {
            if (pictureBox == null) throw new NullReferenceException();
            DefaultSettings.Initialize();
            PictureBox = pictureBox;
            GraphBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics = Graphics.FromImage(GraphBitmap);
            Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ClearSheet();
            pictureBox.MouseClick += new MouseEventHandler(DrawTool.BasicDrawHandler);
        }
        /// <summary>
        /// Redraws graph
        /// </summary>
        public static void RedrawSheet()
        {
            ClearSheet();
            foreach (var edge in Graph.Edges)
            {
                DrawEdge(edge);
            }
            foreach (var vertex in Graph.Vertices)
            {
                DrawVertex(vertex);
            }
            UpdateSheet();
        }
        /// <summary>
        /// Sets graphic graph data object
        /// </summary>
        /// <param name="graph"></param>
        private static void SetGraphData(GraphData graph)
        {
            if (graph != null)
            {
                ClearGraph();
                Graph = graph;
                InitializeLocalGraph();
            }
        }
        /// <summary>
        /// Builds local graph
        /// </summary>
        private static void InitializeLocalGraph()
        {
            foreach (var vertex in Graph.Vertices)
            {
                LocalGraph.AddVertex(vertex.Name);
            }
            foreach (var edge in Graph.Edges)
            {
                LocalGraph.AddEdge(edge.FirstVertex.Name, edge.SecondVertex.Name, edge.Weight);
            }
        }
        /// <summary>
        /// Clear bitmap
        /// </summary>
        private static void ClearSheet()
        {
            Graphics.Clear(Color.White);
            UpdateSheet();
        }
        /// <summary>
        /// Applies current bitmap
        /// </summary>
        private static void UpdateSheet()
        {
            PictureBox.Image = GraphBitmap;
        }
        /// <summary>
        /// Clear whole graph
        /// </summary>
        public static void ClearGraph()
        {
            Graph.Vertices.Clear();
            Graph.SelectedVertices.Clear();
            Graph.Edges.Clear();
            Graph.CurrentNumber = 1;
            LocalGraph.Clear();
            ClearSheet();
        }
        /// <summary>
        /// Graph savig type constants
        /// </summary>
        public enum SaveType
        {
            /// <summary>
            /// Saving methods will show all the dialogs
            /// </summary>
            Normal,
            /// <summary>
            /// Saving methods won't show any dialogs
            /// </summary>
            Silent
        }
        /// <summary>
        /// Graph loading type constants
        /// </summary>
        public enum LoadType
        {
            /// <summary>
            /// Loading methods will show all the dialogs
            /// </summary>
            Normal,
            /// <summary>
            /// Loading methods won't show any dialogs
            /// </summary>
            Silent
        }
        /// <summary>
        /// Represents saving graph as image method
        /// </summary>
        public static void SaveGraphAsImage()
        {
            if (PictureBox.Image != null)
            {
                if (Graph.SelectedVertices.Count > 0) RemoveSelection();
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Title = "Save graph as Image...";
                    saveDialog.OverwritePrompt = true;
                    saveDialog.CheckPathExists = true;
                    saveDialog.Filter = "Image Files(*.jpg)|*.jpg|Image Files(*.bmp)|*.bmp|Image Files(*.gif)|*.gif|Image Files(*.png)|*.png|All files (*.*)|*.*";
                    saveDialog.ShowHelp = true;
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            PictureBox.Image.Save(saveDialog.FileName);
                        }
                        catch
                        {
                            MessageBox.Show("An error occurred while saving an image", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Represents saving graph as json string with saving dialog method
        /// </summary>
        /// <param name="Type">Saving type</param>
        public static void SaveGraphAsGWFFile(SaveType Type = SaveType.Normal)
        {
            if (Graph.SelectedVertices.Count > 0) RemoveSelection();
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save graph as Graph Builder File...";
                saveDialog.OverwritePrompt = true;
                saveDialog.CheckPathExists = true;
                saveDialog.Filter = "Graph Builder Files(*.gwf)|*.gwf";
                saveDialog.ShowHelp = true;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveGraphAsGWFFile(saveDialog.FileName, Type);
                }
            }
        }
        /// <summary>
        /// Represents saving graph as json string without saving dialog method
        /// </summary>
        /// <param name="FileName">Path to file to save data</param>
        /// <param name="Type">Saving type</param>
        public static void SaveGraphAsGWFFile(string FileName, SaveType Type = SaveType.Normal)
        {
            try
            {
                using (var GraphFile = new StreamWriter(FileName))
                {
                    var JsonString = JsonConvert.SerializeObject(Graph);
                    GraphFile.Write(JsonString);
                    GraphFile.Close();
                }
            }
            catch
            {
                if (Type == SaveType.Normal)
                {
                    MessageBox.Show("An error occurred while saving a graph", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Represents loading graph from json string with loading dialog method
        /// </summary>
        /// <param name="Type">Loading type</param>
        public static void LoadGraphFromGWFFile(LoadType Type = LoadType.Normal)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Title = "Open Graph File...";
                openDialog.CheckPathExists = true;
                openDialog.Filter = "Graph Files(*.gwf)|*.gwf";
                openDialog.ShowHelp = true;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    var loadGraph = ReadJsonFromLocalFile(openDialog.FileName, Type);
                    if (loadGraph != null)
                    {
                        SetGraphData(loadGraph);
                        RedrawSheet();
                    }
                }
            }
        }
        /// <summary>
        /// Represents loading graph from json string without loading dialog method
        /// </summary>
        /// <param name="FileName">Path to file to load data</param>
        /// <param name="Type">Loading type</param>
        public static void LoadGraphFromGWFFile(string FileName, LoadType Type = LoadType.Normal)
        {
            var loadGraph = ReadJsonFromLocalFile(FileName, Type);
            if (loadGraph != null)
            {
                SetGraphData(loadGraph);
                RedrawSheet();
            }
        }
        /// <summary>
        /// Reads json string from stated file
        /// </summary>
        /// <param name="FileName">Path to file to read data</param>
        /// <param name="Type">Loading type</param>
        /// <returns>Graphic graph data object</returns>
        private static GraphData ReadJsonFromLocalFile(string FileName, LoadType Type = LoadType.Normal)
        {
            try
            {
                using (var GraphFile = new StreamReader(FileName))
                {
                    return JsonConvert.DeserializeObject<GraphData>(GraphFile.ReadToEnd());
                }
            }
            catch (Exception)
            {
                if (Type == LoadType.Normal)
                {
                    MessageBox.Show("An error occurred while opening Graph File", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
        }
        /// <summary>
        /// Check vertex existence by it's coordinates
        /// </summary>
        /// <param name="xPos">Vertex x coordinate</param>
        /// <param name="yPos">Vertex y cooddinate</param>
        /// <param name="vertexRadiusExpansion">Allows to extend searching area near any vertex</param>
        /// <returns>Returns <c>true</c> if any vertex exists under stated coordinates and <c>false</c> otherwise</returns>
        public static bool IsVertexExist(int xPos, int yPos, int vertexRadiusExpansion = 0)
        {
            foreach (var vertex in Graph.Vertices)
            {
                if (Math.Pow(xPos - vertex.X, 2) + Math.Pow(yPos - vertex.Y, 2) <=
                    Math.Pow(vertex.Radius + vertexRadiusExpansion, 2))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Check vertex existence by it's name
        /// </summary>
        /// <param name="vertexName">Vertex name</param>
        /// <returns>Returns <c>true</c> if vertex with the same name exists and <c>false</c> otherwise</returns>
        public static bool IsVertexExist(string vertexName)
        {
            foreach (var vertex in Graph.Vertices)
            {
                if (vertex.Name == vertexName) return true;
            }
            return false;
        }
        /// <summary>
        /// Check edge between two vertices existence
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <returns>Returns <c>true</c> if edge between two vertices stated by their names exists and <c>false</c> otherwise</returns>
        public static bool IsEdgeExist(string firstVertexName, string secondVertexName)
        {
            if (firstVertexName != secondVertexName)
            {
                foreach (var edge in Graph.Edges)
                {
                    if (edge.FirstVertex.Name == firstVertexName && edge.SecondVertex.Name == secondVertexName ||
                        edge.FirstVertex.Name == secondVertexName && edge.SecondVertex.Name == firstVertexName)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Allows to get vertex by it's coordinates
        /// </summary>
        /// <param name="xPos">Vertex x coordinate</param>
        /// <param name="yPos">Vertex y coordinate</param>
        /// <param name="vertexRadiusExpansion">Allows to extend searching area near any vertex</param>
        /// <returns>Returns vertex handler if vertex was found and <c>null</c> reference otherwise</returns>
        public static Vertex GetVertexByCoordinates(int xPos, int yPos, int vertexRadiusExpansion = 0)
        {
            foreach (var vertex in Graph.Vertices)
            {
                if (Math.Pow(xPos - vertex.X, 2) + Math.Pow(yPos - vertex.Y, 2) <=
                    Math.Pow(vertex.Radius + vertexRadiusExpansion, 2))
                    return vertex;
            }
            return null;
        }
        /// <summary>
        /// Allows to get vertex by it's name
        /// </summary>
        /// <param name="vertexName">Vertex name</param>
        /// <returns>Returns vertex handler if vertex was found and <c>null</c> reference otherwise</returns>
        public static Vertex GetVertexByName(string vertexName)
        {
            foreach (var vertex in Graph.Vertices)
            {
                if (vertex.Name == vertexName) return vertex;
            }
            return null;
        }
        /// <summary>
        /// Allows to get edge by it's vertex names
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <returns>Returns edge handler if edge was found and <c>null</c> reference otherwise</returns>
        public static Edge GetEdgeByVerticesNames(string firstVertexName, string secondVertexName)
        {
            if (firstVertexName != secondVertexName)
            {
                foreach (var edge in Graph.Edges)
                {
                    if (edge.FirstVertex.Name == firstVertexName && edge.SecondVertex.Name == secondVertexName ||
                        edge.FirstVertex.Name == secondVertexName && edge.SecondVertex.Name == firstVertexName)
                        return edge;
                }
            }
            return null;
        }
        /// <summary>
        /// Allows to get edge by it's vertices
        /// </summary>
        /// <param name="firstVertex">First vertex</param>
        /// <param name="secondVertex">Second vertex</param>
        /// <returns>Returns edge handler if edge was found and <c>null</c> reference otherwise</returns>
        public static Edge GetEdgeByVertices(Vertex firstVertex, Vertex secondVertex)
        {
            if (!Utils.Equals(firstVertex, secondVertex))
            {
                foreach (var edge in Graph.Edges)
                {
                    if (edge.FirstVertex.Equals(firstVertex) && edge.SecondVertex.Equals(secondVertex) ||
                        edge.FirstVertex.Equals(secondVertex) && edge.SecondVertex.Equals(firstVertex))
                        return edge;
                }
            }
            return null;
        }
        /// <summary>
        /// Adds a new vertex to graph
        /// </summary>
        /// <param name="xPos">Vertex x coordinate</param>
        /// <param name="yPos">Vertex y coordinate</param>
        /// <param name="name">Vertex name</param>
        public static void AddVertex(int xPos, int yPos, string name = "")
        {
            if (!IsVertexExist(xPos, yPos, DefaultSettings.VertexRadiusExpansion))
            {
                var Name = name == String.Empty ? Graph.CurrentNumber++.ToString() : name;
                if (!IsVertexExist(Name))
                {
                    Graph.Vertices.Add(new Vertex(xPos, yPos, Name));
                    LocalGraph.AddVertex(Name);
                }
            }
        }
        /// <summary>
        /// Adds a new edge to graph
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <param name="weight">Edge weight</param>
        public static void AddEdge(string firstVertexName, string secondVertexName, int weight)
        {
            if (firstVertexName != secondVertexName && weight > 0 && !IsEdgeExist(firstVertexName, secondVertexName))
            {
                var FirstVertex = GetVertexByName(firstVertexName);
                var SecondVertex = GetVertexByName(secondVertexName);
                if (FirstVertex != null && SecondVertex != null)
                {
                    Graph.Edges.Add(new Edge(FirstVertex, SecondVertex, weight));
                    LocalGraph.AddEdge(firstVertexName, secondVertexName, weight);
                }
            }
        }
        ////////////////////////////////////////////////
        // Have to add Editing methods to Graph class //
        ////////////////////////////////////////////////
        /// <summary>
        /// Edits vertex data
        /// </summary>
        /// <param name="xPos">Vertex x coordinate</param>
        /// <param name="yPos">Vertex y coordinate</param>
        /// <param name="newName">Vertex name</param>
        /// <param name="newX">New vertex x coordinate</param>
        /// <param name="newY">New vertex y coordinate</param>
        /// <param name="newRadius">Vertex radius</param>
        public static void EditVertex(int xPos, int yPos, string newName, int newX = -1, int newY = -1, int newRadius = DefaultSettings.VertexRadius)
        {
            var vertex = GetVertexByCoordinates(xPos, yPos);
            if (vertex != null)
            {
                if (!IsVertexExist(newX, newY, DefaultSettings.VertexRadiusExpansion)) vertex.SetCoordinates(newX, newY);
                if (!IsVertexExist(newName))
                {
                    foreach (var edge in Graph.Edges)
                    {
                        if (edge.ContainsVertex(vertex))
                        {
                            edge.UpdateVertex(vertex, newName);
                        }
                    }
                    vertex.SetName(newName);
                }
                vertex.SetRadius(newRadius);
            }
        }
        /// <summary>
        /// Edits vertex data
        /// </summary>
        /// <param name="vertex">Vertex</param>
        /// <param name="newName">Vertex name</param>
        /// <param name="newX">New vertex x coordinate</param>
        /// <param name="newY">New vertex y coordinate</param>
        /// <param name="newRadius">Vertex radius</param>
        public static void EditVertex(Vertex vertex, string newName, int newX = -1, int newY = -1, int newRadius = DefaultSettings.VertexRadius)
        {
            if (vertex != null)
            {
                if (!IsVertexExist(newX, newY, DefaultSettings.VertexRadiusExpansion)) vertex.SetCoordinates(newX, newY);
                if (!IsVertexExist(newName))
                {
                    foreach (var edge in Graph.Edges)
                    {
                        if (edge.ContainsVertex(vertex))
                        {
                            edge.UpdateVertex(vertex, newName);
                        }
                    }
                    vertex.SetName(newName);
                }
                vertex.SetRadius(newRadius);
            }
        }
        /// <summary>
        /// Edit edge data
        /// </summary>
        /// <param name="edge">Edge</param>
        /// <param name="weight">Edge weight</param>
        private static void EditEdge(Edge edge, int weight)
        {
            if (edge != null)
            {
                edge.SetWeight(weight);
            }
        }
        /// <summary>
        /// Edit edge data
        /// </summary>
        /// <param name="firstVertex">First vertex</param>
        /// <param name="secondVertex">Second vertex</param>
        /// <param name="weight">Edge weight</param>
        public static void EditEdge(Vertex firstVertex, Vertex secondVertex, int weight)
        {
            var Edge = GetEdgeByVertices(firstVertex, secondVertex);
            EditEdge(Edge, weight);
        }
        /// <summary>
        /// Edit edge data
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        /// <param name="weight">Edge weight</param>
        public static void EditEdge(string firstVertexName, string secondVertexName, int weight)
        {
            var Edge = GetEdgeByVerticesNames(firstVertexName, secondVertexName);
            EditEdge(Edge, weight);
        }
        /// <summary>
        /// Removes edge from graph
        /// </summary>
        /// <param name="edge">Edge</param>
        public static void RemoveEdge(Edge edge)
        {
            if (edge != null)
            {
                Graph.Edges.Remove(edge);
                LocalGraph.RemoveEdge(edge.FirstVertex.Name, edge.SecondVertex.Name);
            }
        }
        /// <summary>
        /// Removes edge from graph
        /// </summary>
        /// <param name="firstVertex">First vertex</param>
        /// <param name="secondVertex">Second vertex</param>
        public static void RemoveEdge(Vertex firstVertex, Vertex secondVertex)
        {
            var Edge = GetEdgeByVertices(firstVertex, secondVertex);
            RemoveEdge(Edge);
        }
        /// <summary>
        /// Removes edge from graph
        /// </summary>
        /// <param name="firstVertexName">First vertex name</param>
        /// <param name="secondVertexName">Second vertex name</param>
        public static void RemoveEdge(string firstVertexName, string secondVertexName)
        {
            var Edge = GetEdgeByVerticesNames(firstVertexName, secondVertexName);
            RemoveEdge(Edge);
        }
        /// <summary>
        /// Removes vertex from graph
        /// </summary>
        /// <param name="vertex">Vertex</param>
        public static void RemoveVertex(Vertex vertex)
        {
            var EdgesToDelete = new List<Edge>();
            foreach (var edge in Graph.Edges)
            {
                if (edge.ContainsVertex(vertex)) EdgesToDelete.Add(edge);
            }
            foreach (var edge in EdgesToDelete)
            {
                RemoveEdge(edge);
            }
            Graph.Vertices.Remove(vertex);
            LocalGraph.RemoveVertex(vertex.Name);
        }
        /// <summary>
        /// Selects stated vertex
        /// </summary>
        /// <param name="vertex">Vertex</param>
        public static void SelectVertex(Vertex vertex)
        {
            vertex.Select();
        }
        /// <summary>
        /// Unselects all selected vertices
        /// </summary>
        public static void RemoveSelection()
        {
            foreach (var vertex in Graph.Vertices)
            {
                vertex.Unselect();
            }
        }
        /// <summary>
        /// Sets borders color to stated list of vertices
        /// </summary>
        /// <param name="verticesNames">List of vertices</param>
        /// <param name="color">Border color</param>
        public static void HighlightPath(List<string> verticesNames, Color color)
        {
            if (verticesNames != null && verticesNames.Count > 1)
            {
                foreach (var vertexName in verticesNames)
                {
                    var vertex = GetVertexByName(vertexName);
                    if (vertex != null)
                    {
                        vertex.SetBorderColor(color);
                    }
                }
                for (var i = 0; i < verticesNames.Count - 1; i++)
                {
                    var Edge = GetEdgeByVerticesNames(verticesNames[i], verticesNames[i + 1]);
                    if (Edge != null)
                    {
                        Edge.SetColor(color);
                    }
                }

                RedrawSheet();
            }
        }
        /// <summary>
        /// Sets borders color of all vertives to default color
        /// </summary>
        public static void UnHighlightPath()
        {
            int count = 0;
            foreach (var vertex in Graph.Vertices)
            {
                if (vertex.BorderColor != DefaultSettings.Color)
                {
                    vertex.SetBorderColor(DefaultSettings.Color);
                    count++;
                }
            }
            foreach (var edge in Graph.Edges)
            {
                if (edge.Color != DefaultSettings.Color)
                {
                    edge.SetColor(DefaultSettings.Color);
                }
            }

            if (count > 0) RedrawSheet();
        }
        /// <summary>
        /// Draws stated vertex
        /// </summary>
        /// <param name="vertex">Vertex</param>
        private static void DrawVertex(Vertex vertex)
        {
            Graphics.FillEllipse
            (
                Brushes.White,
                vertex.X - vertex.Radius,
                vertex.Y - vertex.Radius,
                2 * vertex.Radius,
                2 * vertex.Radius
            );
            Graphics.DrawEllipse
            (
                new Pen(vertex.BorderColor, 2),
                vertex.X - vertex.Radius,
                vertex.Y - vertex.Radius,
                2 * vertex.Radius,
                2 * vertex.Radius
            );
            PointF point = new PointF
            (
                vertex.X - vertex.Radius / 2,
                vertex.Y - vertex.Radius / 2
            );

            var rect = new Rectangle
            (
                vertex.X - DefaultSettings.LabelWidthModifier * vertex.Radius / 2,
                vertex.Y - DefaultSettings.LabelHeightModifier * vertex.Radius / 2,
                DefaultSettings.LabelWidthModifier * vertex.Radius,
                DefaultSettings.LabelHeightModifier * vertex.Radius
            );
            Graphics.DrawString
            (
                vertex.Name,
                DefaultSettings.VertexFont,
                Brushes.Black,
                rect,
                DefaultSettings.StringFormat
            );
        }
        /// <summary>
        /// Draws stated edge
        /// </summary>
        /// <param name="edge">Edge</param>
        private static void DrawEdge(Edge edge)
        {
            Graphics.DrawLine
            (
                new Pen(edge.Color, 2),
                new Point(edge.FirstVertex.X, edge.FirstVertex.Y),
                new Point(edge.SecondVertex.X, edge.SecondVertex.Y)
            );

            var centerX = (edge.FirstVertex.X + edge.SecondVertex.X) / 2;
            var centerY = (edge.FirstVertex.Y + edge.SecondVertex.Y) / 2;
            var defaultWidth = (DefaultSettings.LabelWidthModifier + (edge.Weight.ToString().Length - DefaultSettings.LabelWidthModifier) + 1) * DefaultSettings.VertexRadius;
            var defaultHeight = DefaultSettings.LabelHeightModifier * DefaultSettings.VertexRadius;

            var rect = new Rectangle
            (
                centerX - defaultWidth / 4,
                centerY - defaultHeight / 4,
                defaultWidth / 2,
                defaultHeight / 2
            );
            Graphics.FillRectangle(Brushes.White, rect);
            Graphics.DrawString
            (
                edge.Weight.ToString(),
                DefaultSettings.EdgeFont,
                Brushes.Black,
                rect,
                DefaultSettings.StringFormat
            );
        }
    }
    public static class Utils
    {
        /// <summary>
        /// Allows to show basic Yes/No dialog
        /// </summary>
        /// <param name="ConfirmationText">Text shown within dialog</param>
        /// <param name="ConfirmationTitle">Text shown in dialog title</param>
        /// <param name="DefaultButton">Default selected dialog button</param>
        /// <returns>Returns <c>true</c> if user clicked Yes button and <c>false</c> otherwise</returns>
        public static bool Confirmation(string ConfirmationText, string ConfirmationTitle, MessageBoxDefaultButton DefaultButton = MessageBoxDefaultButton.Button1)
        {
            if (ConfirmationText == String.Empty || ConfirmationTitle == String.Empty)
                throw new ArgumentNullException();

            var ConfirmationResult = MessageBox.Show
            (
                ConfirmationText,
                ConfirmationTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                DefaultButton
            );

            if (ConfirmationResult == DialogResult.Yes) return true;
            return false;
        }
        /// <summary>
        /// Check if application has administrator rights
        /// </summary>
        /// <returns>Returns <c>true</c> if application has administrator right and <c>false</c> otherwise</returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        /// <summary>
        /// Associates application extension with files
        /// </summary>
        public static void AssociateExtension()
        {
            if (!FileAssociation.IsAssociated)
            {
                if (IsAdministrator())
                    FileAssociation.Associate("Graph Builder File", Application.ExecutablePath);
                else
                    MessageBox.Show
                    (
                        "Application has to be run with administrator rights to associate it's file extension with itself.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly
                    );
            }
        }
        /// <summary>
        /// Unassociates application extension with files
        /// </summary>
        public static void UnAssociateExtension()
        {
            if (FileAssociation.IsAssociated)
            {
                if (IsAdministrator())
                    FileAssociation.Remove();
                else
                {
                    MessageBox.Show
                    (
                        "Application has to be run with administrator rights to associate it's file extension with itself.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly
                    );
                }
            }
        }
        /// <summary>
        /// Represents equation checking method
        /// </summary>
        /// <param name="firstObject">First object</param>
        /// <param name="secondObject">Second object</param>
        /// <returns>Return <c>true</c> if objects are equal and <c>false</c> otherwise</returns>
        public static bool Equals(object firstObject, object secondObject)
        {
            return JsonConvert.SerializeObject(firstObject) == JsonConvert.SerializeObject(secondObject);
        }
    }
}