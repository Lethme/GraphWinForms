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
using Graph;
using Newtonsoft.Json;
using Extension;

namespace GraphWinForms
{
    public enum DrawTools
    {
        Cursor = 0,
        Vertex = 1,
        Edge = 2,
        Edit = 3,
        Delete = 4
    }
    public static class DrawTool
    {
        public static int CurrentTool { get; private set; } = (int)DrawTools.Cursor;
        public static Form1 FormHandler { get; private set; }
        public static void SetFormHandler(Form1 formHandler)
        { 
            FormHandler = formHandler;
            SelectTool();
        }
        public static void SetTool(int Tool)
        {
            if (!Enum.IsDefined(typeof(DrawTools), (int)Tool))
                CurrentTool = (int)DrawTools.Cursor;
            CurrentTool = Tool;
            SelectTool();
        }
        public static void LoseFocus()
        {
            FormHandler.ActiveControl = null;
        }
        private static void SelectTool()
        {
            switch (CurrentTool)
            {
                case (int)DrawTools.Cursor:
                    {
                        FormHandler.cursorTool.Enabled = false;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        break;
                    }
                case (int)DrawTools.Vertex:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = false;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        break;
                    }
                case (int)DrawTools.Edge:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = false;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        break;
                    }
                case (int)DrawTools.Edit:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = false;
                        FormHandler.deleteTool.Enabled = true;
                        break;
                    }
                case (int)DrawTools.Delete:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = false;
                        break;
                    }
            }
            LoseFocus();
        }
        public static void BasicDrawHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (DrawTool.CurrentTool)
                {
                    case (int)DrawTools.Cursor:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            MessageBox.Show(vertex == null ? "No vertex clicked" : vertex.Name);
                            break;
                        }
                    case (int)DrawTools.Vertex:
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
                    case (int)DrawTools.Edge:
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
                    case (int)DrawTools.Edit:
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
                    case (int)DrawTools.Delete:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            if (vertex != null)
                            {
                                if (Utils.Confirmation($"Are you really want to delete '{vertex.Name}' vertex?", "Delete Vertex"))
                                {
                                    DrawGraph.DeleteVertex(vertex);
                                    DrawGraph.RedrawSheet();
                                }
                            }
                            break;
                        }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                switch (DrawTool.CurrentTool)
                {
                    case (int)DrawTools.Cursor:
                        {
                            break;
                        }
                    case (int)DrawTools.Vertex:
                        {
                            break;
                        }
                    case (int)DrawTools.Edge:
                        {
                            break;
                        }
                    case (int)DrawTools.Edit:
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
                    case (int)DrawTools.Delete:
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
                                        DrawGraph.DeleteEdge(DrawGraph.Graph.SelectedVertices[0], DrawGraph.Graph.SelectedVertices[1]);
                                    }
                                }
                                DrawGraph.RemoveSelection();
                                DrawGraph.RedrawSheet();
                            }
                            break;
                        }
                }
            }
        }
    }
    static public class DefaultSettings
    {
        public const int VertexRadius = 20;
        public const int VertexRadiusExpansion = 100;
        public const int LabelWidthModifier = 5;
        public const int LabelHeightModifier = 2;
        public static Font VertexFont { get; private set; } = new Font("Consolas", 9);
        public static Font EdgeFont { get; private set; } = new Font("Consolas", 12);
        public static StringFormat StringFormat { get; private set; } = new StringFormat();
        public static void Initialize()
        {
            StringFormat.Alignment = StringAlignment.Center;
            StringFormat.LineAlignment = StringAlignment.Center;
        }
    }
    public class Vertex
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Name { get; private set; }
        public int Radius { get; private set; }
        public Color BorderColor { get; private set; } = Color.Black;
        public Vertex(int x, int y, string name, int radius = DefaultSettings.VertexRadius)
        {
            this.X = x;
            this.Y = y;
            this.Name = name;
            this.Radius = radius;
        }
        public void SetBorderColor(Color borderColor)
        {
            this.BorderColor = borderColor;
        }
        public void SetCoordinates(int x, int y)
        {
            if (x >= 0) this.X = x;
            if (y >= 0) this.Y = y;
        }
        public void SetRadius(int radius)
        {
            if (radius >= 15 && radius <= 30) this.Radius = radius;
        }
        public void SetName(string name)
        {
            if (name != String.Empty) this.Name = name;
        }
        public void Select()
        {
            SetBorderColor(Color.Red);
            if (DrawGraph.Graph.SelectedVertices.Contains(this)) Unselect();
            else
            {
                DrawGraph.Graph.SelectedVertices.Add(this);
                DrawGraph.RedrawSheet();
            }
        }
        public void Unselect()
        {
            SetBorderColor(Color.Black);
            DrawGraph.Graph.SelectedVertices.Remove(this);
            DrawGraph.RedrawSheet();
        }
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
    public class Edge
    {
        public Vertex FirstVertex { get; private set; }
        public Vertex SecondVertex { get; private set; }
        public int Weight { get; private set; }
        public Edge (Vertex firstVertex, Vertex secondVertex, int weight)
        {
            this.FirstVertex = firstVertex;
            this.SecondVertex = secondVertex;
            this.Weight = weight;
        }
        public void SetWeight(int weight)
        {
            if (weight >= 0) this.Weight = weight;
        }
        public void SetVertices(Vertex firstVertex, Vertex secondVertex)
        {
            if (!Utils.Equals(firstVertex, secondVertex))
            {
                FirstVertex = firstVertex;
                SecondVertex = secondVertex;
            }
        }
        public bool ContainsVertex(Vertex vertex)
        {
            return JsonConvert.SerializeObject(FirstVertex) == JsonConvert.SerializeObject(vertex) ||
                   JsonConvert.SerializeObject(SecondVertex) == JsonConvert.SerializeObject(vertex);
        }
        public void UpdateVertex(Vertex vertex, string vertexName)
        {
            if (Utils.Equals(FirstVertex, vertex)) FirstVertex.SetName(vertexName);
            if (Utils.Equals(SecondVertex, vertex)) SecondVertex.SetName(vertexName);
        }
    }
    public class GraphData
    {
        public int CurrentNumber { get; set; } = 1;
        public List<Vertex> Vertices { get; private set; } = new List<Vertex>();
        public List<Vertex> SelectedVertices { get; private set; } = new List<Vertex>();
        public List<Edge> Edges { get; private set; } = new List<Edge>();
    }
    public static class DrawGraph
    {
        private static Graphics Graphics { get; set; }
        public static PictureBox PictureBox { get; private set; }
        public static Bitmap GraphBitmap { get; private set; }
        public static GraphData Graph { get; private set; } = new GraphData();
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
        private static void SetGraphData(GraphData graph)
        {
            if (graph != null)
            {
                ClearGraph();
                Graph = graph;
            }
        }
        private static void ClearSheet()
        {
            Graphics.Clear(Color.White);
            UpdateSheet();
        }
        private static void UpdateSheet()
        {
            PictureBox.Image = GraphBitmap;
        }
        public static void ClearGraph()
        {
            Graph.Vertices.Clear();
            Graph.SelectedVertices.Clear();
            Graph.Edges.Clear();
            Graph.CurrentNumber = 1;
            ClearSheet();
        }
        public static void SaveGraphAsImage()
        {
            if (PictureBox.Image != null)
            {
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
                            MessageBox.Show("An error occurred while saving the image", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        public static void SaveGraphAsGWFFile()
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save graph as Graph Builder File...";
                saveDialog.OverwritePrompt = true;
                saveDialog.CheckPathExists = true;
                saveDialog.Filter = "Graph Builder Files(*.gwf)|*.gwf";
                saveDialog.ShowHelp = true;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var GraphFile = new StreamWriter(saveDialog.FileName))
                        {
                            var JsonString = JsonConvert.SerializeObject(Graph);
                            GraphFile.WriteLine(JsonString);
                            GraphFile.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred while saving graph", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public static void LoadGraphFromGWFFile()
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Title = "Open Graph File...";
                openDialog.CheckPathExists = true;
                openDialog.Filter = "Graph Files(*.gwf)|*.gwf";
                openDialog.ShowHelp = true;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    var loadGraph = ConvertJsonToGraphData(openDialog.FileName);
                    if (loadGraph != null)
                    {
                        SetGraphData(loadGraph);
                        RedrawSheet();
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while opening Graph File", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public static void LoadGraphFromGWFFile(string FileName)
        {
            var loadGraph = ConvertJsonToGraphData(FileName);
            if (loadGraph != null)
            {
                SetGraphData(loadGraph);
                RedrawSheet();
            }
            else
            {
                MessageBox.Show("An error occurred while opening Graph File", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static GraphData ConvertJsonToGraphData(string FileName)
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
                return null;
            }
        }
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
        public static bool IsVertexExist(string vertexName)
        {
            foreach (var vertex in Graph.Vertices)
            {
                if (vertex.Name == vertexName) return true;
            }
            return false;
        }
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
        public static Vertex GetVertexByName(string vertexName)
        {
            foreach (var vertex in Graph.Vertices)
            {
                if (vertex.Name == vertexName) return vertex;
            }
            return null;
        }
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
        public static void AddVertex(int xPos, int yPos, string name = "")
        {
            if (!IsVertexExist(xPos, yPos, DefaultSettings.VertexRadiusExpansion))
            {
                var Name = name == String.Empty ? Graph.CurrentNumber++.ToString() : name;
                if (!IsVertexExist(Name))
                {
                   Graph.Vertices.Add(new Vertex(xPos, yPos, Name));
                }
            }
        }
        public static void AddEdge(string firstVertexName, string secondVertexName, int weight)
        {
            if (firstVertexName != secondVertexName && weight > 0 && !IsEdgeExist(firstVertexName, secondVertexName))
            {
                var FirstVertex = GetVertexByName(firstVertexName);
                var SecondVertex = GetVertexByName(secondVertexName);
                if (FirstVertex != null && SecondVertex != null)
                {
                    Graph.Edges.Add(new Edge(FirstVertex, SecondVertex, weight));
                }
            }
        }
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
        private static void EditEdge(Edge edge, int weight)
        {
            if (edge != null)
            {
                edge.SetWeight(weight);
            }
        }
        public static void EditEdge(Vertex firstVertex, Vertex secondVertex, int weight)
        {
            var Edge = GetEdgeByVertices(firstVertex, secondVertex);
            EditEdge(Edge, weight);
        }
        public static void EditEdge(string firstVertexName, string secondVertexName, int weight)
        {
            var Edge = GetEdgeByVerticesNames(firstVertexName, secondVertexName);
            EditEdge(Edge, weight);
        }
        public static void DeleteEdge(Edge edge)
        {
            if (edge != null) Graph.Edges.Remove(edge);
        }
        public static void DeleteEdge(Vertex firstVertex, Vertex secondVertex)
        {
            var Edge = GetEdgeByVertices(firstVertex, secondVertex);
            DeleteEdge(Edge);
        }
        public static void DeleteEdge(string firstVertexName, string secondVertexName)
        {
            var Edge = GetEdgeByVerticesNames(firstVertexName, secondVertexName);
            DeleteEdge(Edge);
        }
        public static void DeleteVertex(Vertex vertex)
        {
            var EdgesToDelete = new List<Edge>();
            foreach (var edge in Graph.Edges)
            {
                if (edge.ContainsVertex(vertex)) EdgesToDelete.Add(edge);
            }
            foreach (var edge in EdgesToDelete)
            {
                DeleteEdge(edge);
            }
            Graph.Vertices.Remove(vertex);
        }
        public static void SelectVertex(Vertex vertex)
        {
            vertex.Select();
        }
        public static void RemoveSelection()
        {
            foreach (var vertex in Graph.Vertices)
            {
                vertex.Unselect();
            }
        }
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
                new Pen(vertex.BorderColor),
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
        private static void DrawEdge(Edge edge)
        {
            Graphics.DrawLine
            (
                new Pen(Color.Black, 2),
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
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
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
        public static bool Equals(object firstObject, object secondObject)
        {
            return JsonConvert.SerializeObject(firstObject) == JsonConvert.SerializeObject(secondObject);
        }
    }
}