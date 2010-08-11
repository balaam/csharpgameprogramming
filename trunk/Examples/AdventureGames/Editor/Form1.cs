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
        public const double Version = 0.1;

        // Once again change this into a state system if it get too complicated.
        enum EditMode
        {
            WalkTest,
            NavMeshEdit
        }
        EditMode _editMode = EditMode.NavMeshEdit;
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

        Scene _scene;
        Layers _layers;

        public Form1()
        {
            InitializeComponent();
            _openGLControl.InitializeContexts();
            _openGLControl.SizeChanged += new EventHandler(OnOpenGLControlResized);

            _input.Mouse = new Mouse(this, _openGLControl);
            _input.Keyboard = new Keyboard(_openGLControl);
            _scene = new Scene();

            InitializeDisplay();
           // InitializeSounds();
            InitializeTextures();
           // InitializeFonts();
            InitializeGameState();

            
            _layers = new Layers(_scene, _textureManager);
            _layers.Show();

            _modeComboBox.SelectedIndex = 1;
            

            _fastLoop = new FastLoop(GameLoop);
        }

   

        private void InitializeGameState()
        {
            _system.AddState("edit_walk_area", new EditWalkArea(_input, _toolStrip, _scene));
            _system.AddState("test_walking", new TestWalkState(_input, _toolStrip, _scene, _textureManager));
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

            // The character animations should probably not be hardcoded
            // but data driven instead.
            _textureManager.LoadTexture("pc_stand_still", "Art/templateman.png");
            _textureManager.LoadTexture("pc_walk_front", "Art/templatemanwalkfront.png");
            _textureManager.LoadTexture("pc_walk_up", "Art/templatemanwalkup.png");
            _textureManager.LoadTexture("pc_walk_side", "Art/templatemanwalkside.png");
        }

        private void UpdateInput(double elapsedTime)
        {
            _input.Update(elapsedTime, _sceneTranslation, _width * Zoom, _height * Zoom);

            if (_input.Mouse.MiddleHeld == true)
            {
                Vector moveDelta = _input.Mouse.MoveDelta;
                _sceneTranslation  += (moveDelta * SceneScrollSpeed);
                Setup2DGraphics(_sceneTranslation, _width, _height);
            }

            Zoom -= (double)_input.Mouse.Wheel * 0.001;
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

        void OnOpenGLControlResized(object sender, EventArgs e)
        {
            int width = _openGLControl.ClientSize.Width;
            int height = _openGLControl.ClientSize.Height;
            Gl.glViewport(0, 0, width, height);
            Setup2DGraphics(_sceneTranslation, _width, _height);
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
            double halfWidth = (width * Zoom) / 2;
            double halfHeight = (height * Zoom) / 2;
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(translate.X - halfWidth, translate.X + halfWidth, translate.Y - halfHeight, translate.Y + halfHeight, -100, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Version == 0.1)
                {
                    Persist.Persist01.Save(saveFileDialog1.FileName, _scene, _textureManager);
                }
            }
        }

        private void OnOpenClicked(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Version == 0.1)
                {
                    Persist.Persist01.Open(openFileDialog1.FileName, _scene, _textureManager, _layers);
                }
            }
        }

        private void OnModeChange(object sender, EventArgs e)
        {
            EditMode newMode = (EditMode)_modeComboBox.SelectedIndex;

            if (newMode == _editMode)
            {
                return;
            }

            // Unload any options.
            if (_editMode == EditMode.NavMeshEdit)
            {
                // We're going to replace the nav mesh, that means
                // it's toolbar needs stripping down.
                _toolStrip.Items.Remove(toolStripLabel1);
                _toolStrip.Items.Remove(_defaultToolStripButton);
                _toolStrip.Items.Remove(_addVertexToolStripButton);
                _toolStrip.Items.Remove(_addPolygonToolStripButton);
                _toolStrip.Items.Remove(_linkToolStripButton); 
            }

            _editMode = newMode;

            // Load new options
            if (newMode == EditMode.NavMeshEdit)
            {
                
                _toolStrip.Items.Add(toolStripLabel1);
                _toolStrip.Items.Add(_defaultToolStripButton);
                _toolStrip.Items.Add(_addPolygonToolStripButton);
                _toolStrip.Items.Add(_linkToolStripButton);
                _toolStrip.Items.Add(_addVertexToolStripButton);
                _system.ChangeState("edit_walk_area");
              
            }
            else if (newMode == EditMode.WalkTest)
            {
                _system.ChangeState("test_walking");
            }
        }

    }
}
