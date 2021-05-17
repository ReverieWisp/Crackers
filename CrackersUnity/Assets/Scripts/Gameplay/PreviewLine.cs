using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    [RequireComponent(typeof(LineRenderer))]
    public class PreviewLine : CrackersMonoBehaviour
    {
        [SerializeField] private LineRenderer _line = null;
        [SerializeField] private TMPro.TextMeshPro _display = null;

        // Start is called before the first frame update
        void Awake()
        {
            if (_line == null)
            {
                _line = GetComponent<LineRenderer>();
            }
        }

        public void UpdateText(float value, float angle)
        {
            _display.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            _display.text = string.Format("{0:0.0}", value);
        }

        public void Set(Vector2 start, Vector2 end)
        {
            _line.SetPosition(0, start);
            _line.SetPosition(1, end);
        }
    }
}