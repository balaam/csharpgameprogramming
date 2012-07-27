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

namespace Chapter8_9
{
    // In this example the input class is added and updated.
    public partial class Form1 : Form
    {
        Input _input = new Input();
        FastLoop _fastLoop;
        bool _fullscreen = false;
        StateSystem _system = new StateSystem();
        TextureManager _textureManager = new TextureManager();
        public Form1()
        {
            _fastLoop = new FastLoop(GameLoop);

            InitializeComponent();
            _openGLControl.InitializeContexts();

            // Initialize DevIL
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);

            // Load textures
            _textureManager.LoadTexture("font", "font.tga");
            _textureManager.LoadTexture("face", "face_alpha.tif");

            if (_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                ClientSize = new Size(1280, 720);
            }

            // Initialize states
            _system.AddState("graph_state", new WaveformGraphState());
            _system.AddState("special_effect_state", new SpecialEffectState(_textureManager));
            _system.AddState("swirl_effect_state", new SwirlEffectState(_textureManager));
            _system.AddState("bounce_state", new CharacterBounceState(_textureManager));
            _system.AddState("circle_intersect_state", new CircleIntersectionState(_input));
            _system.AddState("rectangle_intersect_state", new RectangleIntersectionState(_input));
            _system.AddState("sprite_tween_state", new SpriteTweenState(_textureManager));
            _system.ChangeState("sprite_tween_state");
        }

        private void UpdateInput()
        {
            System.Drawing.Point mousePos = Cursor.Position;
            mousePos = _openGLControl.PointToClient(mousePos);

            // Now use our point definition, 
            Point adjustedMousePoint = new Point();
            adjustedMousePoint.X = (float)mousePos.X - ((float)ClientSize.Width / 2);
            adjustedMousePoint.Y = ((float)ClientSize.Height / 2)-(float)mousePos.Y;
            _input.MousePosition = adjustedMousePoint;
        }


        void GameLoop(double elapsedTime)
        {
             UpdateInput();
            _system.Update(elapsedTime);
            _system.Render();
            _openGLControl.Refresh();

        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Gl.glViewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
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

    }
}
