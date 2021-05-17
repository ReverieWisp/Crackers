using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// Contains static reference to the game, and handles startup/teardown.
    /// </summary>
    public class Core : MonoBehaviour
    {
        [Header("Global Links")]
        [SerializeField] private Camera _gameplayCamera;
        [SerializeField] private GameAssets _assets;

        /// <summary>
        /// Game singleton variable and setup
        /// </summary>
        public static Game Game { get; private set; }
        private void Awake()
        {
            Game = new Game(_gameplayCamera, _assets);
        }

        /// <summary>
        /// Update handling, etc. Use the return value of the game to determine
        /// if we need to be leaving now.
        /// </summary>
        private void Update()
        {
            if(!Game.Update())
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}