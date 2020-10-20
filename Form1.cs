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
        public Form1(string[] args)
        {
            InitializeComponent();
            display.BackColor = Color.White;
            display.BorderStyle = BorderStyle.FixedSingle;

            var ToolTip = new ToolTip();
            ToolTip.SetToolTip(cursorTool, "Basic tool");
            ToolTip.SetToolTip(vertexTool, "Vetex building tool");
            ToolTip.SetToolTip(edgeTool, "Edge building tool");
            ToolTip.SetToolTip(editTool, "Edit graph elements tool");
            ToolTip.SetToolTip(deleteTool, "Delete graph elements tool");
            ToolTip.SetToolTip(clearTool, "Clear graph");

            DrawGraph.CreateGraphics(display);
            DrawTool.SetFormHandler(this);

            this.Activated += delegate { DrawTool.LoseFocus(); };

            if (args.Length > 0)
            {
                DrawGraph.LoadGraphFromGWFFile(args[0]);
            }
        }

        private void cursorTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)DrawTools.Cursor);
        }

        private void vertexTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)DrawTools.Vertex);
        }

        private void edgeTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)DrawTools.Edge);
        }
        private void editTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)DrawTools.Edit);
        }

        private void deleteTool_Click(object sender, EventArgs e)
        {
            DrawTool.SetTool((int)DrawTools.Delete);
        }

        private void clearTool_Click_1(object sender, EventArgs e)
        {
            DrawTool.LoseFocus();
            if (Utils.Confirmation("Are you really want to delete a whole graph?", "Delete graph"))
            {
                DrawGraph.ClearGraph();
            }
        }
        private void saveImageTool_Click(object sender, EventArgs e)
        {
            DrawTool.LoseFocus();
            DrawGraph.SaveGraphAsImage();
        }

        private void saveAsImageFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawGraph.SaveGraphAsImage();
        }

        private void saveAsGWFFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawGraph.SaveGraphAsGWFFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawGraph.LoadGraphFromGWFFile();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void associateFileTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.AssociateExtension();
        }

        private void unassociateFileTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.UnAssociateExtension();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var VertexInfo = String.Empty;
            foreach (var vertex in DrawGraph.Graph.Vertices)
            {
                VertexInfo += $"{vertex} -- {vertex.X} -- {vertex.Y}\n";
            }

            Task.Run(() =>
            {
                MessageBox.Show(VertexInfo);
            });

            var EdgeInfo = String.Empty;
            foreach (var edge in DrawGraph.Graph.Edges)
            {
                EdgeInfo += $"{edge.FirstVertex} -- {edge.SecondVertex} -- {edge.Weight}\n";
            }

            Task.Run(() =>
            {
                MessageBox.Show(EdgeInfo);
            });
        }
    }
}