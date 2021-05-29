using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Crackers
{
    public class BorbLauncher : CrackersMonoBehaviour
    {
        // Inspector variables
        [SerializeField] private float _launchSpeedAtMax = 10.0f;
        [SerializeField] private Transform _visualStart;
        [SerializeField] private float _startDistance;
        [SerializeField] private float _maxDragDistance = 5.0f;

        // Internal variables
        private Vector2 _startPos;
        private Vector2 _endPos;
        private BorbLaunchPreview _lineBoi;
        private Borb _borbPreview;
        private List<Borb> _activeBorbs;
        private bool _doingLaunch;

        // Utility properties
        private Vector2 DragDifference => _endPos - _startPos;
        private float DragMagnitude => DragDifference.magnitude;
        private Vector2 DragDirection => DragDifference.normalized;
        private float DragAngle => Mathf.Atan2(_startPos.y - _endPos.y, _startPos.x - _endPos.x) * Mathf.Rad2Deg;
        private Vector2 SlingshotStart => _visualStart.position;
        private float TVal => Mathf.Clamp(DragMagnitude / _maxDragDistance, 0.0f, 1.0f);

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
            if (!IsShutdown)
            {
                Game.Input.OnPrimaryActionAccept -= LaunchBorb;
                Game.Input.OnPrimaryActionPersist -= UpdatePreview;
                Game.Input.OnPrimaryActionStart -= BeginPreview;
            }
        }

        /// <summary>
        /// Initialize and configure both the line and the preview borb.
        /// This event is fired once when the input, say mouse, is pressed down.
        /// </summary>
        /// <param name="inputPos">initual input position for the event.</param>
        private void BeginPreview(Vector2 inputPos)
        {
            if(Vector2.Distance(inputPos, _visualStart.transform.position) < _startDistance)
            {
                Vector2 slingshotStart = _visualStart.transform.position;

                _startPos = inputPos;
                _endPos = _startPos;

                CleanVisible();

                _lineBoi = Game.Assets.Create(Game.Assets.LineTemplate);
                _lineBoi.transform.position = _startPos;

                _borbPreview = Game.Assets.Create(Game.Assets.Borb);
                _borbPreview.SetState(BorbState.LaunchReady);

                UpdateLinePos();
                UpdateBorbPreview();

                _doingLaunch = true;
            }
        }

        /// <summary>
        /// Assuming there's already a line and a preview borb, update them.
        /// </summary>
        /// <param name="inputPos">The current position of the input data (a held mouse position, for example)</param>
        private void UpdatePreview(Vector2 inputPos)
        {
            if (_doingLaunch)
            {
                _endPos = inputPos;

                UpdateLinePos();
                UpdateBorbPreview();
            }
        }

        /// <summary>
        /// Assuming there's info associated with the borb and the launch trajectory, send the borb on its way.
        /// </summary>
        /// <param name="inputPos">The last known position of the input data (a held mouse or touch, for example)</param>
        private void LaunchBorb(Vector2 inputPos)
        {
            if (_doingLaunch)
            {
                _endPos = inputPos;

                _borbPreview.SetState(BorbState.Fly);
                _borbPreview.GetComponent<Rigidbody2D>().velocity = -DragDirection * TVal * _launchSpeedAtMax;
                _activeBorbs.Add(_borbPreview);
                _borbPreview = null;

                CleanVisible();

                _doingLaunch = false;
            }
        }

        private void UpdateLinePos()
        {
            if (_lineBoi != null)
            {
                float dist = DragMagnitude;
                if (dist > _maxDragDistance)
                {
                    dist = _maxDragDistance;
                }

                _lineBoi.Set(SlingshotStart, SlingshotStart + DragDirection * dist);
            }
        }

        private void UpdateBorbPreview()
        {
            Vector2 offset = DragDirection * TVal * _maxDragDistance;
            _borbPreview.transform.position = SlingshotStart + offset;
            _borbPreview.transform.rotation = Quaternion.Euler(0, 0, DragAngle);
            _lineBoi.UpdateText($"{(int)(TVal * 100)}%", DragAngle);
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