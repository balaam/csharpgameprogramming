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

namespace Chapter10_14
{
    public partial class Form1 : Form
    {
        bool _fullscreen = false;
        FastLoop _fastLoop;
        StateSystem _system = new StateSystem();
        Input _input = new Input();
        TextureManager _textureManager = new TextureManager();
        SoundManager _soundManager = new SoundManager();
        Engine.Font _generalFont;
        Engine.Font _titleFont;

        public Form1()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();

            _input.Mouse = new Mouse(this, simpleOpenGlControl1);
            _input.Keyboard = new Keyboard(simpleOpenGlControl1);

            InitializeDisplay();
            InitializeSounds();
            InitializeTextures();
            InitializeGameData();
            InitializeFonts();
            InitializeGameState();


            _fastLoop = new FastLoop(GameLoop);
        }

        PersistantGameData _persistantGameData = new PersistantGameData();
        private void InitializeGameData()
        {
            LevelDescription level = new LevelDescription();
            level.Time = 30;
            _persistantGameData.CurrentLevel = level;
        }


        private void InitializeTextures()
        {
            // Init DevIl
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);

            // Textures are loaded here.
            _textureManager.LoadTexture("player_ship", "spaceship.tga");
            _textureManager.LoadTexture("enemy_ship", "spaceship2.tga");
            _textureManager.LoadTexture("title_font", "title_font.tga");
            _textureManager.LoadTexture("general_font", "general_font.tga");
            _textureManager.LoadTexture("background", "background.tga");
            _textureManager.LoadTexture("background_layer_1", "background_p.tga"); 
            _textureManager.LoadTexture("bullet", "bullet.tga");
            _textureManager.LoadTexture("explosion", "explode.tga");
        }


        private void InitializeFonts()
        {
            // Fonts are loaded here.
            _titleFont = new Engine.Font(_textureManager.Get("title_font"),
                                    FontParser.Parse("title_font.fnt"));

            _generalFont = new Engine.Font(_textureManager.Get("general_font"),
                        FontParser.Parse("general_font.fnt"));

        }

        private void InitializeSounds()
        {
            // Sounds are loaded here.
        }

        private void InitializeGameState()
        {
            // Game states are loaded here
            _system.AddState("start_menu", new StartMenuState(_titleFont, _generalFont, _input, _system));
            _system.AddState("inner_game", new InnerGameState(_system, _input, _textureManager, _persistantGameData, _generalFont));
            _system.AddState("game_over", new GameOverState(_persistantGameData, _system, _input, _generalFont, _titleFont));
            _system.ChangeState("start_menu");
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
            simpleOpenGlControl1.Refresh();
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