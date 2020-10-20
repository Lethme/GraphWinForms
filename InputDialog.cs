using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphWinForms
{
    public partial class InputDialog : Form
    {
        public enum DialogType
        {
            Text,
            IntNumber,
            RealNumber
        }
        public string Line { get; private set; } = String.Empty;
        public DialogType Type { get; private set; }
        public InputDialog()
        {
            InitializeComponent();
        }

        public InputDialog(string windowTitle, string windowText, DialogType dialogType)
        {
            InitializeComponent();
            this.Text = windowTitle;
            label1.Text = windowText;
            Type = dialogType;

            switch (Type)
            {
                case DialogType.Text:
                    {
                        textBox1.MaxLength = 14;
                        break;
                    }
                case DialogType.IntNumber:
                    {
                        textBox1.MaxLength = 10;
                        break;
                    }
                case DialogType.RealNumber:
                    {
                        textBox1.MaxLength = 14;
                        break;
                    }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            switch (Type)
            {
                case DialogType.Text:
                    {
                        Line = textBox1.Text;
                        break;
                    }
                case DialogType.IntNumber:
                    {
                        try
                        {
                            Int32.Parse(textBox1.Text);
                            Line = textBox1.Text;
                        }
                        catch (Exception)
                        {
                            Line = String.Empty;
                        }
                        break;
                    }
                case DialogType.RealNumber:
                    {
                        try
                        {
                            Double.Parse(textBox1.Text);
                            Line = textBox1.Text;
                        }
                        catch (Exception)
                        {
                            Line = String.Empty;
                        }
                        break;
                    }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}