using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Crackers
{
    public class BorbLauncher : CrackersMonoBehaviour
    {
        private Vector2 _startPos;
        private Vector2 _endPos;
        private LineRenderer _lineBoi;
        private Borb _borbPreview;
        private List<Borb> _activeBorbs;

        private void Awake()
        {
            _activeBorbs = new List<Borb>();
        }

        private void Start()
        {
            Game.Input.OnPrimaryActionStart += (pos) =>
            {
                _startPos = pos;
                _endPos = pos;

                CleanVisible();

                _lineBoi = Game.Assets.Create(Game.Assets.LineTemplate);
                _borbPreview = Game.Assets.Create(Game.Assets.Borb);
                _borbPreview.SetState(BorbState.LaunchReady);

                UpdateLinePos();
                UpdateBorbPreview();
            };

            Game.Input.OnPrimaryActionPersist += (pos) =>
            {
                _endPos = pos;

                UpdateLinePos();
                UpdateBorbPreview();
            };

            Game.Input.OnPrimaryActionAccept += (pos) =>
            {
                _borbPreview.SetState(BorbState.Fly);
                _borbPreview.GetComponent<Rigidbody2D>().velocity = (_startPos - _endPos);
                _activeBorbs.Add(_borbPreview);
                _borbPreview = null;

                CleanVisible();
            };
        }

        private void UpdateLinePos()
        {
            if (_lineBoi != null)
            {
                _lineBoi.SetPosition(0, _startPos);
                _lineBoi.SetPosition(1, _endPos);
            }
        }

        private void UpdateBorbPreview()
        {
            _borbPreview.transform.position = _endPos;
            _borbPreview.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(_startPos, _endPos));
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