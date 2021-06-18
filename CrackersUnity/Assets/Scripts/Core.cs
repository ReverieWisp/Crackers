using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] private string _toLoadFirst = "Menu";

        private static bool _performingJTL = false;
        private static string _sceneJTL = "";
        private static Game _game = null;
        private static string _currentLoaded = null;

        public static IEnumerator PerformJustTooLateLoad()
        {
            if (_performingJTL)
            {
                yield break;
            }

            _performingJTL = true;
            _sceneJTL = SceneManager.GetActiveScene().name;
            Debug.LogWarning($"Expect errors, and hold onto your socks - we're performing a just-too-late load back to your scene: {_sceneJTL}");

            yield return null;

            AsyncOperation coreLoad = SceneManager.LoadSceneAsync("Core");

            while (!coreLoad.isDone)
            {
                yield return null;
            }

            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(_sceneJTL, LoadSceneMode.Additive);

            while(!sceneLoad.isDone)
            {
                yield return null;
            }

            EditorTools.ClearEditorLog();
            Debug.LogWarning("Just-too-late reload finished!");
        }

        /// <summary>
        /// Game singleton variable and setup
        /// </summary>
        public static Game Game
        {
            get
            {
                if (_game == null)
                {
                    CoroutineUtility.Instance.StartCoroutine(PerformJustTooLateLoad());
                }

                return _game;
            }
        }
        
        public static bool HasGame()
        {
            return _game != null;
        }

        private void Awake()
        {
            if (_game == null)
            {
                _game = new Game(_gameplayCamera, _assets);
            }
        }

        private void Start()
        {
            if (!_performingJTL)
            {
                LoadLevel(_toLoadFirst);
            }
        }

        public static void LoadLevel(string toLoad)
        {
            AsyncOperation unload = null;
            if(!string.IsNullOrEmpty(_currentLoaded))
            {
                unload = SceneManager.UnloadSceneAsync(_currentLoaded, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            }

            if(unload != null)
            {
                unload.completed += (_)=>
                {
                    SceneManager.LoadSceneAsync(toLoad, LoadSceneMode.Additive);
                    _currentLoaded = toLoad;
                };
            }
        }

        /// <summary>
        /// Update handling, etc. Use the return value of the game to determine
        /// if we need to be leaving now.
        /// </summary> 
        private void Update()
        {
            if (_game != null)
            {
                if (!_game.Update())
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
        }

        /// <summary>
        /// A consistent update for only very specific types of fixed time operations.
        /// do NOT use this or any subsequent updates for too much that's critical, 
        /// use Time.DeltaTime instead. 
        /// </summary>
        private void FixedUpdate()
        {
            _game?.FixedUpdate();
        }
    }
}