using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Engine.PathFinding;
using Tao.OpenGl;
using System.Windows.Forms;

namespace Editor
{
    class EditWalkArea : IGameObject
    {
        Input       _input;
        ToolStrip   _toolStrip;
        StateSystem _editState = new StateSystem();
        Scene       _scene;

        public EditWalkArea(Input input, ToolStrip toolstrip, Scene scene)
        {
            _input = input;
            _toolStrip = toolstrip;
            _scene = scene;
            InitEditStates();
            AddToolStripCallbacks();
        }

        private void InitEditStates()
        {
            _editState.AddState("default", new DefaultEditState(_input, _scene.NavMesh));
            _editState.AddState("add_vertex", new AddVertexState(_input, _scene.NavMesh));
            _editState.AddState("add_link", new AddLinkState(_input, _scene.NavMesh));
            _editState.AddState("add_polygon", new AddPolygonState(_input, _scene.NavMesh));
            _editState.ChangeState("default");
        }


        private void AddToolStripCallbacks()
        {
            // Assumes certain buttons are present would be better
            // to add these buttons programmatically here but that makes them a little more bother 
            // to easily edit.
            _toolStrip.Items["_defaultToolStripButton"].Click += new EventHandler(OnClickDefaultMode);
            _toolStrip.Items["_polygonAddToolStripButton"].Click += new EventHandler(OnClickPolygonAddMode);
            _toolStrip.Items["_linkToolStripButton"].Click += new EventHandler(OnClickLinkMode);
            _toolStrip.Items["_addVertexToolStripButton"].Click += new EventHandler(OnClickAddVertexMode);
        }

        void OnClickAddVertexMode(object sender, EventArgs e)
        {
            UncheckAllToolstripButtons();
            ((ToolStripButton)_toolStrip.Items["_addVertexToolStripButton"]).CheckState = CheckState.Checked;
            _editState.ChangeState("add_vertex");
        }

        void OnClickLinkMode(object sender, EventArgs e)
        {
            UncheckAllToolstripButtons();
            ((ToolStripButton)_toolStrip.Items["_linkToolStripButton"]).CheckState = CheckState.Checked;
            _editState.ChangeState("add_link");
        }

        void OnClickPolygonAddMode(object sender, EventArgs e)
        {
            UncheckAllToolstripButtons();
            ((ToolStripButton)_toolStrip.Items["_polygonAddToolStripButton"]).CheckState = CheckState.Checked;
            _editState.ChangeState("add_polygon");
        }

        void OnClickDefaultMode(object sender, EventArgs e)
        {
            UncheckAllToolstripButtons();
            ((ToolStripButton)_toolStrip.Items["_defaultToolStripButton"]).CheckState = CheckState.Checked;
            _editState.ChangeState("default");
        }

        private void UncheckAllToolstripButtons()
        {
            foreach (ToolStripItem item in _toolStrip.Items)
            {
                ToolStripButton button = item as ToolStripButton;
                if (button != null)
                {
                    button.CheckState = CheckState.Unchecked;
                }
            }
        }

        public void Activated()
        {
        }

        public void Update(double elapsedTime)
        {
            _scene.Update(elapsedTime);
            _editState.Update(elapsedTime);
        }

        public void Render()
        {
            GLUtil.Clear(new Color(0.5f, 0.5f, 0.5f, 0));
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            {
                _scene.Render();
            }
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            RenderNavMesh(_scene.NavMesh);
            
            _editState.Render();
        }

        private void RenderNavMesh(NavMesh navMesh)
        {
            GLUtil.SetColor(new Color(0f, 0f, 1.0f, 0.5f));
            navMesh.PolygonList.ForEach(x => GLUtil.RenderPolygonFilled(x));            
            GLUtil.SetColor(new Color(0.5f, 0.1f, 0.1f, 1f));
            navMesh.PolygonList.ForEach(x => GLUtil.RenderPolygon(x));
            
        }

    }
}
