namespace Editor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this._openGLControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this._defaultToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._addPolygonToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._linkToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._addVertexToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this._modeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 462);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(628, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(628, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OnOpenClicked);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveClicked);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.Size = new System.Drawing.Size(628, 396);
            // 
            // _openGLControl
            // 
            this._openGLControl.AccumBits = ((byte)(0));
            this._openGLControl.AutoCheckErrors = false;
            this._openGLControl.AutoFinish = false;
            this._openGLControl.AutoMakeCurrent = true;
            this._openGLControl.AutoSwapBuffers = true;
            this._openGLControl.BackColor = System.Drawing.Color.Black;
            this._openGLControl.ColorBits = ((byte)(32));
            this._openGLControl.DepthBits = ((byte)(16));
            this._openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._openGLControl.Location = new System.Drawing.Point(0, 63);
            this._openGLControl.Name = "_openGLControl";
            this._openGLControl.Size = new System.Drawing.Size(628, 399);
            this._openGLControl.StencilBits = ((byte)(0));
            this._openGLControl.TabIndex = 3;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(42, 36);
            this.toolStripLabel1.Text = "Action";
            // 
            // _defaultToolStripButton
            // 
            this._defaultToolStripButton.Checked = true;
            this._defaultToolStripButton.CheckOnClick = true;
            this._defaultToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this._defaultToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._defaultToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_defaultToolStripButton.Image")));
            this._defaultToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._defaultToolStripButton.Name = "_defaultToolStripButton";
            this._defaultToolStripButton.Size = new System.Drawing.Size(36, 36);
            this._defaultToolStripButton.Text = "Default Mode - allow vertices to be moved.";
            // 
            // _addPolygonToolStripButton
            // 
            this._addPolygonToolStripButton.CheckOnClick = true;
            this._addPolygonToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._addPolygonToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_addPolygonToolStripButton.Image")));
            this._addPolygonToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._addPolygonToolStripButton.Name = "_addPolygonToolStripButton";
            this._addPolygonToolStripButton.Size = new System.Drawing.Size(36, 36);
            this._addPolygonToolStripButton.Text = "Polygon Add Mode - add polygons by left clicking";
            // 
            // _linkToolStripButton
            // 
            this._linkToolStripButton.CheckOnClick = true;
            this._linkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._linkToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_linkToolStripButton.Image")));
            this._linkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._linkToolStripButton.Name = "_linkToolStripButton";
            this._linkToolStripButton.Size = new System.Drawing.Size(36, 36);
            this._linkToolStripButton.Text = "Link Walkable Areas - connect polygons via a shared edge";
            // 
            // _addVertexToolStripButton
            // 
            this._addVertexToolStripButton.CheckOnClick = true;
            this._addVertexToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._addVertexToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_addVertexToolStripButton.Image")));
            this._addVertexToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._addVertexToolStripButton.Name = "_addVertexToolStripButton";
            this._addVertexToolStripButton.Size = new System.Drawing.Size(36, 36);
            this._addVertexToolStripButton.Text = "Add Vertex - click a vertex to the nearest edge, if allowable.";
            // 
            // _toolStrip
            // 
            this._toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this._modeComboBox,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this._defaultToolStripButton,
            this._addPolygonToolStripButton,
            this._linkToolStripButton,
            this._addVertexToolStripButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 24);
            this._toolStrip.MaximumSize = new System.Drawing.Size(0, 39);
            this._toolStrip.MinimumSize = new System.Drawing.Size(0, 39);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(628, 39);
            this._toolStrip.TabIndex = 2;
            this._toolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(38, 36);
            this.toolStripLabel2.Text = "Mode";
            // 
            // _modeComboBox
            // 
            this._modeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._modeComboBox.Items.AddRange(new object[] {
            "Walk Test",
            "Edit NavMesh"});
            this._modeComboBox.Name = "_modeComboBox";
            this._modeComboBox.Size = new System.Drawing.Size(121, 39);
            this._modeComboBox.SelectedIndexChanged += new System.EventHandler(this.OnModeChange);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 484);
            this.Controls.Add(this._openGLControl);
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private Tao.Platform.Windows.SimpleOpenGlControl _openGLControl;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton _defaultToolStripButton;
        private System.Windows.Forms.ToolStripButton _addPolygonToolStripButton;
        private System.Windows.Forms.ToolStripButton _linkToolStripButton;
        private System.Windows.Forms.ToolStripButton _addVertexToolStripButton;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox _modeComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

    }
}

