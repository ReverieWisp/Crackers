using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    /// <summary>
    /// THE borb
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Borb : CrackersMonoBehaviour
    {
        // Inspector Variabales
        [SerializeField] private List<BorbVisualPair> _visualConfig;

        // Internal Variables
        private BorbState _state;
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Accumulate references and set defaults
        /// </summary>
        private void Awake()
        {
            _state = BorbState.Idle;
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Fully configures the internal state and associated visuals of the borb
        /// </summary>
        /// <param name="desiredState"></param>
        public void SetState(BorbState desiredState)
        {
            Sprite toSet = GetSpriteForState(desiredState);
            if(toSet != null)
            {
                _spriteRenderer.sprite = toSet;
            }
        }

        /// <summary>
        /// Fish up what borb we're supposed to look like for a state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private Sprite GetSpriteForState(BorbState state)
        {
            foreach(BorbVisualPair pair in _visualConfig)
            {
                if(pair.State == state)
                {
                    return pair.Sprite;
                }
            }

            return null;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_state != BorbState.LaunchReady )
            {
                SetState(BorbState.Crash);
            }

            // If a cracker is involved...
            if (collision.collider.gameObject.tag.Contains("cracker"))
            {
                Destroy(collision.collider.gameObject);
                Game.CrackersAcquired?.Invoke();
            }
        }
    }

    [System.Serializable]
    public enum BorbState
    {
        LaunchReady,
        Fly,
        Crash,
        Idle
    }

    [System.Serializable]
    public class BorbVisualPair
    {
        public BorbState State;
        public Sprite Sprite;
    }

}