using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr{
    public class SortAnswerGroup2D : SortAnswerGroup
    {
        public SortAnswer2D[] twoDSortAnswers;
        // Use this for initialization
        void Start()
        {
            lRenderer = GetComponent<LineRenderer>();
            lRenderer.positionCount = twoDSortAnswers.Length;

            Array.Sort(twoDSortAnswers, ComparePosition);

            for (int i = 0; i < twoDSortAnswers.Length; i++)
            {
                lRenderer.SetPosition(i, twoDSortAnswers[i].transform.position);
            }
        }

        public static int ComparePosition(SortAnswer2D objectPosition1, SortAnswer2D objectPosition2)
        {
            return objectPosition1.gameObject.GetComponent<RectTransform>().position.x.CompareTo(objectPosition2.gameObject.GetComponent<RectTransform>().position.x);
        }

        public void UpdateSortState()
        {
            Array.Sort(twoDSortAnswers, ComparePosition);

            for (int i = 0; i < twoDSortAnswers.Length; i++)
            {
                twoDSortAnswers[i].currentOrder = i;
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < twoDSortAnswers.Length; i++)
            {
                lRenderer.SetPosition(i, twoDSortAnswers[i].transform.position);
            }
        }
    }
}

