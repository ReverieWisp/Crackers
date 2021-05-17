using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Crackers
{
    public class BorbLauncher : CrackersMonoBehaviour
    {
        // Inspector variables
        [SerializeField] private float BorbLaunchSpeedScalar = 5.0f;

        // Internal variables
        private Vector2 _startPos;
        private Vector2 _endPos;
        private BorbLaunchPreview _lineBoi;
        private Borb _borbPreview;
        private List<Borb> _activeBorbs;

        // Utility properties
        private Vector2 Direction => _startPos - _endPos;
        private Vector2 Normalized => Direction.normalized;
        private float Angle => Mathf.Atan2(_startPos.y - _endPos.y, _startPos.x - _endPos.x) * Mathf.Rad2Deg;
        private float BorbMagnitude => Direction.magnitude * BorbLaunchSpeedScalar;



        /// <summary>
        /// Configure defaults
        /// </summary>
        private void Awake()
        {
            _activeBorbs = new List<Borb>();
            _startPos = Vector2.zero;
            _endPos = Vector2.zero;
            _lineBoi = null;
            _borbPreview = null;
        }

        /// <summary>
        /// Event connect/reconnect
        /// </summary>
        private void OnEnable()
        {
            Game.Input.OnPrimaryActionStart += BeginPreview;
            Game.Input.OnPrimaryActionPersist += UpdatePreview;
            Game.Input.OnPrimaryActionAccept += LaunchBorb;
        }

        /// <summary>
        /// Event disconnect
        /// </summary>
        private void OnDisable()
        {
            Game.Input.OnPrimaryActionAccept -= LaunchBorb;
            Game.Input.OnPrimaryActionPersist -= UpdatePreview;
            Game.Input.OnPrimaryActionStart -= BeginPreview;
        }

        /// <summary>
        /// Initialize and configure both the line and the preview borb.
        /// This event is fired once when the input, say mouse, is pressed down.
        /// </summary>
        /// <param name="inputPos">initual input position for the event.</param>
        private void BeginPreview(Vector2 inputPos)
        {
            _startPos = inputPos;
            _endPos = inputPos;

            CleanVisible();

            _lineBoi = Game.Assets.Create(Game.Assets.LineTemplate);
            _lineBoi.transform.position = _startPos;

            _borbPreview = Game.Assets.Create(Game.Assets.Borb);
            _borbPreview.SetState(BorbState.LaunchReady);

            UpdateLinePos();
            UpdateBorbPreview();
        }

        /// <summary>
        /// Assuming there's already a line and a preview borb, update them.
        /// </summary>
        /// <param name="inputPos">The current position of the input data (a held mouse position, for example)</param>
        private void UpdatePreview(Vector2 inputPos)
        {
            _endPos = inputPos;

            UpdateLinePos();
            UpdateBorbPreview();
        }

        /// <summary>
        /// Assuming there's info associated with the borb and the launch trajectory, send the borb on its way.
        /// </summary>
        /// <param name="inputPos">The last known position of the input data (a held mouse or touch, for example)</param>
        private void LaunchBorb(Vector2 inputPos)
        {
            _endPos = inputPos;

            _borbPreview.SetState(BorbState.Fly);
            _borbPreview.GetComponent<Rigidbody2D>().velocity = Normalized * BorbMagnitude;
            _activeBorbs.Add(_borbPreview);
            _borbPreview = null;

            CleanVisible();
        }

        private void UpdateLinePos()
        {
            if (_lineBoi != null)
            {
                _lineBoi.Set(_startPos, _endPos);
            }
        }

        private void UpdateBorbPreview()
        {
            _borbPreview.transform.position = _endPos;
            _borbPreview.transform.rotation = Quaternion.Euler(0, 0, Angle);
            _lineBoi.UpdateText(string.Format("{0:0.0}", BorbMagnitude), Angle);
        }

        private void CleanVisible()
        {
            Game.Assets.Destroy(_lineBoi?.gameObject);
            Game.Assets.Destroy(_borbPreview?.gameObject);
            _borbPreview = null;
            _lineBoi = null;
        }
    }
}