using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graph;

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
                                var inputDialog = new InputDialog("Input vertex name", "Vertex name", InputDialog.DialogType.Text);
                                inputDialog.ShowDialog();
                                if (inputDialog.Line != String.Empty)
                                {
                                    DrawGraph.AddVertex(e.X, e.Y, inputDialog.Line);
                                    DrawGraph.RedrawSheet();
                                }
                                inputDialog.Dispose();
                            }
                            break;
                        }
                    case (int)DrawTools.Edge:
                        {
                            var vertex = DrawGraph.GetVertexByCoordinates(e.X, e.Y);
                            if (vertex != null) vertex.Select();
                            else if (DrawGraph.SelectedVertices.Count > 0) DrawGraph.RemoveSelection();
                            DrawGraph.RedrawSheet();

                            if (DrawGraph.SelectedVertices.Count == 2)
                            {
                                if (!DrawGraph.IsEdgeExist(DrawGraph.SelectedVertices[0].Name, DrawGraph.SelectedVertices[1].Name))
                                {
                                    var inputDialog = new InputDialog("Input edge weight", "Edge weight", InputDialog.DialogType.IntNumber);
                                    inputDialog.ShowDialog();
                                    if (inputDialog.Line != String.Empty)
                                    {
                                        DrawGraph.AddEdge(DrawGraph.SelectedVertices[0].Name, DrawGraph.SelectedVertices[1].Name, Int32.Parse(inputDialog.Line));
                                    }
                                    inputDialog.Dispose();
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
                                var inputDialog = new InputDialog("Change vertex name", "Vertex name", InputDialog.DialogType.Text);
                                inputDialog.ShowDialog();
                                if (inputDialog.Line != String.Empty)
                                {
                                    DrawGraph.EditVertex(vertex, inputDialog.Line);
                                    DrawGraph.RedrawSheet();
                                }
                                inputDialog.Dispose();
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
                            else if (DrawGraph.SelectedVertices.Count > 0) DrawGraph.RemoveSelection();
                            DrawGraph.RedrawSheet();

                            if (DrawGraph.SelectedVertices.Count == 2)
                            {
                                if (DrawGraph.IsEdgeExist(DrawGraph.SelectedVertices[0].Name, DrawGraph.SelectedVertices[1].Name))
                                {
                                    var inputDialog = new InputDialog("Change edge weight", "Edge weight", InputDialog.DialogType.IntNumber);
                                    inputDialog.ShowDialog();
                                    if (inputDialog.Line != String.Empty)
                                    {
                                        var firstVertex = DrawGraph.SelectedVertices[0];
                                        var secondVertex = DrawGraph.SelectedVertices[1];
                                        DrawGraph.EditEdge(firstVertex, secondVertex, Int32.Parse(inputDialog.Line));
                                    }
                                    inputDialog.Dispose();
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
                            else if (DrawGraph.SelectedVertices.Count > 0) DrawGraph.RemoveSelection();
                            DrawGraph.RedrawSheet();

                            if (DrawGraph.SelectedVertices.Count == 2)
                            {
                                if (DrawGraph.IsEdgeExist(DrawGraph.SelectedVertices[0].Name, DrawGraph.SelectedVertices[1].Name))
                                {
                                    if (Utils.Confirmation($"Are you really want to delete edge between " +
                                        $"'{DrawGraph.SelectedVertices[0].Name}' and '{DrawGraph.SelectedVertices[1].Name}' vertices?",
                                        "Delete Vertex"))
                                    {
                                        DrawGraph.DeleteEdge(DrawGraph.SelectedVertices[0], DrawGraph.SelectedVertices[1]);
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
            if (DrawGraph.SelectedVertices.Contains(this)) Unselect();
            else
            {
                DrawGraph.SelectedVertices.Add(this);
                DrawGraph.RedrawSheet();
            }
        }
        public void Unselect()
        {
            SetBorderColor(Color.Black);
            DrawGraph.SelectedVertices.Remove(this);
            DrawGraph.RedrawSheet();
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
            if (!firstVertex.Equals(secondVertex))
            {
                FirstVertex = firstVertex;
                SecondVertex = secondVertex;
            }
        }
        public bool ContainsVertex(Vertex vertex)
        {
            return FirstVertex.Equals(vertex) || SecondVertex.Equals(vertex);
        }
        public void UpdateVertex(Vertex vertex, string vertexName)
        {
            if (FirstVertex.Equals(vertex)) FirstVertex.SetName(vertexName);
            if (SecondVertex.Equals(vertex)) SecondVertex.SetName(vertexName);
        }
    }
    public static class DrawGraph
    {
        private static Graphics Graphics { get; set; }
        public static PictureBox PictureBox { get; private set; }
        public static Bitmap GraphBitmap { get; private set; }
        public static int CurrentNumber { get; private set; } = 1;
        public static List<Vertex> Vertices { get; private set; } = new List<Vertex>();
        public static List<Vertex> SelectedVertices { get; private set; } = new List<Vertex>();
        public static List<Edge> Edges { get; private set; } = new List<Edge>();
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
            foreach (var edge in Edges)
            {
                DrawEdge(edge);
            }
            foreach (var vertex in Vertices)
            {
                DrawVertex(vertex);
            }
            UpdateSheet();
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
            Vertices.Clear();
            SelectedVertices.Clear();
            Edges.Clear();
            CurrentNumber = 1;
            ClearSheet();
        }
        public static void SaveGraphAsImage()
        {
            if (PictureBox.Image != null)
            {
                var saveDialog = new SaveFileDialog();
                saveDialog.Title = "Save Graph As...";
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
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                saveDialog.Dispose();
            }
        }
        public static bool IsVertexExist(int xPos, int yPos, int vertexRadiusExpansion = 0)
        {
            foreach (var vertex in Vertices)
            {
                if (Math.Pow(xPos - vertex.X, 2) + Math.Pow(yPos - vertex.Y, 2) <=
                    Math.Pow(vertex.Radius + vertexRadiusExpansion, 2))
                    return true;
            }
            return false;
        }
        public static bool IsVertexExist(string vertexName)
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.Name == vertexName) return true;
            }
            return false;
        }
        public static bool IsEdgeExist(string firstVertexName, string secondVertexName)
        {
            if (firstVertexName != secondVertexName)
            {
                foreach (var edge in Edges)
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
            foreach (var vertex in Vertices)
            {
                if (Math.Pow(xPos - vertex.X, 2) + Math.Pow(yPos - vertex.Y, 2) <=
                    Math.Pow(vertex.Radius + vertexRadiusExpansion, 2))
                    return vertex;
            }
            return null;
        }
        public static Vertex GetVertexByName(string vertexName)
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.Name == vertexName) return vertex;
            }
            return null;
        }
        public static Edge GetEdgeByVerticesNames(string firstVertexName, string secondVertexName)
        {
            if (firstVertexName != secondVertexName)
            {
                foreach (var edge in Edges)
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
            if (!firstVertex.Equals(secondVertex))
            {
                foreach (var edge in Edges)
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
                var Name = name == String.Empty ? CurrentNumber++.ToString() : name;
                if (!IsVertexExist(Name))
                {
                    Vertices.Add(new Vertex(xPos, yPos, Name));
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
                    Edges.Add(new Edge(FirstVertex, SecondVertex, weight));
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
                    foreach (var edge in Edges)
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
                    foreach (var edge in Edges)
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
            if (edge != null) Edges.Remove(edge);
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
            foreach (var edge in Edges)
            {
                if (edge.ContainsVertex(vertex)) EdgesToDelete.Add(edge);
            }
            foreach (var edge in EdgesToDelete)
            {
                DeleteEdge(edge);
            }
            Vertices.Remove(vertex);
        }
        public static void SelectVertex(Vertex vertex)
        {
            vertex.Select();
        }
        public static void RemoveSelection()
        {
            foreach (var vertex in Vertices)
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
    }
}