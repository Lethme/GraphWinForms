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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.display = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.deikstraTool = new System.Windows.Forms.Button();
            this.editTool = new System.Windows.Forms.Button();
            this.clearTool = new System.Windows.Forms.Button();
            this.deleteTool = new System.Windows.Forms.Button();
            this.edgeTool = new System.Windows.Forms.Button();
            this.vertexTool = new System.Windows.Forms.Button();
            this.cursorTool = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsImageFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsGWFFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.associateFileTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unassociateFileTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.display);
            this.panel1.Location = new System.Drawing.Point(15, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 510);
            this.panel1.TabIndex = 0;
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(3, 3);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(954, 504);
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(15, 538);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(954, 76);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.deikstraTool);
            this.panel3.Controls.Add(this.editTool);
            this.panel3.Controls.Add(this.clearTool);
            this.panel3.Controls.Add(this.deleteTool);
            this.panel3.Controls.Add(this.edgeTool);
            this.panel3.Controls.Add(this.vertexTool);
            this.panel3.Controls.Add(this.cursorTool);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(946, 68);
            this.panel3.TabIndex = 0;
            // 
            // deikstraTool
            // 
            this.deikstraTool.BackgroundImage = global::GraphWinForms.Properties.Resources.way;
            this.deikstraTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deikstraTool.Location = new System.Drawing.Point(589, 2);
            this.deikstraTool.Name = "deikstraTool";
            this.deikstraTool.Size = new System.Drawing.Size(69, 65);
            this.deikstraTool.TabIndex = 6;
            this.deikstraTool.UseVisualStyleBackColor = true;
            this.deikstraTool.Click += new System.EventHandler(this.deikstraTool_Click);
            // 
            // editTool
            // 
            this.editTool.BackgroundImage = global::GraphWinForms.Properties.Resources.edit;
            this.editTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.editTool.Location = new System.Drawing.Point(439, 2);
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
            this.clearTool.Location = new System.Drawing.Point(664, 2);
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
            this.deleteTool.Location = new System.Drawing.Point(514, 2);
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
            this.edgeTool.Location = new System.Drawing.Point(364, 2);
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
            this.vertexTool.Location = new System.Drawing.Point(289, 2);
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
            this.cursorTool.Location = new System.Drawing.Point(214, 2);
            this.cursorTool.Name = "cursorTool";
            this.cursorTool.Size = new System.Drawing.Size(69, 65);
            this.cursorTool.TabIndex = 0;
            this.cursorTool.UseVisualStyleBackColor = true;
            this.cursorTool.Click += new System.EventHandler(this.cursorTool_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.associateFileTypeToolStripMenuItem,
            this.unassociateFileTypeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsImageFileToolStripMenuItem,
            this.saveAsGWFFileToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsImageFileToolStripMenuItem
            // 
            this.saveAsImageFileToolStripMenuItem.Name = "saveAsImageFileToolStripMenuItem";
            this.saveAsImageFileToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveAsImageFileToolStripMenuItem.Text = "Save as Image File";
            this.saveAsImageFileToolStripMenuItem.Click += new System.EventHandler(this.saveAsImageFileToolStripMenuItem_Click);
            // 
            // saveAsGWFFileToolStripMenuItem
            // 
            this.saveAsGWFFileToolStripMenuItem.Name = "saveAsGWFFileToolStripMenuItem";
            this.saveAsGWFFileToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveAsGWFFileToolStripMenuItem.Text = "Save as GWF File";
            this.saveAsGWFFileToolStripMenuItem.Click += new System.EventHandler(this.saveAsGWFFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(182, 6);
            // 
            // associateFileTypeToolStripMenuItem
            // 
            this.associateFileTypeToolStripMenuItem.Name = "associateFileTypeToolStripMenuItem";
            this.associateFileTypeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.associateFileTypeToolStripMenuItem.Text = "Associate File Type";
            this.associateFileTypeToolStripMenuItem.Click += new System.EventHandler(this.associateFileTypeToolStripMenuItem_Click);
            // 
            // unassociateFileTypeToolStripMenuItem
            // 
            this.unassociateFileTypeToolStripMenuItem.Name = "unassociateFileTypeToolStripMenuItem";
            this.unassociateFileTypeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.unassociateFileTypeToolStripMenuItem.Text = "Unassociate File Type";
            this.unassociateFileTypeToolStripMenuItem.Click += new System.EventHandler(this.unassociateFileTypeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.graphToolStripMenuItem.Text = "Graph";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.testToolStripMenuItem,
            this.test1ToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.testToolStripMenuItem.Text = "Show graphic part";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // test1ToolStripMenuItem
            // 
            this.test1ToolStripMenuItem.Name = "test1ToolStripMenuItem";
            this.test1ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.test1ToolStripMenuItem.Text = "Show local part";
            this.test1ToolStripMenuItem.Click += new System.EventHandler(this.test1ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 626);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Graph Builder";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.PictureBox display;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsImageFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsGWFFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem associateFileTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unassociateFileTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Button deikstraTool;
        public System.Windows.Forms.Button editTool;
        public System.Windows.Forms.Button clearTool;
        public System.Windows.Forms.Button deleteTool;
        public System.Windows.Forms.Button edgeTool;
        public System.Windows.Forms.Button vertexTool;
        public System.Windows.Forms.Button cursorTool;
    }
}

