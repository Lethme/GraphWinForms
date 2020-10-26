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
            this.panel2 = new System.Windows.Forms.Panel();
            this.centerTool = new System.Windows.Forms.Button();
            this.deikstraTool = new System.Windows.Forms.Button();
            this.edgeTool = new System.Windows.Forms.Button();
            this.editTool = new System.Windows.Forms.Button();
            this.deleteTool = new System.Windows.Forms.Button();
            this.cursorTool = new System.Windows.Forms.Button();
            this.vertexTool = new System.Windows.Forms.Button();
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
            this.graphToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.findMinimalPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.display = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.centerTool);
            this.panel2.Controls.Add(this.deikstraTool);
            this.panel2.Controls.Add(this.edgeTool);
            this.panel2.Controls.Add(this.editTool);
            this.panel2.Controls.Add(this.deleteTool);
            this.panel2.Controls.Add(this.cursorTool);
            this.panel2.Controls.Add(this.vertexTool);
            this.panel2.Location = new System.Drawing.Point(12, 552);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(964, 64);
            this.panel2.TabIndex = 1;
            // 
            // centerTool
            // 
            this.centerTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.centerTool.BackgroundImage = global::GraphWinForms.Properties.Resources.center1;
            this.centerTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.centerTool.Location = new System.Drawing.Point(649, 1);
            this.centerTool.Name = "centerTool";
            this.centerTool.Size = new System.Drawing.Size(60, 60);
            this.centerTool.TabIndex = 7;
            this.centerTool.UseVisualStyleBackColor = true;
            // 
            // deikstraTool
            // 
            this.deikstraTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.deikstraTool.BackgroundImage = global::GraphWinForms.Properties.Resources.way;
            this.deikstraTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deikstraTool.Location = new System.Drawing.Point(583, 1);
            this.deikstraTool.Name = "deikstraTool";
            this.deikstraTool.Size = new System.Drawing.Size(60, 60);
            this.deikstraTool.TabIndex = 6;
            this.deikstraTool.UseVisualStyleBackColor = true;
            // 
            // edgeTool
            // 
            this.edgeTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.edgeTool.BackgroundImage = global::GraphWinForms.Properties.Resources.edge;
            this.edgeTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.edgeTool.Location = new System.Drawing.Point(385, 1);
            this.edgeTool.Name = "edgeTool";
            this.edgeTool.Size = new System.Drawing.Size(60, 60);
            this.edgeTool.TabIndex = 2;
            this.edgeTool.UseVisualStyleBackColor = true;
            // 
            // editTool
            // 
            this.editTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.editTool.BackgroundImage = global::GraphWinForms.Properties.Resources.edit;
            this.editTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.editTool.Location = new System.Drawing.Point(451, 1);
            this.editTool.Name = "editTool";
            this.editTool.Size = new System.Drawing.Size(60, 60);
            this.editTool.TabIndex = 5;
            this.editTool.UseVisualStyleBackColor = true;
            // 
            // deleteTool
            // 
            this.deleteTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.deleteTool.BackgroundImage = global::GraphWinForms.Properties.Resources.delete;
            this.deleteTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteTool.Location = new System.Drawing.Point(517, 1);
            this.deleteTool.Name = "deleteTool";
            this.deleteTool.Size = new System.Drawing.Size(60, 60);
            this.deleteTool.TabIndex = 3;
            this.deleteTool.UseVisualStyleBackColor = true;
            // 
            // cursorTool
            // 
            this.cursorTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cursorTool.BackgroundImage = global::GraphWinForms.Properties.Resources.cursor;
            this.cursorTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cursorTool.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cursorTool.Location = new System.Drawing.Point(253, 1);
            this.cursorTool.Name = "cursorTool";
            this.cursorTool.Size = new System.Drawing.Size(60, 60);
            this.cursorTool.TabIndex = 0;
            this.cursorTool.UseVisualStyleBackColor = true;
            // 
            // vertexTool
            // 
            this.vertexTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.vertexTool.BackgroundImage = global::GraphWinForms.Properties.Resources.vertex;
            this.vertexTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.vertexTool.Location = new System.Drawing.Point(319, 1);
            this.vertexTool.Name = "vertexTool";
            this.vertexTool.Size = new System.Drawing.Size(60, 60);
            this.vertexTool.TabIndex = 1;
            this.vertexTool.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphToolStripMenuItem1,
            this.listToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1194, 24);
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
            // 
            // saveAsGWFFileToolStripMenuItem
            // 
            this.saveAsGWFFileToolStripMenuItem.Name = "saveAsGWFFileToolStripMenuItem";
            this.saveAsGWFFileToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveAsGWFFileToolStripMenuItem.Text = "Save as GWF File";
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
            // 
            // unassociateFileTypeToolStripMenuItem
            // 
            this.unassociateFileTypeToolStripMenuItem.Name = "unassociateFileTypeToolStripMenuItem";
            this.unassociateFileTypeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.unassociateFileTypeToolStripMenuItem.Text = "Unassociate File Type";
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
            // 
            // graphToolStripMenuItem1
            // 
            this.graphToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findMinimalPathToolStripMenuItem,
            this.toolStripSeparator3,
            this.clearToolStripMenuItem});
            this.graphToolStripMenuItem1.Name = "graphToolStripMenuItem1";
            this.graphToolStripMenuItem1.Size = new System.Drawing.Size(51, 20);
            this.graphToolStripMenuItem1.Text = "Graph";
            // 
            // findMinimalPathToolStripMenuItem
            // 
            this.findMinimalPathToolStripMenuItem.Name = "findMinimalPathToolStripMenuItem";
            this.findMinimalPathToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.findMinimalPathToolStripMenuItem.Text = "Find minimal path";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // listToolStripMenuItem1
            // 
            this.listToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem1});
            this.listToolStripMenuItem1.Name = "listToolStripMenuItem1";
            this.listToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.listToolStripMenuItem1.Text = "List";
            // 
            // clearToolStripMenuItem1
            // 
            this.clearToolStripMenuItem1.Name = "clearToolStripMenuItem1";
            this.clearToolStripMenuItem1.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem1.Text = "Clear";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(987, 27);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(195, 589);
            this.listBox1.TabIndex = 0;
            // 
            // display
            // 
            this.display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.display.Location = new System.Drawing.Point(12, 27);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(964, 519);
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 626);
            this.Controls.Add(this.display);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel2);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1210, 665);
            this.Name = "Form1";
            this.Text = "Graph Builder";
            this.panel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem saveAsImageFileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem saveAsGWFFileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem associateFileTypeToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem unassociateFileTypeToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.Button deikstraTool;
        public System.Windows.Forms.Button editTool;
        public System.Windows.Forms.Button deleteTool;
        public System.Windows.Forms.Button edgeTool;
        public System.Windows.Forms.Button vertexTool;
        public System.Windows.Forms.Button cursorTool;
        public System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem findMinimalPathToolStripMenuItem;
        public System.Windows.Forms.PictureBox display;
        public System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem1;
        public System.Windows.Forms.Button centerTool;
    }
}

