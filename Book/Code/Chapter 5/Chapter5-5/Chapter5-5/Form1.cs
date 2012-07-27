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
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            // Uncomment this line to make the point bigger
            //Gl.glPointSize(5.0f); 

            Gl.glBegin(Gl.GL_POINTS);
            {
                Gl.glVertex3d(0, 0, 1);
            }
            Gl.glEnd();

            // Uncomment these lines to draw a triangle

            //// Uncomment this line to rotate the triangle
            //// Gl.glRotated(10 * elapsedTime, 0, 1, 0);
            //Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
            //{
            //    Gl.glColor3d(1.0, 0.0, 0.0);
            //    Gl.glVertex3d(-0.5, 0, 0);
            //    Gl.glColor3d(0.0, 1.0, 0.0);
            //    Gl.glVertex3d(0.5, 0, 0);
            //    Gl.glColor3d(0.0, 0.0, 1.0);
            //    Gl.glVertex3d(0, 0.5, 0);
            //}
            //Gl.glEnd();


            Gl.glFinish();
            _openGLControl.Refresh();

        }
    }
}
