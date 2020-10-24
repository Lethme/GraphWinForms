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
using GraphWinForms;

namespace GraphWinForms
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();

            display.BackColor = Color.White;
            display.BorderStyle = BorderStyle.FixedSingle;

            this.Activated += delegate { DrawTool.LoseFocus(); };

            DrawGraph.CreateGraphics(display);
            DrawTool.SetFormHandler(this);

            if (args.Length > 0)
            {
                DrawGraph.LoadGraphFromGWFFile(args[0]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var tool in Controls.OfType<Button>())
            {
                tool.Click += (s, ev) =>
                {
                    DrawGraph.UnHighlightPath();
                    DisplayList.LoseFocus();
                };
            }

            var ToolTip = new ToolTip();
            ToolTip.SetToolTip(cursorTool, "Basic tool");
            ToolTip.SetToolTip(vertexTool, "Vetex building tool");
            ToolTip.SetToolTip(edgeTool, "Edge building tool");
            ToolTip.SetToolTip(editTool, "Edit graph elements tool");
            ToolTip.SetToolTip(deleteTool, "Delete graph elements tool");
            ToolTip.SetToolTip(deikstraTool, "Find a shortest path between two selected vertices");

            cursorTool.Click += (s, ev) =>
            {
                DrawTool.SetTool(DrawTools.Cursor);
            };

            vertexTool.Click += (s, ev) =>
            {
                DrawTool.SetTool(DrawTools.Vertex);
            };

            edgeTool.Click += (s, ev) =>
            {
                DrawTool.SetTool(DrawTools.Edge);
            };

            editTool.Click += (s, ev) =>
            {
                DrawTool.SetTool(DrawTools.Edit);
            };

            deleteTool.Click += (s, ev) =>
            {
                DrawTool.SetTool(DrawTools.Delete);
            };

            deikstraTool.Click += (s, ev) =>
            {
                DrawTool.SetTool(DrawTools.Deikstra);
            };
        }

        private void saveImageTool_Click(object sender, EventArgs e)
        {
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
            using (var AboutForm = new About())
            {
                AboutForm.ShowDialog();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utils.Confirmation("You really want to exit?", "Exit"))
            {
                Application.Exit();
            }
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

            var EdgeInfo = String.Empty;
            foreach (var edge in DrawGraph.Graph.Edges)
            {
                EdgeInfo += $"{edge.FirstVertex} -- {edge.SecondVertex} -- {edge.Weight}\n";
            }

            Task.Run(() =>
            {
                MessageBox.Show(VertexInfo + "\n" + EdgeInfo, "Graphic Representation");
            });
        }

        private void test1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var EdgeInfo = String.Empty;
            foreach (var vertex in DrawGraph.LocalGraph.Vertices)
            {
                EdgeInfo += $"{vertex}: {{ ";
                foreach (var edge in vertex.Edges)
                {
                    EdgeInfo += $"'{edge.ConnectedVertex}({edge.EdgeWeight})' ";
                }
                EdgeInfo += "}\n";
            }

            Task.Run(() =>
            {
                MessageBox.Show(EdgeInfo, "Local Representation");
            });
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DrawGraph.LocalGraph.IsEmpty)
            {
                if (Utils.Confirmation("Are you really want to delete graph?", "Delete graph"))
                {
                    DisplayList.Clear();
                    DrawGraph.ClearGraph();
                }
            }
        }

        private void findMinimalPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DrawGraph.LocalGraph.IsEmpty)
            {
                var Line = Utils.ShowInputDialog
                (
                    "Input path length",
                    "Path length",
                    InputDialog.DialogType.IntNumber
                );
                if (Line != null && Line != String.Empty)
                {
                    DisplayList.Clear();
                    DrawGraph.UnHighlightPath();
                    var MinPathList = DrawGraph.LocalGraph.FindMinPath(Int32.Parse(Line));
                    if (MinPathList != null)
                    {
                        foreach (var path in MinPathList)
                        {
                            DisplayList.AddItem(path);
                        }
                    }
                }
            }
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!DisplayList.IsEmpty && Utils.Confirmation("You really want to clear the list?", "Clear list"))
            {
                DisplayList.Clear();
                DrawGraph.UnHighlightPath();
            }
        }
    }
}