using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    [RequireComponent(typeof(LineRenderer))]
    public class BorbLaunchPreview : CrackersMonoBehaviour
    {
        // Inspector Variables
        [SerializeField] private LineRenderer _line = null;
        [SerializeField] private TMPro.TextMeshPro _display = null;

        /// <summary>
        /// Collect line renderer and associated.
        /// </summary>
        void Awake()
        {
            if (_line == null)
            {
                _line = GetComponent<LineRenderer>();
            }

            if(_display == null)
            {
                _display = GetComponentInChildren<TMPro.TextMeshPro>();
            }
        }

        /// <summary>
        /// Given the specified value and angle, update the text.
        /// This is not derived from the pure line info even tho we have it because
        /// we may not actually be showing the user the truth.
        /// </summary>
        /// <param name="value">The string value to display as text</param>
        /// <param name="angle">The angle to tilt the text at</param>
        public void UpdateText(string value, float angle)
        {
            _display.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            _display.text = value;
        }

        /// <summary>
        /// Sets the desired world space locations of line preivew
        /// </summary>
        /// <param name="start">world-space start</param>
        /// <param name="end">world-space end</param>
        public void Set(Vector2 start, Vector2 end)
        {
            this.transform.position = start;
            _line.SetPosition(0, Vector2.zero);
            _line.SetPosition(1, end - start);
        }
    }
}