using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public class SliderItem2D : SliderItem
    {
        public Slider sliderUI;
        public AudioSource sliderAudio;

        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
            //sliderUI = GetComponentInChildren<Slider>();
            sliderUI.wholeNumbers = true;
            //sliderUI.minValue = 16;
            //sliderUI.maxValue = 65;
            //ageText.text = sliderUI.value.ToString();
            sliderUI.onValueChanged.AddListener(OnSliderValueChanged);
        }

        public override void CheckSelectedAnswers()
        {
            gameManager.Next();
        }

        public void OnSliderValueChanged(float value)
        {
            if (!submitGameObject.activeInHierarchy)
                submitGameObject.SetActive(true);

            ageText.text = value.ToString();
            IncrementInteraction();
            sliderAudio.Play();
        }

        public override string GetSliderValue()
        {
            return ageText.text;
        }

        public override void InsertSliderData(string qID, string stem, int beginValue, int endValue)
        {
            questionID = qID;
            sliderUI.minValue = beginValue;
            sliderUI.maxValue = endValue;
            sliderUI.value = beginValue;
            ageText.text = sliderUI.value.ToString();
            canvasText.text = stem;
            timeStart = Time.time;
        }
    }
}

