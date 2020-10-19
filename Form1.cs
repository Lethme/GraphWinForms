using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graph;

namespace GraphWinForms
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Tool DrawTool;
        public Form1()
        {
            InitializeComponent();
            display.BackColor = Color.White;
            display.BorderStyle = BorderStyle.FixedSingle;

            DrawGraph.CreateGraphics(display);
            DrawTool = new Tool(this);

            this.Activated += delegate { DrawTool.LoseFocus(); };
        }
        private void display_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (DrawTool.CurrentTool)
                {
                    case (int)Tools.Cursor:
                        {
                            var vertex = DrawGraph.GetVertexOnClick(e.X, e.Y);
                            MessageBox.Show(vertex == null ? "No vertex clicked" : vertex.Name);
                            break;
                        }
                    case (int)Tools.Vertex:
                        {
                            DrawGraph.AddVertex(e.X, e.Y);
                            break;
                        }
                    case (int)Tools.Edge:
                        {
                            var vertex = DrawGraph.GetVertexOnClick(e.X, e.Y);
                            if (vertex != null) vertex.Select();
                            else if (DrawGraph.SelectedVertices.Count > 0) DrawGraph.RemoveSelection();

                            if (DrawGraph.SelectedVertices.Count == 2)
                            {
                                MessageBox.Show
                                (
                                    $"Edge from {DrawGraph.SelectedVertices[0]} to {DrawGraph.SelectedVertices[1]}"
                                );
                                DrawGraph.RemoveSelection();
                            }
                            break;
                        }
                    case (int)Tools.Edit:
                        {
                            var vertex = DrawGraph.GetVertexOnClick(e.X, e.Y);
                            if (vertex != null) DrawGraph.EditVertex(vertex, -1, -1, 30, "Just Testing");
                            break;
                        }
                    case (int)Tools.Delete:
                        {
                            break;
                        }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                switch (DrawTool.CurrentTool)
                {
                    case (int)Tools.Cursor:
                        {
                            var vertex = DrawGraph.GetVertexOnClick(e.X, e.Y, DefaultSettings.VertexRadiusExpansion);
                            MessageBox.Show(vertex == null ? "No vertex clicked" : vertex.Name);
                            break;
                        }
                    case (int)Tools.Vertex:
                        {
                            break;
                        }
                    case (int)Tools.Edge:
                        {
                            break;
                        }
                    case (int)Tools.Edit:
                        {
                            break;
                        }
                    case (int)Tools.Delete:
                        {
                            break;
                        }
                }
            }
        }

        private void cursorTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)Tools.Cursor);
        }

        private void vertexTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)Tools.Vertex);
        }

        private void edgeTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)Tools.Edge);
        }
        private void editTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)Tools.Edit);
        }

        private void deleteTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)Tools.Delete);
        }

        private void clearTool_Click_1(object sender, EventArgs e)
        {
            if (Utils.Confirmation("Are you really want to delete a whole graph?", "Delete graph"))
            {
                DrawGraph.ClearGraph();
            }
        }
    }
}
