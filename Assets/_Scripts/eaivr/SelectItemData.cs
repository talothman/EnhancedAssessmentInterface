using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace eaivr
{
    [Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(SelectAnswerData))]
    public class SelectItemData
    {
        public string stem;
        public SelectAnswerData[] selectAnswers;
    }

    [Serializable]
    public struct SelectAnswerData
    {
        public string answerText;
        public bool isKey;
    }
}