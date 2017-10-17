﻿using System.Collections;
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
        AudioSource buttonSound;
        void Start()
        {
            itemInParent = transform.GetComponentInParent<Item>();
            buttonSound = GetComponent<AudioSource>();

            buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
            if (buttonEvents == null)
            {
                buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
            }

            if (oneButtonSubmit)
                buttonEvents.OnPushed.AddListener(OnNext);
            else
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
            buttonSound.Play();
        }
    }
}

