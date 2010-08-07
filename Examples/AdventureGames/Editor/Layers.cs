using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace Editor
{
    public partial class Layers : Form
    {
        Scene _scene;
        TextureManager _textureManager;

        public Layers(Scene scene, TextureManager textureManager)
        {
            InitializeComponent();
            _scene = scene;
            _textureManager = textureManager;
        }

        private void AddLayerClicked(object sender, EventArgs e)
        {
            // For now all layers are assumed to be some type of image file.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Load the image in as a new layer.
                Layer layer = new Layer("Layer " + _layerListView.Items.Count);
                if (_textureManager.Exists(openFileDialog1.FileName) == false)
                {
                    _textureManager.LoadTexture(openFileDialog1.FileName, openFileDialog1.FileName);
                }
                layer.SetImage(_textureManager.Get(openFileDialog1.FileName));
                _scene.AddLayer(layer);
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = "Layer " + _layerListView.Items.Count;
                listViewItem.Name = "someuniqueid";
                _layerListView.Items.Add(listViewItem);
            }
        }
    }
}
