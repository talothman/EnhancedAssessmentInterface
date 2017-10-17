using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public abstract class SliderItem : Item
    {
        public Text ageText;
        public abstract string GetSliderValue();
        public abstract void InsertSliderData(string qID, string stem, int beginValue, int endValue);
        public void IncrementInteraction()
        {
            if (!firstInteraction)
            {
                firstInteraction = true;
                timeToFirstInteraction = Time.time;
            }

            ++numOfInteractions;
        }
    }
}