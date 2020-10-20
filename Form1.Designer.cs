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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.saveImageTool = new System.Windows.Forms.Button();
            this.editTool = new System.Windows.Forms.Button();
            this.clearTool = new System.Windows.Forms.Button();
            this.deleteTool = new System.Windows.Forms.Button();
            this.edgeTool = new System.Windows.Forms.Button();
            this.vertexTool = new System.Windows.Forms.Button();
            this.cursorTool = new System.Windows.Forms.Button();
            this.display = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
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
            this.panel3.Controls.Add(this.saveImageTool);
            this.panel3.Controls.Add(this.editTool);
            this.panel3.Controls.Add(this.clearTool);
            this.panel3.Controls.Add(this.deleteTool);
            this.panel3.Controls.Add(this.edgeTool);
            this.panel3.Controls.Add(this.vertexTool);
            this.panel3.Controls.Add(this.cursorTool);
            this.panel3.Location = new System.Drawing.Point(184, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(592, 68);
            this.panel3.TabIndex = 0;
            // 
            // saveImageTool
            // 
            this.saveImageTool.BackgroundImage = global::GraphWinForms.Properties.Resources.save;
            this.saveImageTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveImageTool.Location = new System.Drawing.Point(487, 2);
            this.saveImageTool.Name = "saveImageTool";
            this.saveImageTool.Size = new System.Drawing.Size(69, 65);
            this.saveImageTool.TabIndex = 6;
            this.saveImageTool.UseVisualStyleBackColor = true;
            this.saveImageTool.Click += new System.EventHandler(this.saveImageTool_Click);
            // 
            // editTool
            // 
            this.editTool.BackgroundImage = global::GraphWinForms.Properties.Resources.edit;
            this.editTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.editTool.Location = new System.Drawing.Point(262, 2);
            this.editTool.Name = "editTool";
            this.editTool.Size = new System.Drawing.Size(69, 65);
            this.editTool.TabIndex = 5;
            this.editTool.UseVisualStyleBackColor = true;
            this.editTool.Click += new System.EventHandler(this.editTool_Click);
            // 
            // clearTool
            // 
            this.clearTool.BackgroundImage = global::GraphWinForms.Properties.Resources.trash;
            this.clearTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clearTool.Location = new System.Drawing.Point(412, 2);
            this.clearTool.Name = "clearTool";
            this.clearTool.Size = new System.Drawing.Size(69, 65);
            this.clearTool.TabIndex = 4;
            this.clearTool.UseVisualStyleBackColor = true;
            this.clearTool.Click += new System.EventHandler(this.clearTool_Click_1);
            // 
            // deleteTool
            // 
            this.deleteTool.BackgroundImage = global::GraphWinForms.Properties.Resources.delete;
            this.deleteTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteTool.Location = new System.Drawing.Point(337, 2);
            this.deleteTool.Name = "deleteTool";
            this.deleteTool.Size = new System.Drawing.Size(69, 65);
            this.deleteTool.TabIndex = 3;
            this.deleteTool.UseVisualStyleBackColor = true;
            this.deleteTool.Click += new System.EventHandler(this.deleteTool_Click);
            // 
            // edgeTool
            // 
            this.edgeTool.BackgroundImage = global::GraphWinForms.Properties.Resources.edge;
            this.edgeTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.edgeTool.Location = new System.Drawing.Point(187, 2);
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
            this.vertexTool.Location = new System.Drawing.Point(112, 2);
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
            this.cursorTool.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cursorTool.Location = new System.Drawing.Point(37, 2);
            this.cursorTool.Name = "cursorTool";
            this.cursorTool.Size = new System.Drawing.Size(69, 65);
            this.cursorTool.TabIndex = 0;
            this.cursorTool.UseVisualStyleBackColor = true;
            this.cursorTool.Click += new System.EventHandler(this.cursorTool_Click);
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(3, 3);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(954, 514);
            this.display.TabIndex = 0;
            this.display.TabStop = false;
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
            this.Text = "Graph Builder";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.PictureBox display;
        public System.Windows.Forms.Button deleteTool;
        public System.Windows.Forms.Button edgeTool;
        public System.Windows.Forms.Button vertexTool;
        public System.Windows.Forms.Button cursorTool;
        public System.Windows.Forms.Button clearTool;
        public System.Windows.Forms.Button editTool;
        public System.Windows.Forms.Button saveImageTool;
    }
}

