using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    [ExecuteAlways]
    public class EnvBounds : MonoBehaviour
    {
        [Header("Bounds Config")]
        [SerializeField] private Color boundsColor = new Color(1, 0, 1, 0.5f);
        [SerializeField] protected bool debugDrawBounds = true;
        [SerializeField] public Vector2 bounds = Vector3.one;

#if UNITY_EDITOR
        // Debug bounds drawing
        public void Update()
        {
            if (debugDrawBounds)
            {
                float x = this.transform.position.x;
                float y = this.transform.position.y;

                Vector3 upperLeft = new Vector3(x - bounds.x / 2, y + bounds.y / 2, 0);
                Vector3 upperRight = new Vector3(x + bounds.x / 2, y + bounds.y / 2, 0);
                Vector3 lowerLeft = new Vector3(x - bounds.x / 2, y - bounds.y / 2, 0);
                Vector3 lowerRight = new Vector3(x + bounds.x / 2, y - bounds.y / 2, 0);

                Debug.DrawLine(upperLeft, upperRight, boundsColor);
                Debug.DrawLine(upperRight, lowerRight, boundsColor);
                Debug.DrawLine(lowerRight, lowerLeft, boundsColor);
                Debug.DrawLine(lowerLeft, upperLeft, boundsColor);
            }
        }
#endif
    }
}