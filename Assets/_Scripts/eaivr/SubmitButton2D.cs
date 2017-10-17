using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public class SubmitButton2D : SubmitButton
    {
        private Button button;
        AudioSource buttonAudio;

        void Start()
        {
            itemInParent = transform.GetComponentInParent<Item>();
            button = GetComponent<Button>();
            buttonAudio = GetComponent<AudioSource>();

            if (oneButtonSubmit)
                button.onClick.AddListener(ClickListener);
            else
                button.onClick.AddListener(OnSelect);
        }

        protected virtual void OnSelect()
        {
            itemInParent.CheckSelectedAnswers();
            GetComponentInChildren<Text>().text = "Next";
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(itemInParent.NextQuestion);
        }

        public void ClickListener()
        {
            buttonAudio.Play();
            itemInParent.NextQuestion();
        }
    }
}

