using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;

namespace Chapter6_2
{
    public partial class Form1 : Form
    {

        FastLoop _fastLoop;
        bool _fullscreen = false;
        StateSystem _stateSystem = new StateSystem();

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
            _stateSystem.AddState("splash", new SplashScreenState(_stateSystem));
            _stateSystem.ChangeState("splash");
        }

        void GameLoop(double elapsedTime)
        {
            Update();
            Render();
        }

        void Upate(double elapsedTime)
        {
            _stateSystem.Update(elapsedTime);
        }

        void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _stateSystem.Render();
            _openGLControl.Refresh();
        }
    }
}
