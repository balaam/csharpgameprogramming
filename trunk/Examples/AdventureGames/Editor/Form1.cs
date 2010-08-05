using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;
using Engine.Input;
using Tao.OpenGl;
using Tao.DevIl;

namespace Editor
{
    public partial class Form1 : Form
    {
        bool _fullscreen = false;
        FastLoop _fastLoop;
        StateSystem _system = new StateSystem();
        Input _input = new Input();
        TextureManager _textureManager = new TextureManager();
        double _width = 1024;
        double _height = 600;
        double _zoom = 1;
        Vector _sceneTranslation = new Vector();
        const double SceneScrollSpeed = 0.8;

        public double Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
                Setup2DGraphics(_sceneTranslation, _width, _height);
            }
        }

        public Form1()
        {
            InitializeComponent();
            _openGLControl.InitializeContexts();

            _input.Mouse = new Mouse(this, _openGLControl);
            _input.Keyboard = new Keyboard(_openGLControl);

            InitializeDisplay();
           // InitializeSounds();
            InitializeTextures();
           // InitializeFonts();
            InitializeGameState();

            

            _fastLoop = new FastLoop(GameLoop);
        }

        private void InitializeGameState()
        {
            _system.AddState("edit_walk_area", new EditWalkArea(_input, toolStrip1));
            _system.ChangeState("edit_walk_area");
        }


        private void InitializeTextures()
        {
            // Init DevIl
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);

            // Textures are loaded here.
        }

        private void UpdateInput(double elapsedTime)
        {
            _input.Update(elapsedTime, _sceneTranslation, _width, _height);

            if (_input.Mouse.MiddleHeld == true)
            {
                Vector moveDelta = _input.Mouse.MoveDelta;
                _sceneTranslation  += (moveDelta * SceneScrollSpeed);
                Setup2DGraphics(_sceneTranslation, _width, _height);
            }
        }

        private void GameLoop(double elapsedTime)
        {
            UpdateInput(elapsedTime);
            _system.Update(elapsedTime);
            _system.Render();
            _openGLControl.Refresh();
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
               // ClientSize = new Size(_width, _height);
            }
            Setup2DGraphics(_sceneTranslation, _width, _height);
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Gl.glViewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            Setup2DGraphics(_sceneTranslation, ClientSize.Width, ClientSize.Height);
        }


        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResize(e);

            int width = _openGLControl.ClientSize.Width;
            int height = _openGLControl.ClientSize.Height;
            Gl.glViewport(0, 0, width, height);
            Setup2DGraphics(_sceneTranslation, _width, _height);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            OnResizeEnd(e);
        }

        private void Setup2DGraphics(Vector translate, double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(translate.X - halfWidth, translate.X + halfWidth, translate.Y - halfHeight, translate.Y + halfHeight, -100, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

    }
}
