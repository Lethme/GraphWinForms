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
        Clear = 3
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
                        FormHandler.clearTool.Enabled = true;
                        break;
                    }
                case (int)Tools.Vertex:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = false;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.clearTool.Enabled = true;
                        break;
                    }
                case (int)Tools.Edge:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = false;
                        FormHandler.clearTool.Enabled = true;
                        break;
                    }
                case (int)Tools.Clear:
                    {
                        FormHandler.cursorTool.Enabled = true;
                        FormHandler.vertexTool.Enabled = true;
                        FormHandler.edgeTool.Enabled = true;
                        FormHandler.clearTool.Enabled = false;
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
        public const int LabelWidth = 4 * VertexRadius;
        public const int LabelHeight = 2 * VertexRadius;
        public static Font Font { get; private set; } = new Font("Consolas", 9);
    }
    public class Vertex
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Name { get; private set; }
        public int Radius { get; private set; }
        public Vertex(int x, int y, string name, int radius = DefaultSettings.VertexRadius)
        {
            this.X = x;
            this.Y = y;
            this.Name = name;
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
    }
    public class DrawGraph
    {
        private Graphics Graphics { get; set; }
        public int CurrentNumber { get; private set; } = 1;
        public List<Vertex> Vertices { get; private set; }
        public DrawGraph(Graphics Graphics)
        {
            if (Graphics == null) throw new NullReferenceException();
            
            this.Graphics = Graphics;
            Vertices = new List<Vertex>();
        }
        private void Redraw()
        {
            Clear();
            foreach (var vertex in Vertices)
            {
                DrawVertex(vertex);
            }
        }
        private void Clear()
        {
            Graphics.Clear(Color.White);
        }
        public bool IsVertexClicked(int xPos, int yPos, int vertexRadiusExpansion = 0)
        {
            foreach (var vertex in Vertices)
            {
                if (Math.Pow(xPos - vertex.X, 2) + Math.Pow(yPos - vertex.Y, 2) <=
                    Math.Pow(DefaultSettings.VertexRadius + vertexRadiusExpansion, 2))
                    return true;
            }
            return false;
        }
        public void AddVertex(int xPos, int yPos, string name = "")
        {
            if (!IsVertexClicked(xPos, yPos, DefaultSettings.VertexRadiusExpansion))
            {
                var Name = name == String.Empty ? CurrentNumber++.ToString() : name;
                Vertices.Add(new Vertex(xPos, yPos, Name));
                Redraw();
            }
        }
        private void DrawVertex(Vertex vertex)
        {
            Graphics.FillEllipse
            (
                Brushes.White,
                vertex.X - DefaultSettings.VertexRadius,
                vertex.Y - DefaultSettings.VertexRadius,
                2 * DefaultSettings.VertexRadius,
                2 * DefaultSettings.VertexRadius
            );
            Graphics.DrawEllipse
            (
                Pens.Black,
                vertex.X - DefaultSettings.VertexRadius,
                vertex.Y - DefaultSettings.VertexRadius,
                2 * DefaultSettings.VertexRadius,
                2 * DefaultSettings.VertexRadius
            );
            PointF point = new PointF
            (
                vertex.X - DefaultSettings.VertexRadius / 2,
                vertex.Y - DefaultSettings.VertexRadius / 2
            );

            var rect = new Rectangle
            (
                vertex.X - DefaultSettings.LabelWidth / 2,
                vertex.Y - DefaultSettings.LabelHeight / 2,
                DefaultSettings.LabelWidth,
                DefaultSettings.LabelHeight
            );
            TextRenderer.DrawText(Graphics, vertex.Name, DefaultSettings.Font, rect, Color.Black,
                   TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
