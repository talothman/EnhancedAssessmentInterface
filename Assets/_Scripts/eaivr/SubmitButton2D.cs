using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public class SubmitButton2D : SubmitButton
    {
        private Button button;
        
        void Start()
        {
            itemInParent = transform.GetComponentInParent<Item>();
            button = GetComponent<Button>();

            if (oneButtonSubmit)
                button.onClick.AddListener(itemInParent.NextQuestion);
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
    }
}

