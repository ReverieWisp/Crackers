using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Crackers
{
    public class EnvObject : MonoBehaviour
    {
#if UNITY_EDITOR
        // Inline inspector button
        [CustomEditor(typeof(EnvObject))]
        public class EnvRandomizerEditor : Editor
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
        [Range(0, 1)] [SerializeField] private float XVariation = 0.05f;
        [Range(0, 1)] [SerializeField] private float YVariation = 0.05f;
        [SerializeField] private bool FlipRandomlyOnX = false;

        public void RandomizeScale(float globalScalar = 1f)
        {
            // Scale
            float xScale = scaleBase + (Random.Range(0, XVariation) - XVariation / 2);
            float yScale = scaleBase + (Random.Range(0, YVariation) - YVariation / 2);

            bool doXFlip = false;
            if(FlipRandomlyOnX)
            {
                if(Random.Range(0, 1.0f) > 0.5f)
                {
                    doXFlip = true;
                }
            }

            this.transform.localScale = new Vector3((doXFlip ? -xScale : xScale) * globalScalar, yScale * globalScalar, 1);
        }
    }
}