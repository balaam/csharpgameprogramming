using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.DevIl;
using Engine;
using Engine.Input;

namespace Chapter11_1
{
    public partial class Form1 : Form
    {
        bool _fullscreen = false;
        FastLoop _fastLoop;
        StateSystem _system = new StateSystem();
        Input _input = new Input();
        TextureManager _textureManager = new TextureManager();
        SoundManager _soundManager = new SoundManager();

        public Form1()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();
            _input.Mouse = new Mouse(this, simpleOpenGlControl1);
            _input.Keyboard = new Keyboard(simpleOpenGlControl1);

            InitializeDisplay();
            InitializeSounds();
            InitializeTextures();
            InitializeGameState();

            _fastLoop = new FastLoop(GameLoop);
        }

        private void InitializeSounds()
        {
            // Load sound files here
        }

        private void InitializeGameState()
        {
            // Load the game states here
            _system.AddState("3d_test", new Test3DState());
            _system.ChangeState("3d_test");
        }

        private void InitializeTextures()
        {
            // Init DevIl
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);

            // Load textures here using the texture manager.
        }

  
private void GameLoop(double elapsedTime)
{
    UpdateInput(elapsedTime);
    _system.Update(elapsedTime);
    _system.Render();
    simpleOpenGlControl1.Refresh();
}

private void UpdateInput(double elapsedTime)
{
    // Previous mouse code removed.
    _input.Update(elapsedTime);
}


        private void InitializeDisplay()
        {
            if (_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                ClientSize = new Size(1280, 720);
            }
            //Setup2DGraphics(ClientSize.Width, ClientSize.Height);
            Setup3DGraphics(ClientSize.Width, ClientSize.Height);
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Gl.glViewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            //Setup2DGraphics(ClientSize.Width, ClientSize.Height);
            Setup3DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void Setup2DGraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        private void Setup3DGraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(90, 4 / 3, 1, 1000);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

    }

}
