using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Engine.Input
{
    public class Mouse
    {
        Form _parentForm;
        Control _openGLControl;

        public Point Position { get; set; }

        bool _leftClickDetect = false;
        bool _rightClickDetect = false;
        bool _middleClickDetect = false;

        public bool MiddlePressed { get; private set; }
        public bool LeftPressed { get; private set; }
        public bool RightPressed { get; private set; }

        public bool MiddleHeld { get; private set; }
        public bool LeftHeld { get; private set; }
        public bool RightHeld { get; set; }

        public Mouse(Form form, Control openGLControl)
        {
            _parentForm = form;
            _openGLControl = openGLControl;
            _openGLControl.MouseClick += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _leftClickDetect = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    _rightClickDetect = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    _middleClickDetect = true;
                }
            };

            _openGLControl.MouseDown += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    LeftHeld = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RightHeld = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    MiddleHeld = true;
                }
            };

            _openGLControl.MouseUp += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    LeftHeld = false;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RightHeld = false;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    MiddleHeld = false;
                }
            };

            _openGLControl.MouseLeave += delegate(object obj, EventArgs e)
            {
                // If you move the mouse out the window then release all held buttons
                LeftHeld = false;
                RightHeld = false;
                MiddleHeld = false;
            };
        }

        public void Update(double elapsedTime)
        {
            UpdateMousePosition();
            UpdateMouseButtons();
        }

        private void UpdateMousePosition()
        {
            System.Drawing.Point mousePos = Cursor.Position;
            mousePos = _openGLControl.PointToClient(mousePos);

            // Now use our point definition, 
            Engine.Point adjustedMousePoint = new Engine.Point();
            adjustedMousePoint.X = (float)mousePos.X - ((float)_parentForm.ClientSize.Width / 2);
            adjustedMousePoint.Y = ((float)_parentForm.ClientSize.Height / 2) - (float)mousePos.Y;
            Position = adjustedMousePoint;
        }

        private void UpdateMouseButtons()
        {
            // Reset buttons
            MiddlePressed = false;
            LeftPressed = false;
            RightPressed = false;

            if (_leftClickDetect)
            {
                LeftPressed = true;
                _leftClickDetect = false;
            }

            if (_rightClickDetect)
            {
                RightPressed = true;
                _rightClickDetect = false;
            }

            if (_middleClickDetect)
            {
                MiddlePressed = true;
                _middleClickDetect = false;
            }
        }

    }
}
