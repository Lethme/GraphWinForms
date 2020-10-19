namespace GraphWinForms
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.display = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.clearTool = new System.Windows.Forms.Button();
            this.edgeTool = new System.Windows.Forms.Button();
            this.vertexTool = new System.Windows.Forms.Button();
            this.cursorTool = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.display);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 520);
            this.panel1.TabIndex = 0;
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(3, 3);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(954, 514);
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            this.display.MouseClick += new System.Windows.Forms.MouseEventHandler(this.display_MouseClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(12, 538);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(960, 76);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.clearTool);
            this.panel3.Controls.Add(this.edgeTool);
            this.panel3.Controls.Add(this.vertexTool);
            this.panel3.Controls.Add(this.cursorTool);
            this.panel3.Location = new System.Drawing.Point(184, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(592, 68);
            this.panel3.TabIndex = 0;
            // 
            // clearTool
            // 
            this.clearTool.BackgroundImage = global::GraphWinForms.Properties.Resources.trash;
            this.clearTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clearTool.Location = new System.Drawing.Point(374, 3);
            this.clearTool.Name = "clearTool";
            this.clearTool.Size = new System.Drawing.Size(69, 65);
            this.clearTool.TabIndex = 3;
            this.clearTool.UseVisualStyleBackColor = true;
            this.clearTool.Click += new System.EventHandler(this.clearTool_Click);
            // 
            // edgeTool
            // 
            this.edgeTool.BackgroundImage = global::GraphWinForms.Properties.Resources.edge;
            this.edgeTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.edgeTool.Location = new System.Drawing.Point(299, 3);
            this.edgeTool.Name = "edgeTool";
            this.edgeTool.Size = new System.Drawing.Size(69, 65);
            this.edgeTool.TabIndex = 2;
            this.edgeTool.UseVisualStyleBackColor = true;
            this.edgeTool.Click += new System.EventHandler(this.edgeTool_Click);
            // 
            // vertexTool
            // 
            this.vertexTool.BackgroundImage = global::GraphWinForms.Properties.Resources.vertex;
            this.vertexTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.vertexTool.Location = new System.Drawing.Point(224, 3);
            this.vertexTool.Name = "vertexTool";
            this.vertexTool.Size = new System.Drawing.Size(69, 65);
            this.vertexTool.TabIndex = 1;
            this.vertexTool.UseVisualStyleBackColor = true;
            this.vertexTool.Click += new System.EventHandler(this.vertexTool_Click);
            // 
            // cursorTool
            // 
            this.cursorTool.BackgroundImage = global::GraphWinForms.Properties.Resources.cursor;
            this.cursorTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cursorTool.Location = new System.Drawing.Point(149, 3);
            this.cursorTool.Name = "cursorTool";
            this.cursorTool.Size = new System.Drawing.Size(69, 65);
            this.cursorTool.TabIndex = 0;
            this.cursorTool.UseVisualStyleBackColor = true;
            this.cursorTool.Click += new System.EventHandler(this.cursorTool_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 626);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Graph";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.PictureBox display;
        public System.Windows.Forms.Button clearTool;
        public System.Windows.Forms.Button edgeTool;
        public System.Windows.Forms.Button vertexTool;
        public System.Windows.Forms.Button cursorTool;
    }
}

