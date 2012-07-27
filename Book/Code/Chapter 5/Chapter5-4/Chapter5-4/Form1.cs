using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;

namespace StartingGraphics
{
    public partial class Form1 : Form
    {
        FastLoop _fastLoop;
        bool _fullscreen = false;

        public Form1()
        {
            _fastLoop = new FastLoop(GameLoop);

            InitializeComponent();
            _openGLControl.InitializeContexts();


            if (_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
        }

        void GameLoop(double elapsedTime)
        {
            Gl.glClearColor(1.0f, 0.0f, 0.0f, 1.0f);
            // Uncomment the following two lines of code to randomly flash the screen differently colours
            //Random r = new Random();
            //Gl.glClearColor((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble(), 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glFinish();

            _openGLControl.Refresh();
        }
    }

}
