using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    [Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(SortAnswerData))]
    public class SortItemData
    {
        public int questionID;
        public string stem;
        public SortAnswerData[] sortAnswers;
    }

    [Serializable]
    public struct SortAnswerData
    {
        public string answerText;
        public int correctOrder;
    }
}