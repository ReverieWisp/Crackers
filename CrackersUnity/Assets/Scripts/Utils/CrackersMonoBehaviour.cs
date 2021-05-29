using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// Utility base class used in place of vanilla MonoBehaviour (Tho it is one)
    /// </summary>
    public class CrackersMonoBehaviour : MonoBehaviour
    {
        public Game Game => Core.Game;

        public bool IsShutdown => !Core.HasGame();
    }
}