using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.PathFinding;

namespace Editor
{
    public class Scene
    {
        Renderer _renderer = new Renderer();
        List<Layer> _layerList = new List<Layer>();
        NavMesh _navMesh = new NavMesh();

        public NavMesh NavMesh
        {
            get { return _navMesh; }
            set { _navMesh = value; }
        }

        public List<Layer> Layers
        {
            get { return _layerList; }
            set { _layerList = value; }
        }

        public void AddLayer(Layer layer)
        {
            _layerList.Add(layer);
        }

        public void Render()
        {
            _layerList.ForEach(x => x.Render(_renderer));
            _renderer.Render();
        }

        public void Update(double elapsedTime)
        {
            _layerList.ForEach(x => x.Update(elapsedTime));
        }

        public void LoadFromXML(System.Xml.XmlTextReader xmlReader)
        {
            if (xmlReader.Name.ToLower() != "scene")
            {
                // No scene data, this is a problem.
                System.Windows.Forms.MessageBox.Show("Scene data wasn't found.");
                return;
            }
        }

    }
}
