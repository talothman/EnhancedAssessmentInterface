using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr{
    public class SelectAnswer2D : SelectAnswer
    {
        private Button button;
        AudioSource buttonAudio;

        void Start()
        {
            selectItemParent = transform.GetComponentInParent<SelectItem2D>();
            button = GetComponent<Button>();
            button.onClick.AddListener(OnSelect);
            buttonAudio = GetComponent<AudioSource>();
        }

        protected virtual void OnSelect()
        {
            selectItemParent.SetSelectedAnswer(gameObject);
            IncrementInteraction();
            buttonAudio.Play();
        }
    }
}

