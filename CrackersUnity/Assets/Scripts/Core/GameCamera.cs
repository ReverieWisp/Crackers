using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// Manages game cameras
    /// </summary>
    public class GameCamera
    {
        public Camera Gameplay; // Ortho camera

        public GameCamera(Camera orthoCamera)
        {
            this.Gameplay = orthoCamera;
        }
    }
}