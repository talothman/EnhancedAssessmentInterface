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
    }
}