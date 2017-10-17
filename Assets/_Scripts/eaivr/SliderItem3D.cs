using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

namespace eaivr
{
    public class SliderItem3D : SliderItem
    {
        public VRTK_Slider slider;
        VRTK_Control_UnityEvents controlEvents;

        public AudioSource sliderAudio;

        private void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
            //slider = GetComponentInChildren<VRTK_Slider>();

            controlEvents = slider.gameObject.GetComponent<VRTK_Control_UnityEvents>();
            controlEvents.OnValueChanged.AddListener(HandleChange);
        }

        private void HandleChange(object sender, Control3DEventArgs e)
        {
            if (!submitGameObject.activeInHierarchy)
                submitGameObject.SetActive(true);

            ageText.text = e.value.ToString();
            IncrementInteraction();
            sliderAudio.Play();
        }

        public override void CheckSelectedAnswers()
        {
            gameManager.Next();
        }

        public override void NextQuestion()
        {
            base.NextQuestion();
        }

        public override string GetSliderValue()
        {
            return ageText.text;
        }

        public override void InsertSliderData(string qID, string stem, int beginValue, int endValue)
        {
            questionID = qID;
            slider.minimumValue = beginValue;
            slider.maximumValue = endValue;
            slider.stepSize = 1;

            canvasText.text = stem;
            timeStart = Time.time;
        }
    }
}
