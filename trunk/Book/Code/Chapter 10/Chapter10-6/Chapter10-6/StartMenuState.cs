using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Tao.OpenGl;
using Engine.Input;

namespace Chapter10_6
{
    class StartMenuState : IGameObject
    {
        Engine.Font _generalFont;
        Input _input;
        VerticalMenu _menu;

        Renderer _renderer = new Renderer();
        Text _title;

        StateSystem _system;
        public StartMenuState(Engine.Font titleFont, Engine.Font generalFont, Input input, StateSystem system)
        {
            _system = system;

            _generalFont = generalFont;
            _input = input;
            InitializeMenu();
            _title = new Text("Shooter", titleFont);
            _title.SetColor(new Color(0, 0, 0, 1));
            // Centerre on the x and place somewhere near the top
            _title.SetPosition(-_title.Width / 2, 300);
        }

        private void InitializeMenu()
        {
            _menu = new VerticalMenu(0, 150, _input);
            Button startGame = new Button(
                delegate(object o, EventArgs e)
                {
                    _system.ChangeState("inner_game");
                },
                new Text("Start", _generalFont));


            Button exitGame = new Button(
                delegate(object o, EventArgs e)
                {
                    // Quit
                    System.Windows.Forms.Application.Exit();
                },
                new Text("Exit", _generalFont));

            _menu.AddButton(startGame);
            _menu.AddButton(exitGame);

        }

        public void Update(double elapsedTime) 
        {
            _menu.HandleInput();
        }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_title);
            _menu.Render(_renderer);
            _renderer.Render();
        }
    }

}
