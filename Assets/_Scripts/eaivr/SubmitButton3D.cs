using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

namespace eaivr
{
    public class SubmitButton3D : SubmitButton
    {
        private VRTK_Button_UnityEvents buttonEvents;
        // Use this for initialization
        void Start()
        {
            itemInParent = transform.GetComponentInParent<Item>();

            buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
            if (buttonEvents == null)
            {
                buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
            }

            buttonEvents.OnPushed.AddListener(OnSelect);

        }

        protected void OnSelect(object sender, Control3DEventArgs e)
        {
            itemInParent.CheckSelectedAnswer();
        }
        
    }
}

