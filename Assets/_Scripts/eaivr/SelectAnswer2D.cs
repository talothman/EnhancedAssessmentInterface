using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr{
    public class SelectAnswer2D : SelectAnswer
    {
        private Button button;
        // Use this for initialization
        void Start()
        {
            selectItemParent = transform.GetComponentInParent<SelectItem2D>();
            button = GetComponent<Button>();
            button.onClick.AddListener(OnSelect);
        }

        protected virtual void OnSelect()
        {
            selectItemParent.SetSelectedAnswer(gameObject);
            IncrementInteraction();
        }
    }
}

