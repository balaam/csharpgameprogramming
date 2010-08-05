namespace WalkablePolygonCodeSketch
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
            this._simpleOpenGLControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.SuspendLayout();
            // 
            // simpleOpenGlControl1
            // 
            this._simpleOpenGLControl.AccumBits = ((byte)(0));
            this._simpleOpenGLControl.AutoCheckErrors = false;
            this._simpleOpenGLControl.AutoFinish = false;
            this._simpleOpenGLControl.AutoMakeCurrent = true;
            this._simpleOpenGLControl.AutoSwapBuffers = true;
            this._simpleOpenGLControl.BackColor = System.Drawing.Color.Black;
            this._simpleOpenGLControl.ColorBits = ((byte)(32));
            this._simpleOpenGLControl.DepthBits = ((byte)(16));
            this._simpleOpenGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._simpleOpenGLControl.Location = new System.Drawing.Point(0, 0);
            this._simpleOpenGLControl.Name = "simpleOpenGlControl1";
            this._simpleOpenGLControl.Size = new System.Drawing.Size(284, 264);
            this._simpleOpenGLControl.StencilBits = ((byte)(0));
            this._simpleOpenGLControl.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this._simpleOpenGLControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl _simpleOpenGLControl;
    }
}

