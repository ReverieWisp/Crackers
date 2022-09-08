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

        public bool MenuIsVisible = true;
        public System.Action CrackersAcquired;

        public Game(Camera orthoCam, GameAssets assets)
        {
            Assets = assets;
            Camera = new GameCamera(orthoCam);
            Input = new GameInput(this);
        }

        /// <summary>
        /// Returns if all is running well. This is the primary update loop.
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            bool cleanUpdate = true;
            
            cleanUpdate &= Input.Update();
            cleanUpdate &= Camera.Update();

            return cleanUpdate;
        }

        /// <summary>
        /// Called only at regular intervals
        /// </summary>
        public void FixedUpdate()
        {
            Camera?.FixedUpdate();
        }
    }
}