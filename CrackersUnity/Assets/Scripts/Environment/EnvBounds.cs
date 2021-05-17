﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    [ExecuteAlways]
    public class EnvBounds : MonoBehaviour
    {
        [Header("Bounds Config")]
        [SerializeField] private Color _boundsColor = new Color(1, 0, 1, 0.5f);
        [SerializeField] protected bool _debugDrawBounds = true;
        [SerializeField] public Vector2 _bounds = Vector3.one;

#if UNITY_EDITOR
        // Debug bounds drawing
        public void Update()
        {
            if (_debugDrawBounds)
            {
                float x = this.transform.position.x;
                float y = this.transform.position.y;

                Vector3 upperLeft = new Vector3(x - _bounds.x / 2, y + _bounds.y / 2, 0);
                Vector3 upperRight = new Vector3(x + _bounds.x / 2, y + _bounds.y / 2, 0);
                Vector3 lowerLeft = new Vector3(x - _bounds.x / 2, y - _bounds.y / 2, 0);
                Vector3 lowerRight = new Vector3(x + _bounds.x / 2, y - _bounds.y / 2, 0);

                Debug.DrawLine(upperLeft, upperRight, _boundsColor);
                Debug.DrawLine(upperRight, lowerRight, _boundsColor);
                Debug.DrawLine(lowerRight, lowerLeft, _boundsColor);
                Debug.DrawLine(lowerLeft, upperLeft, _boundsColor);
            }
        }
#endif
    }
}