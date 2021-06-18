using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// Manages game cameras
    /// </summary>
    public class GameCamera
    {
        public Camera Gameplay;            // Ortho camera that renders the game world
        public EnvBounds CameraBounds;     // Bounds to keep the gameplay camera within
        public Vector2 PixelsPerWorldUnit; // For every world unit, this is how many pixels in a given direciton is one unit.

        private float _aspectRatio;                   // The screen aspect ratio, width/height
        private float _velocity = 0;                  // The current velocity update
        private float _velocityDrag = 0.85f;          // The per-update drag change to velocity.
        private float _velocityScalar = 10;           // A speed scalar to apply the velocity per update.
        private float _velocityStopThreshold = 0.02f; // The reshold for snapping the velocity to 0

        public GameCamera(Camera orthoCamera)
        {
            this.Gameplay = orthoCamera;
            _aspectRatio = (float)Screen.width / Screen.height;
            PixelsPerWorldUnit.x = this.Gameplay.orthographicSize * 2 * _aspectRatio;
            PixelsPerWorldUnit.y = this.Gameplay.orthographicSize * 2;
            PixelsPerWorldUnit.x = Screen.width / PixelsPerWorldUnit.x;
            PixelsPerWorldUnit.x = Screen.height / PixelsPerWorldUnit.y;
        }

        /// <summary>
        /// Set a velocity of the X position of the camera. The velocity is subject to drag.
        /// </summary>
        /// <param name="xVelocity"></param>
        public void SetCameraVelocity(float xVelocity)
        {
            _velocity = xVelocity;
        }

        /// <summary>
        /// Repositions the orthographic game camera, respecting the camera bounds if present.
        /// </summary>
        /// <param name="target"></param>
        public void SetCameraX(float target)
        {
            Vector3 pos = Gameplay.transform.position;
            pos.x = target;
            pos = ClampCameraX(pos);
            Gameplay.transform.position = pos;
        }

        /// <summary>
        /// Clamp the camera X position 
        /// </summary>
        /// <param name="toClamp"></param>
        /// <returns></returns>
        private Vector3 ClampCameraX(Vector3 toClamp)
        {
            if (CameraBounds != null)
            {
                float camHalfWidth = this.Gameplay.orthographicSize * _aspectRatio;

                float left = -(CameraBounds.Bounds.x / 2) + camHalfWidth;
                float right = CameraBounds.Bounds.x / 2 - camHalfWidth;

                if (toClamp.x < left)
                {
                    toClamp.x = left;
                }

                if (toClamp.x > right)
                {
                    toClamp.x = right;
                }
            }

            return toClamp;
        }

        /// <summary>
        /// Different way to call SetCameraX, with an offset instead of a new value.
        /// This calls SetCameraX under the hood
        /// </summary>
        /// <param name="offset">X position offset from the current position</param>
        public void OffsetCameraX(float offset)
        {
            Vector3 pos = Gameplay.transform.position;
            SetCameraX(pos.x + offset);
        }

        /// <summary>
        /// A variable rate update for the game camera system
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            // Velocity application
            if(Mathf.Abs(_velocity) < _velocityStopThreshold)
            {
                _velocity = 0;
            }
            else
            {
                float offset = _velocity * _velocityScalar * Time.deltaTime;
                OffsetCameraX(offset);
            }

            // Bounds enforcement
            Vector3 pos = Gameplay.transform.position;
            Gameplay.transform.position = ClampCameraX(pos);

            return true;
        }

        /// <summary>
        /// A fixed-time update
        /// </summary>
        public void FixedUpdate()
        {
            _velocity *= _velocityDrag;
        }
    }
}