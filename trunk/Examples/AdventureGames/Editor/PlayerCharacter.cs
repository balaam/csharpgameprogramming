using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Editor
{
    class PlayerCharacter
    {
        enum Direction
        {
            Up,
            Down,
            Left,
            Right,
        }
        Direction _direction = Direction.Down;

        AnimatedSprite _sprite = new AnimatedSprite();
        TextureManager _textureManager;
        public bool FollowingPath { get; set; }

        public PlayerCharacter(TextureManager textureManager)
        {
            _textureManager = textureManager;
            FollowingPath = false;
            SetToWalkUp();
        }

        // Take a normalized vector and return the direction it's facing.
        // There's a quite of lot of operations here I'm sure it could be made more efficent.
        private Direction VectorToDirection(Vector direction)
        {
            Vector up = new Vector(0, 1, 0);
            Vector down = new Vector(0, -1, 0);
            Vector left = new Vector(-1, 0, 0);
            Vector right = new Vector(1, 0, 0);

            double upDiff = Math.Acos(direction.DotProduct(up));
            double downDiff = Math.Acos(direction.DotProduct(down));
            double leftDiff = Math.Acos(direction.DotProduct(left));
            double rightDiff = Math.Acos(direction.DotProduct(right));

            double smallest = Math.Min(Math.Min(upDiff, downDiff), Math.Min(leftDiff, rightDiff));

            // yes there's a precidence if they're the same value, it doesn't matter
            if (smallest == upDiff)
            {
                return Direction.Up;
            }
            else if (smallest == downDiff)
            {
                return Direction.Down;
            }
            else if (smallest == leftDiff)
            {
                return Direction.Left;
            }
            else
            {
                return Direction.Right;
            }
        }

        private void SetToIdle()
        {
            _sprite.Texture = _textureManager.Get("pc_stand_still");
            _sprite.SetAnimation(3, 1);
            _sprite.SetWidth(_sprite.Texture.Width / 3);
            _sprite.SetHeight(_sprite.Texture.Height);
            _sprite.FlipHorizontal = false;
            _sprite.Pause = true;
        }

        private void SetToWalkUp()
        {
            _sprite.Texture = _textureManager.Get("pc_walk_up");
            _sprite.SetAnimation(5, 2);
            _sprite.SetWidth(_sprite.Texture.Width / 5);
            _sprite.SetHeight(_sprite.Texture.Height / 2);
            _sprite.Looping = true;
            _sprite.Pause = false;
            _sprite.Finished = false;
            _sprite.FlipHorizontal = false;
            _sprite.Speed = 0.1;
        }

        private void SetToWalkDown()
        {
            _sprite.Texture = _textureManager.Get("pc_walk_front");
            _sprite.SetAnimation(5, 2);
            _sprite.SetWidth(_sprite.Texture.Width / 5);
            _sprite.SetHeight(_sprite.Texture.Height / 2);
            _sprite.Looping = true;
            _sprite.Pause = false;
            _sprite.Finished = false;
            _sprite.FlipHorizontal = false;
            _sprite.Speed = 0.1;
        }


        private void SetToWalkRight()
        {
            _sprite.Texture = _textureManager.Get("pc_walk_side");
            _sprite.SetAnimation(5, 2);
            _sprite.SetWidth(_sprite.Texture.Width / 5);
            _sprite.SetHeight(_sprite.Texture.Height / 2);
            _sprite.Looping = true;
            _sprite.Pause = false;
            _sprite.Finished = false;
            _sprite.FlipHorizontal = false;
            _sprite.Speed = 0.1;
        }

        private void SetToWalkLeft()
        {
            _sprite.Texture = _textureManager.Get("pc_walk_side");
            _sprite.SetAnimation(5, 2);
            _sprite.SetWidth(_sprite.Texture.Width / 5);
            _sprite.SetHeight(_sprite.Texture.Height / 2);
            _sprite.Looping = true;
            _sprite.Pause = false;
            _sprite.Finished = false;
            _sprite.FlipHorizontal = true;
            _sprite.Speed = 0.1;
        }


        public void FaceDown()
        {
            SetToIdle();
            _sprite.SetFrame(1);
        }

        public void FaceUp()
        {
            SetToIdle();
            _sprite.SetFrame(2);
        }

        public void FaceRight()
        {
            SetToIdle();
            _sprite.SetFrame(0);
        }

        public void FaceLeft()
        {
            SetToIdle();
            _sprite.FlipHorizontal = true;
            _sprite.SetFrame(0);
        }

        public void Update(double elapsedTime)
        {
            if (FollowingPath)
            {
                UpdatePath(elapsedTime);
            }
            _sprite.Update(elapsedTime);
            
        }

        public void Render(Renderer renderer)
        {
            renderer.DrawSprite(_sprite);
        }

        /// <summary>
        /// The player position should be defined as the bottom of the sprite in the middle.
        /// </summary>
        /// <param name="position"></param>
        internal void SetPosition(Point position)
        {
            // floor position so at least if pixel resolution matches the won't wobble.
            _sprite.SetPosition(Math.Floor(position.X), Math.Floor(position.Y + _sprite.GetHeight() / 2));
        }

        internal Point GetPosition()
        {
            Vector position = _sprite.GetPosition();
            return new Point((float)position.X, (float)(position.Y - _sprite.GetHeight() / 2));
        }


        List<Point> _path;
        int _currentPathIndex = 1;
        double _walkSpeed = 75;
        Vector _travelPosition; // keep accurate position here
        internal void FollowPath(List<Point> path)
        {
            
            FollowingPath = true;
            _path = path;
            _travelPosition = new Vector(GetPosition().X, GetPosition().Y, 0);

            // Setup the initial animation
            Vector direction = Vector.Normalize(new Vector(path[0].X, path[0].Y, 0) - new Vector(path[1].X, path[1].Y, 0));
            _direction = VectorToDirection(direction);
            ChangeWalkingAnimation();


        }
       
        internal void UpdatePath(double elapsedTime)
        {
            System.Diagnostics.Debug.Assert(_currentPathIndex != _path.Count);

            Vector position = _travelPosition;
            Vector target = new Vector(_path[_currentPathIndex].X, _path[_currentPathIndex].Y, 0);
            Vector fromPositionToTarget = target - position;
            Vector direction = Vector.Normalize(fromPositionToTarget);

            Direction walkDirection = VectorToDirection(direction);
            if (_direction != walkDirection)
            {
                _direction = walkDirection;
                ChangeWalkingAnimation();
            }

            Vector distanceToMove = (direction * _walkSpeed) * elapsedTime;

            if (distanceToMove.Length() <= fromPositionToTarget.Length())
            {
                position += distanceToMove;
            }
            else
            {
                position = target;
            }

            

            _travelPosition = position;
            SetPosition(new Point((float)position.X, (float)position.Y));

            if ((target - position).Length() <= 1)
            {
                _currentPathIndex++;
                if (_currentPathIndex == _path.Count)
                {
                    _currentPathIndex = 1;
                    FollowingPath = false;
                    _path.Clear();
                    StopWalkingAnimation();
                    return;
                }
            }
 
        }

        private void StopWalkingAnimation()
        {
            if (_direction == Direction.Down)
            {
                FaceDown();
            }
            else if (_direction == Direction.Up)
            {
                FaceUp();
            }
            else if (_direction == Direction.Left)
            {
                FaceLeft();
            }
            else
            {
                FaceRight();
            }
        }

        private void ChangeWalkingAnimation()
        {
            if (_direction == Direction.Up)
            {
                SetToWalkUp();
            }
            else if (_direction == Direction.Down)
            {
                SetToWalkDown();
            }
            else if (_direction == Direction.Left)
            {
                SetToWalkLeft();
            }
            else
            {
                SetToWalkRight();
            }
        }

/*
        /// <summary>
        /// Player position is a vector, it's being compared to floats
        /// There's going to be accuracy lost of the compare method needs to have a tolerance.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool HasReachedPoint(Point point)
        {
            float distance = Distance(_travelPosition, point);
            return (distance <= 1); // within a pixel
        }

        private float Distance(Point point1, Point point2)
        {
            float xd = point2.X - point1.X;
            float yd = point2.Y - point1.Y;
	        return (float)Math.Sqrt(xd*xd + yd*yd);

        }
 * */
    }
}
