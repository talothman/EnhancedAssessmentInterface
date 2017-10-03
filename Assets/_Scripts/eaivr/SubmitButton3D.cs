using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            itemInParent.CheckSelectedAnswers();
            GetComponentInChildren<Text>().text = "Next";
            buttonEvents.OnPushed.RemoveListener(OnSelect);
            buttonEvents.OnPushed.AddListener(OnNext);
        }

        protected void OnNext(object sender, Control3DEventArgs e)
        {
            //buttonEvents.OnPushed.RemoveListener(OnNext);
            itemInParent.NextQuestion();

        }
        
    }
}

