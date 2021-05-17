using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// Contains static reference to the game, and handles startup/teardown
    /// </summary>
    public class Core : MonoBehaviour
    {
        [Header("Global Links")]
        [SerializeField] private Camera _gameplayCamera;
        [SerializeField] private GameAssets _assets;

        // Game singleton setup
        public static Game Game { get; private set; }
        private void Awake()
        {
            Game = new Game(_gameplayCamera, _assets);
        }


        // Update handling, etcs
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