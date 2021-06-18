using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crackers
{
    public class UIButtonLoadLevel : UIButton
    {
        [SerializeField] private string _toLoad;

        protected override void OnClicked()
        {
            Core.LoadLevel(_toLoad);
        }
    }
}
