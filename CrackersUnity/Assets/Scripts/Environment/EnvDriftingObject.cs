using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Crackers
{
    public class EnvDriftingObject : EnvObject
    {
#if UNITY_EDITOR
        // Inline inspector button
        [UnityEditor.CanEditMultipleObjects]
        [UnityEditor.CustomEditor(typeof(EnvDriftingObject))]
        public class EnvDriftingObjectEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                if (GUILayout.Button("Randomize"))
                {
                    (target as EnvObject).RandomizeScale();
                }

                GUILayout.Space(20);

                base.OnInspectorGUI();
            }
        }
#endif

        // Inspector variables
        [Header("Drift Variables")]
        [SerializeField] private EnvBounds _resetBounds;
        [Tooltip("DT normalized speed in left direction")] [SerializeField] private float _speed = 10f;

        // Internal variables
        private SpriteRenderer _sprite;
        private float _objWidth = 0;
        private float _objPivotX = 0;

        private void Start()
        {
            SpriteRenderer spr = this.GetComponent<SpriteRenderer>();
            if(spr != null)
            {
                _objWidth = spr.sprite.rect.width / spr.sprite.pixelsPerUnit * Mathf.Abs(this.transform.localScale.x);
                _objPivotX = spr.sprite.pivot.x / spr.sprite.pixelsPerUnit * Mathf.Abs(this.transform.localScale.x);
            }
        }

        private void Update()
        {
            // Slide
            transform.position += new Vector3(Time.deltaTime * -Mathf.Abs(_speed), 0, 0);

            // See if we need to loop! This is when we're fully off the screen to the left.
            float rightOfObject = _objWidth - _objPivotX;
            float leftOfObject = (1 - _objPivotX / _objWidth) * _objWidth;
            float leftResetThresholdX = _resetBounds.transform.position.x - _resetBounds.Bounds.x / 2;
            float rightResetTargetX = _resetBounds.transform.position.x + _resetBounds.Bounds.x / 2 + leftOfObject;

            if (this.transform.position.x + rightOfObject < leftResetThresholdX)
            {
                this.transform.position = new Vector3(rightResetTargetX, this.transform.position.y, 0);
            }
        }
    }
}