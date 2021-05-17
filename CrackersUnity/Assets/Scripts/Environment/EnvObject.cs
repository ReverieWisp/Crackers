using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    public class EnvObject : MonoBehaviour
    {
#if UNITY_EDITOR
        // Inline inspector button
        [UnityEditor.CanEditMultipleObjects]
        [UnityEditor.CustomEditor(typeof(EnvObject))]
        public class EnvRandomizerEditor : UnityEditor.Editor
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

        [Header("Scale settings")]
        [SerializeField] private float scaleBase = 0.1f;
        [Range(0, 1)] [SerializeField] private float xVariation = 0.05f;
        [Range(0, 1)] [SerializeField] private float yVariation = 0.05f;
        [SerializeField] private bool flipRandomlyOnX = false;

        public void RandomizeScale(float globalScalar = 1f)
        {
            // Scale
            float xScale = scaleBase + (Random.Range(0, xVariation) - xVariation / 2);
            float yScale = scaleBase + (Random.Range(0, yVariation) - yVariation / 2);

            bool doXFlip = false;
            if(flipRandomlyOnX)
            {
                if(Random.Range(0, 1.0f) > 0.5f)
                {
                    doXFlip = true;
                }
            }

            this.transform.localScale = new Vector3((doXFlip ? -xScale : xScale) * globalScalar, yScale * globalScalar, 0);
        }
    }
}