using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] private EnvBounds resetBounds;
        [Tooltip("DT normalized speed in left direction")] [SerializeField] private float speed = 10f;

        // Internal variables
        private SpriteRenderer sprite;
        private float objWidth = 0;
        private float objPivotX = 0;

        private void Start()
        {
            SpriteRenderer spr = this.GetComponent<SpriteRenderer>();
            if(spr != null)
            {
                objWidth = spr.sprite.rect.width / spr.sprite.pixelsPerUnit * Mathf.Abs(this.transform.localScale.x);
                objPivotX = spr.sprite.pivot.x / spr.sprite.pixelsPerUnit * Mathf.Abs(this.transform.localScale.x);
            }
        }

        private void Update()
        {
            // Slide
            transform.position += new Vector3(Time.deltaTime * -Mathf.Abs(speed), 0, 0);

            // See if we need to loop! This is when we're fully off the screen to the left.
            float rightOfObject = objWidth - objPivotX;
            float leftOfObject = (1 - objPivotX / objWidth) * objWidth;
            float leftResetThresholdX = resetBounds.transform.position.x - resetBounds.bounds.x / 2;
            float rightResetTargetX = resetBounds.transform.position.x + resetBounds.bounds.x / 2 + leftOfObject;

            if (this.transform.position.x + rightOfObject < leftResetThresholdX)
            {
                this.transform.position = new Vector3(rightResetTargetX, this.transform.position.y, 0);
            }
        }
    }
}