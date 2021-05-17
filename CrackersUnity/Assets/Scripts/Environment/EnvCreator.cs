using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Crackers
{
    [ExecuteAlways]
    public class EnvCreator : EnvBounds
    {
#if UNITY_EDITOR
        // Inline inspector button
        [CustomEditor(typeof(EnvCreator))]
        public class EnvCreatorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                if (GUILayout.Button("Regenerate"))
                {
                    (target as EnvCreator).ClearChildren();
                    (target as EnvCreator).Populate();
                }

                GUILayout.Space(20);

                base.OnInspectorGUI();
            }
        }
#endif

        [SerializeField] private int numObjects = 10;
        [SerializeField] private List<EnvObject> spawnable = new List<EnvObject>();

        public void Populate()
        {
            if (spawnable.Count > 0)
            {
                for (int i = 0; i < numObjects; ++i)
                {
                    // Collect new object information
                    EnvObject template = spawnable[Random.Range(0, spawnable.Count)];
                    float x = Random.Range(0, bounds.x) - bounds.x / 2;
                    float y = Random.Range(0, bounds.y) - bounds.y / 2;

                    // Create and position object
                    EnvObject newObj = GameObject.Instantiate(template, this.transform);
                    newObj.name = "generated environment piece";
                    newObj.transform.localPosition = new Vector3(x, y, 0);

                    newObj.RandomizeScale();
                }
            }
            else
            {
                Debug.Log("You need to specify something that's possible to even spawn in the 'Spawnable' list");
            }
        }

        // Destroys absolutely all children (identified via transforms) parented to this object in the hierarchy
        public void ClearChildren()
        {
            Transform[] children = this.gameObject.GetComponentsInChildren<Transform>(includeInactive: true);
            if(children != null)
            {
                for(int i = children.Length - 1; i >= 0; i--)
                {
                    if (this.transform != children[i])
                    {
                        GameObject.DestroyImmediate(children[i].gameObject);
                        children[i] = null;
                    }
                }
            }
        }
    }
}