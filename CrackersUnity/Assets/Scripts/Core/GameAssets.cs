using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// Contains references to all the prefabs or in-scene objects expected.
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "gameAssets", menuName = "Birb Cracker Game/Asset Object", order = 2)]
    public class GameAssets : ScriptableObject
    {
        [Header("Prefabs")]
        public LineRenderer LineTemplate;
        public Borb Borb;

        public T Create<T>(T toMake) where T : UnityEngine.Object
        {
            return GameObject.Instantiate(toMake);
        }

        public void Destroy<T>(T toDestroy, bool now = false) where T : UnityEngine.Object
        {
            if (toDestroy != null)
            {
                if (now)
                {
                    GameObject.DestroyImmediate(toDestroy);
                }
                else
                {
                    GameObject.Destroy(toDestroy);
                }
            }
        }
    }
}