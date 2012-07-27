using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;

namespace Chapter6_1
{
    enum GameState
    {
        CompanySplash,
        TitleMenu,
        PlayingGame,
        SettingsMenu,
    }


    public partial class Form1 : Form
    {
        GameState _currentState = GameState.CompanySplash;


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


            switch (_currentState)
            {
                case GameState.CompanySplash:
                    {
                        // Update the starting splash screen
                    } break;
                case GameState.SettingsMenu:
                    {
                        // Update the settings menu
                    } break;
                case GameState.PlayingGame:
                    {
                        // Update the game
                    } break;
                case GameState.TitleMenu:
                    {
                        // Update title menu 
                    } break;
                default:
                    {
                        // Error invalid state
                    } break;
            }

            _openGLControl.Refresh();
        }
    }
}
