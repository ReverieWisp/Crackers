using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    public class SetCameraBounds : CrackersMonoBehaviour
    {
        [SerializeField] private EnvBounds _boundsToUse;

        public void Awake()
        {
            Game.Camera.CameraBounds = _boundsToUse;
        }
    }
}