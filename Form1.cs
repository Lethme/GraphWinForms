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
            
            this.Icon = Icon.FromHandle(Properties.Resources.graph_icon.GetHicon());
            
            DrawGraph.CreateGraphics(display);
            DrawTool.SetFormHandler(this, args);
        }
    }
}