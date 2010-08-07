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
        Point _previousPosition = new Point();
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

        private bool WheelDeltaChanged { get; set; }
        public int Wheel { get; set; }
        /// <summary>
        /// The direction and amount that the mouse pointe has been moved in one frame.
        /// </summary>
        Vector _moveDelta = new Vector();
        public Vector MoveDelta 
        {
            get
            {
                return _moveDelta;
            }
        }

        public Mouse(Form form, Control openGLControl)
        {
            _parentForm = form;
            _openGLControl = openGLControl;
            Position = new Point();
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

            _openGLControl.MouseWheel += delegate(object obj, MouseEventArgs e)
            {
                WheelDeltaChanged = true;
                Wheel = e.Delta;
            };
        }

        public void Update(double elapsedTime)
        {
            Update(elapsedTime, Vector.Zero, _parentForm.ClientSize.Width, _parentForm.ClientSize.Height);
        }

        /// <summary>
        /// Update with information about the opengl view port size.
        /// </summary>
        public void Update(double elapsedTime, Vector origin, double glWidth, double glHeight)
        {
            _previousPosition = Position;
            Position = GetMousePosition(origin, glWidth, glHeight);
            _moveDelta.X = Position.X - _previousPosition.X;
            _moveDelta.Y = Position.Y - _previousPosition.Y;
            UpdateMouseButtons();
            UpdateMouseWheel();
        }

        private void UpdateMouseWheel()
        {
            if (WheelDeltaChanged == true)
            {
                WheelDeltaChanged = false;
            }
            else
            {
                Wheel = 0;
            }
        }

        private Point GetMousePosition(Vector origin, double width, double height)
        {
            System.Drawing.Point mousePos = Cursor.Position;
            mousePos = _openGLControl.PointToClient(mousePos);

            double x = (double)mousePos.X / ((double)_openGLControl.ClientSize.Width);
            double y = (double)mousePos.Y / ((double)_openGLControl.ClientSize.Height);

            Engine.Point adjustedMousePoint = new Engine.Point();
            adjustedMousePoint.X = (float)Interpolation.Lerp(x, 0, 1, origin.X - (width / 2), origin.X + (width / 2));
            adjustedMousePoint.Y = (float)Interpolation.Lerp(y, 0, 1, origin.Y + (height / 2),origin.Y - (height / 2));
            return adjustedMousePoint;
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
