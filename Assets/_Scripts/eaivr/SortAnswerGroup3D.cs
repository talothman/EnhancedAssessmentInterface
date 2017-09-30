using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    public class SortAnswerGroup3D : SortAnswerGroup
    {
        public SortAnswer3D[] threeDSortAnswers;
        // Use this for initialization
        void Start()
        {
            lRenderer = GetComponent<LineRenderer>();
            lRenderer.positionCount = threeDSortAnswers.Length;

            Array.Sort(threeDSortAnswers, ComparePosition);

            for (int i = 0; i < threeDSortAnswers.Length; i++)
            {
                lRenderer.SetPosition(i, threeDSortAnswers[i].transform.position);
            }
        }

        // Update is called once per frame
        void Update()
        {
            for(int i = 0; i < threeDSortAnswers.Length; i++)
            {
                lRenderer.SetPosition(i, threeDSortAnswers[i].transform.position);
            }
        }

        public void UpdateSortState()
        {
            Array.Sort(threeDSortAnswers, ComparePosition);

            for (int i = 0; i < threeDSortAnswers.Length; i++)
            {
                threeDSortAnswers[i].currentOrder = i;
            }
        }

        public static int ComparePosition(SortAnswer3D objectPosition1, SortAnswer3D objectPosition2)
        {
            return objectPosition1.gameObject.GetComponent<Transform>().position.x.CompareTo(objectPosition2.gameObject.GetComponent<Transform>().position.x);
        }
    }
}

