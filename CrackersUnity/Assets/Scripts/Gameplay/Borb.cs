using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Borb : CrackersMonoBehaviour
    {
        // Inspector Variabales
        [SerializeField] private List<BorbVisualPair> _visualConfig;

        // Internal Variables
        private BorbState _state;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _state = BorbState.Idle;
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        public void SetState(BorbState desiredState)
        {
            Sprite toSet = GetSpriteForState(desiredState);
            if(toSet != null)
            {
                _spriteRenderer.sprite = toSet;
            }
        }

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