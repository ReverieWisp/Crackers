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
        private PreviewLine _lineBoi;
        private Borb _borbPreview;
        private List<Borb> _activeBorbs;

        // Utility properties
        private Vector2 Direction => _startPos - _endPos;
        private Vector2 Normalized => Direction.normalized;
        private float Angle => Mathf.Atan2(_startPos.y - _endPos.y, _startPos.x - _endPos.x) * Mathf.Rad2Deg;
        private float Magnitude => Direction.magnitude;



        // Configure defaultss
        private void Awake()
        {
            _activeBorbs = new List<Borb>();
            _startPos = Vector2.zero;
            _endPos = Vector2.zero;
            _lineBoi = null;
            _borbPreview = null;
        }

        // Event connect/reconnect
        private void OnEnable()
        {
            Game.Input.OnPrimaryActionStart += BeginPreview;
            Game.Input.OnPrimaryActionPersist += UpdatePreview;
            Game.Input.OnPrimaryActionAccept += LaunchBorb;
        }

        // Event disconnect
        private void OnDisable()
        {
            Game.Input.OnPrimaryActionAccept -= LaunchBorb;
            Game.Input.OnPrimaryActionPersist -= UpdatePreview;
            Game.Input.OnPrimaryActionStart -= BeginPreview;
        }

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

        private void UpdatePreview(Vector2 inputPos)
        {
            _endPos = inputPos;

            UpdateLinePos();
            UpdateBorbPreview();
        }

        private void LaunchBorb(Vector2 inputPos)
        {
            _endPos = inputPos;

            _borbPreview.SetState(BorbState.Fly);
            _borbPreview.GetComponent<Rigidbody2D>().velocity = Normalized * Magnitude * BorbLaunchSpeedScalar;
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
            _lineBoi.UpdateText(Angle, Angle);
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