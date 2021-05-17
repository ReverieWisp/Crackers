using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// Collects all aspects of the game for use as a singleton
    /// </summary>
    [System.Serializable]
    public class Game
    {
        public GameAssets Assets;
        public GameCamera Camera;
        public GameInput Input;

        public Game(Camera orthoCam, GameAssets assets)
        {
            Assets = assets;
            Camera = new GameCamera(orthoCam);
            Input = new GameInput(this);
        }

        // Returns if all is running well.
        public bool Update()
        {
            bool cleanUpdate = true;
            cleanUpdate &= Input.Update();

            return cleanUpdate;
        }
    }
}