using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Tao.OpenGl;

namespace Chapter10_2
{
    class StartMenuState : IGameObject
    {
        Renderer _renderer = new Renderer();
        Text _title;

        public StartMenuState(Engine.Font titleFont)
        {
            _title = new Text("Shooter", titleFont);
            _title.SetColor(new Color(0, 0, 0, 1));
            // Centerre on the x and place somewhere near the top
            _title.SetPosition(-_title.Width / 2, 300);
        }

        public void Update(double elapsedTime) { }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 0);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_title);
            _renderer.Render();
        }
    }

}
