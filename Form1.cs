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
        DrawGraph GraphHandler;
        Tool DrawTool;
        public Form1()
        {
            InitializeComponent();
            display.BackColor = Color.White;
            display.BorderStyle = BorderStyle.FixedSingle;

            graphics = display.CreateGraphics();
            GraphHandler = new DrawGraph(graphics);
            DrawTool = new Tool(this);

            this.Activated += delegate { DrawTool.LoseFocus(); };
        }
        private void display_MouseClick(object sender, MouseEventArgs e)
        {
            switch (DrawTool.CurrentTool)
            {
                case (int)Tools.Cursor:
                    {
                        MessageBox.Show(GraphHandler.IsVertexClicked(e.X, e.Y).ToString());
                        break;
                    }
                case (int)Tools.Vertex:
                    {
                        GraphHandler.AddVertex(e.X, e.Y);
                        break;
                    }
                case (int)Tools.Edge:
                    {
                        break;
                    }
                case (int)Tools.Clear:
                    {
                        break;
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

        private void clearTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)Tools.Clear);
        }
    }
}
