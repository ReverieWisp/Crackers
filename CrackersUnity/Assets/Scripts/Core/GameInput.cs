using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// All things input! Note that input may come from many sources.
    /// </summary>
    [System.Serializable]
    public class GameInput
    {
        // Usable externally
        public System.Action<Vector2> OnPrimaryActionStart;
        public System.Action<Vector2> OnPrimaryActionPersist;
        public System.Action<Vector2> OnPrimaryActionAccept;
        public System.Action<Vector2> OnPrimaryActionCancel_NEVER_CALLED;

        public Vector2 InputPosition;

        // Internal
        private GameCamera camera;

        public GameInput(Game game)
        {
            this.camera = game.Camera;
            InputPosition = UnityEngine.Input.mousePosition;
        }

        public bool Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnPrimaryActionStart?.Invoke(InputInWorld());
            }

            if (Input.GetMouseButton(0))
            {
                OnPrimaryActionPersist?.Invoke(InputInWorld());
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnPrimaryActionAccept?.Invoke(InputInWorld());
            }

            InputPosition = UnityEngine.Input.mousePosition;

            return true;
        }

        private Vector2 InputInWorld()
        {
            return camera.Gameplay.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}