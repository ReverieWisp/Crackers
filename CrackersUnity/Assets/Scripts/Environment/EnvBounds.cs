using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
#if UNITY_EDITOR
    [ExecuteAlways]
#endif
    public class EnvBounds : MonoBehaviour
    {
        [SerializeField] private Color BoundsColor = new Color(1, 0, 1, 0.5f);
        [SerializeField] protected bool DebugDrawBounds = true;
        [SerializeField] public Vector2 Bounds = Vector3.one;

        // Debug bounds drawing
        public void Update()
        {
            if (DebugDrawBounds)
            {
                float x = this.transform.position.x;
                float y = this.transform.position.y;

                Vector3 upperLeft = new Vector3(x - Bounds.x / 2, y + Bounds.y / 2, 0);
                Vector3 upperRight = new Vector3(x + Bounds.x / 2, y + Bounds.y / 2, 0);
                Vector3 lowerLeft = new Vector3(x - Bounds.x / 2, y - Bounds.y / 2, 0);
                Vector3 lowerRight = new Vector3(x + Bounds.x / 2, y - Bounds.y / 2, 0);

                Debug.DrawLine(upperLeft, upperRight, BoundsColor);
                Debug.DrawLine(upperRight, lowerRight, BoundsColor);
                Debug.DrawLine(lowerRight, lowerLeft, BoundsColor);
                Debug.DrawLine(lowerLeft, upperLeft, BoundsColor);
            }
        }
    }
}