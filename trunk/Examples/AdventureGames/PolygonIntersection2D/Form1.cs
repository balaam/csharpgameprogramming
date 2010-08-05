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

namespace WalkablePolygonCodeSketch
{
    public partial class Form1 : Form
    {
        bool _fullscreen = false;
        FastLoop _fastLoop;
        StateSystem _system = new StateSystem();
        Input _input = new Input();
        TextureManager _textureManager = new TextureManager();
        SoundManager _soundManager = new SoundManager();
        ConvexPolyDrawer.PathFinder _pathFinder = new ConvexPolyDrawer.PathFinder();

        // Used for testing path creation.
        PathTest.PathData _pathData = new PathTest.PathData();
        Engine.Font _numberFont;

        public Form1()
        {
            InitializeComponent();
            _simpleOpenGLControl.InitializeContexts();

            _input.Mouse = new Mouse(this, _simpleOpenGLControl);
            _input.Keyboard = new Keyboard(_simpleOpenGLControl);

            InitializeDisplay();
            InitializeSounds();
            InitializeTextures();
            InitializeFonts();
            InitializeGameState();

            _fastLoop = new FastLoop(GameLoop);
        }

        private void InitializeGameData()
        {
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


        private void InitializeFonts()
        {

            // Fonts are loaded here.
            _numberFont = Engine.FontParser.CreateFont("numberFont.fnt", _textureManager);
        }

        private void InitializeSounds()
        {
            // Sounds are loaded here.
        }

        private void InitializeGameState()
        {
            // Game states are loaded here

            // start_state - draw some custom convex polygons and perform an intersection test
            // on each one.
           _system.AddState("start_state", new PolygonCreateState(_input));
            // second try - draw one custom poly and work out a graph of nodes for path finding.
           _system.AddState("second_try", new NavMeshCreateState(_input));

            // setup - create a poly with node set for the second state
            // test path - choose some nodes and plots a path between them
           _system.AddState("setup", new PathTest.SetupStatePathCreation(_input, _system, _pathData));
           _system.AddState("test_path", new PathTest.PathCreationState(_input, _pathData, _numberFont));

            // thid time
           _system.AddState("third_time", new ConvexPolyDrawer.ConvexPolyDrawerState(_input, _pathFinder, _system));
            _system.AddState("pathfinder", new ConvexPolyDrawer.PathFinderState(_input, _pathFinder, _numberFont));

           _system.ChangeState("third_time");
        }

        private void UpdateInput(double elapsedTime)
        {
            _input.Update(elapsedTime);
        }

        private void GameLoop(double elapsedTime)
        {
            UpdateInput(elapsedTime);
            _system.Update(elapsedTime);
            _system.Render();
            _simpleOpenGLControl.Refresh();
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
                ClientSize = new Size(1024, 600);
            }
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
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