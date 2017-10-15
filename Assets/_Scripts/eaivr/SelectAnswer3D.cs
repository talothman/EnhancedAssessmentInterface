using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

namespace eaivr
{
    public class SelectAnswer3D : SelectAnswer
    {
        private VRTK_Button_UnityEvents buttonEvents;
        // Use this for initialization
        void Start()
        {
            selectItemParent = transform.GetComponentInParent<SelectItem3D>();

            buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
            if (buttonEvents == null)
            {
                buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
            }

            buttonEvents.OnPushed.AddListener(OnSelect);
            
        }

        protected virtual void OnSelect(object sender, Control3DEventArgs e)
        {
            selectItemParent.SetSelectedAnswer(gameObject);
            GetComponent<VRTK_InteractableObject>().InteractableObjectUntouched += OnUntouched;
            IncrementInteraction();
        }

        protected virtual void OnUntouched(object sender, InteractableObjectEventArgs e)
        {
            (selectItemParent as SelectItem3D).SetHighlightedAnswer(gameObject);
        }
    }
}
