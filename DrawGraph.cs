using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graph;

namespace GraphWinForms
{
    public enum Tools
    {
        Cursor = 0,
        Vertex = 1,
        Edge = 2,
        Edit = 3,
        Delete = 4
    }
    public class Tool
    {
        public int CurrentTool { get; private set; }
        public Form1 FormHandler { get; private set; }
        public Tool(Form1 formHandler, int Tool = (int)Tools.Cursor)
        { 
            this.FormHandler = formHandler;
            SetTool(Tool);
        }
        public void SetTool(int Tool)
        {
            if (!Enum.IsDefined(typeof(Tools), (int)Tool))
                CurrentTool = (int)Tools.Cursor;
            CurrentTool = Tool;
            SelectTool();
        }
        public void LoseFocus()
        {
            FormHandler.ActiveControl = null;
        }
        private void SelectTool()
        {
            switch (CurrentTool)
            {
                case (int)Tools.Cursor:
                    {
                        FormHandler.cursorTool.Enabled = false;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        break;
                    }
                case (int)Tools.Vertex:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = false;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        break;
                    }
                case (int)Tools.Edge:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = false;
                        FormHandler.editTool.Enabled = true;
                        FormHandler.deleteTool.Enabled = true;
                        break;
                    }
                case (int)Tools.Edit:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.editTool.Enabled = false;
                        FormHandler.deleteTool.Enabled = true;
                        break;
                    }
                case (int)Tools.Delete:
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
    }
    static public class DefaultSettings
    {
        public const int VertexRadius = 20;
        public const int VertexRadiusExpansion = 30;
        public const int LabelWidthModifier = 5;
        public const int LabelHeightModifier = 2;
        public static Font Font { get; private set; } = new Font("Consolas", 9);
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
    }
    public static class DrawGraph
    {
        private static Graphics Graphics { get; set; }
        public static int CurrentNumber { get; private set; } = 1;
        public static List<Vertex> Vertices { get; private set; } = new List<Vertex>();
        public static List<Vertex> SelectedVertices { get; private set; } = new List<Vertex>();
        public static void SetGraphics(Graphics graphics)
        {
            if (graphics == null) throw new NullReferenceException();
            Graphics = graphics;
        }
        public static void RedrawSheet()
        {
            ClearSheet();
            foreach (var vertex in Vertices)
            {
                DrawVertex(vertex);
            }
        }
        private static void ClearSheet()
        {
            Graphics.Clear(Color.White);
        }
        public static void ClearGraph()
        {
            Vertices.Clear();
            SelectedVertices.Clear();
            CurrentNumber = 1;
            ClearSheet();
        }
        public static bool IsVertexClicked(int xPos, int yPos, int vertexRadiusExpansion = 0)
        {
            foreach (var vertex in Vertices)
            {
                if (Math.Pow(xPos - vertex.X, 2) + Math.Pow(yPos - vertex.Y, 2) <=
                    Math.Pow(DefaultSettings.VertexRadius + vertexRadiusExpansion, 2))
                    return true;
            }
            return false;
        }
        public static Vertex GetVertexOnClick(int xPos, int yPos)
        {
            foreach (var vertex in Vertices)
            {
                if (Math.Pow(xPos - vertex.X, 2) + Math.Pow(yPos - vertex.Y, 2) <=
                    Math.Pow(DefaultSettings.VertexRadius, 2))
                    return vertex;
            }
            return null;
        }
        public static bool IsVertexExist(string name)
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.Name == name) return true;
            }
            return false;
        }
        public static void AddVertex(int xPos, int yPos, string name = "")
        {
            if (!IsVertexClicked(xPos, yPos, DefaultSettings.VertexRadiusExpansion))
            {
                var Name = name == String.Empty ? CurrentNumber++.ToString() : name;
                if (!IsVertexExist(Name))
                {
                    Vertices.Add(new Vertex(xPos, yPos, Name));
                    RedrawSheet();
                }
            }
        }
        public static void EditVertex(int xPos, int yPos, int newX, int newY, int newRadius = DefaultSettings.VertexRadius, string newName = "")
        {
            var vertex = GetVertexOnClick(xPos, yPos);
            if (vertex != null)
            {
                if (!IsVertexClicked(newX, newY)) vertex.SetCoordinates(newX, newY);
                if (!IsVertexExist(newName)) vertex.SetName(newName);
                vertex.SetRadius(newRadius);
                RedrawSheet();
            }
        }
        public static void EditVertex(Vertex vertex, int newX, int newY, int newRadius = DefaultSettings.VertexRadius, string newName = "")
        {
            if (vertex != null)
            {
                if (!IsVertexClicked(newX, newY)) vertex.SetCoordinates(newX, newY);
                if (!IsVertexExist(newName)) vertex.SetName(newName);
                vertex.SetRadius(newRadius);
                RedrawSheet();
            }
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
            RedrawSheet();
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
            TextRenderer.DrawText(Graphics, vertex.Name, DefaultSettings.Font, rect, Color.Black,
                   TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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
