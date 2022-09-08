using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    public class UICrackersFade : CrackersMonoBehaviour
    {
        // Inspector
        [SerializeField] AnimationCurve _curve;
        [SerializeField] private CanvasGroup _fadeTarget;
        [SerializeField] private float _fadeSpeedScalar = 1.5f;

        // Internal
        private bool _doFade = false;
        private float _amt = 0;


        private void Start()
        {
            Game.CrackersAcquired += ResetCountdown;
            _doFade = true;
        }

        private void Update()
        {
            if (_doFade)
            {
                _amt = _amt - (Time.deltaTime * _fadeSpeedScalar);
                float targetAlpha = Mathf.Clamp01(_amt); // Ensure at least zero, otherwise above 0.
                targetAlpha = _curve.Evaluate(targetAlpha); // Dangerous because the curve can dip to 0, which we'll consider done.

                _fadeTarget.alpha = targetAlpha;

                if(targetAlpha <= 0)
                {
                    _doFade = false;
                }
            }
        }

        public void ResetCountdown()
        {
            _amt = 1;
            _fadeTarget.alpha = 1;
            _doFade = true;
        }
    }
}