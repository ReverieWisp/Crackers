using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crackers
{
    /// <summary>
    /// Base class for buttons to make callbacks easier to set up.
    /// Derive from this class and override OnClicked
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : CrackersMonoBehaviour
    {
        // Internal Variables
        protected Button _button;



        /// <summary>
        /// Happens before anything else
        /// </summary>
        protected void Awake()
        {
            _button = this.gameObject.GetComponent<Button>();
        }

        /// <summary>
        /// Happens after Awake but before Start, and each consecutive enable/disable
        /// </summary>
        protected void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        /// <summary>
        /// Happens with each disable, and before OnDestroy. Good form to remove listeners here to prevent unwanted calls.
        /// </summary>
        protected void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        /// <summary>
        /// Called when the button is pressed, while enabled.
        /// </summary>
        protected abstract void OnClicked();
    }
}