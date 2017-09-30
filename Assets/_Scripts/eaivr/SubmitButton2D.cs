using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public class SubmitButton2D : SubmitButton
    {
        private Button button;
        // Use this for initialization
        void Start()
        {
            itemInParent = transform.GetComponentInParent<Item>();
            button = GetComponent<Button>();
            button.onClick.AddListener(OnSelect);
        }

        protected virtual void OnSelect()
        {
            itemInParent.CheckSelectedAnswer();
        }

    }
}

