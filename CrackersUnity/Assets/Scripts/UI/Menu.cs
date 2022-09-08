using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// NOTE: Done quickly for testing.
    /// </summary>
    public class Menu : CrackersMonoBehaviour
    {
        // Inspector
        [SerializeField] UnityEngine.UI.Button _play;
        [SerializeField] AnimationCurve _curve;
        [SerializeField] private CanvasGroup _fadeTarget;
        [SerializeField] private float _fadeSpeedScalar = 1.5f;

        // Internal
        private bool _doFade = false;
        private float _amt = 1;

        private void Start()
        {
            _play.onClick.AddListener(OnClicked);
            Game.MenuIsVisible = true;
        }

        private void OnClicked()
        {
            _doFade = true;
        }

        private void Update()
        {
            if(_doFade)
            {
                _amt = _amt - (Time.deltaTime * _fadeSpeedScalar);
                float targetAlpha = Mathf.Clamp01(_amt); // Ensure at least zero, otherwise above 0.
                targetAlpha = _curve.Evaluate(targetAlpha); // Dangerous because the curve can dip to 0, which we'll consider done.

                if (targetAlpha <= 0)
                {
                    _fadeTarget.alpha = targetAlpha;
                    _fadeTarget.interactable = false;
                    _fadeTarget.blocksRaycasts = false;
                    Game.MenuIsVisible = false;
                }
                else
                {
                    _fadeTarget.alpha = targetAlpha;
                    Game.MenuIsVisible = true;
                }
            }
        }

        public void SetVisible()
        {
            _amt = 1;
            _fadeTarget.alpha = 1;
            _fadeTarget.interactable = true;
            _fadeTarget.blocksRaycasts = true;
            Game.MenuIsVisible = true;
        }
    }
}