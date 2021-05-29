using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    public class CoroutineUtility : MonoBehaviour
    {
        private static CoroutineUtility _instance;
        public static CoroutineUtility Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject thisObj = new GameObject("Coroutine Utility");
                    _instance = thisObj.AddComponent<CoroutineUtility>();
                    DontDestroyOnLoad(thisObj);
                }

                return _instance;
            }
        }
    }
}
