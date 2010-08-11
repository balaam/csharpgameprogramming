using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;
using Engine.Input;
using Tao.OpenGl;
using Engine.PathFinding;

namespace Editor
{
    class TestWalkState : IGameObject
    {

        Input       _input;
        ToolStrip   _toolstrip;
        Scene       _scene;
        Renderer    _renderer = new Renderer();


        bool            _canWalkMousePosition = false;
        List<Point>     _path = new List<Point>();
        PathFinder      _pathFinder = new PathFinder();

        TextureManager _textureManager;
        PlayerCharacter _playerCharacter;
        Random _random = new Random();

        public TestWalkState(Input input, ToolStrip toolstrip, Scene scene, TextureManager textureManager)
        {
            _input = input;
            _toolstrip = toolstrip;
            _scene = scene;
            _textureManager = textureManager;
            _playerCharacter = new PlayerCharacter(_textureManager);
        }

        public void Activated()
        {
            // Put the character in the center of a random polygon.
            ConvexPolygon randomPoly = _scene.NavMesh.PolygonList[_random.Next(_scene.NavMesh.PolygonList.Count)];
            _playerCharacter.SetPosition(randomPoly.CalculateCentroid());
            _playerCharacter.FaceDown();
        }

        public void Update(double elapsedTime)
        {
            _canWalkMousePosition = _scene.NavMesh.PolygonList.Any(x => x.Intersects(_input.Mouse.Position));
            if (_input.Mouse.LeftPressed && _canWalkMousePosition && _playerCharacter.FollowingPath == false)
            {
                 _path = _pathFinder.GetPath(_playerCharacter.GetPosition(), _input.Mouse.Position, _scene.NavMesh);
                if(_path.Count >= 2)
                {
                    _playerCharacter.FollowPath(_path);
                }
            }

            _playerCharacter.Update(elapsedTime);
        }

        public void Render()
        {
            GLUtil.Clear(new Color(0, 0, 0, 1));

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            foreach (Layer layer in _scene.Layers)
            {
                layer.Render(_renderer);
            }

       
       

        
      

            _renderer.Render();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            if (_canWalkMousePosition)
            {
                GLUtil.DrawFilledCircle(_input.Mouse.Position, 10, new Color(0, 1, 0, 1));
            }
            else
            {
                GLUtil.DrawFilledCircle(_input.Mouse.Position, 10, new Color(1, 0, 0, 1));
            }

            // Draw the basic nav meshes
            GLUtil.SetColor(new Color(1, 0, 0, 1));
            _scene.NavMesh.PolygonList.ForEach(x => GLUtil.RenderPolygon(x));
            GLUtil.SetColor(new Color(1, 1, 1, 1));
            Gl.glLineWidth(4);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            {
                foreach (Point point in _path)
                {

                    GLUtil.DrawPointVertex(point);

                }
            }
            Gl.glEnd();
            Gl.glLineWidth(1);

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            _playerCharacter.Render(_renderer);
            _renderer.Render();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }
    }
}
