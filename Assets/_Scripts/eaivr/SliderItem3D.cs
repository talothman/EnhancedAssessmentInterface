using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

namespace eaivr
{
    public class SliderItem3D : SliderItem
    {
        VRTK_Slider slider;
        VRTK_Control_UnityEvents controlEvents;

        private void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
            slider = GetComponentInChildren<VRTK_Slider>();

            controlEvents = slider.gameObject.GetComponent<VRTK_Control_UnityEvents>();
            controlEvents.OnValueChanged.AddListener(HandleChange);
        }

        private void HandleChange(object sender, Control3DEventArgs e)
        {
            if (!submitGameObject.activeInHierarchy)
                submitGameObject.SetActive(true);

            ageText.text = e.value.ToString();
        }

        public override void CheckSelectedAnswers()
        {
            gameManager.Next();
        }
    }
}
